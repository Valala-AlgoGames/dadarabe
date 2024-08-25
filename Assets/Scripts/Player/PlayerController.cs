using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    Animator animator;
    ParticleSystem particleDirt;
    Rigidbody rb;
    Vector2 axis = Vector2.zero;
    public bool isAI = false;
    [SerializeField] float speed = 5f;
    [SerializeField] public GameObject[] creatures;
    int[] creaturesCumulativeProbability;
    [HideInInspector]public GameObject otherPlayer;
    public SkinnedMeshRenderer bodyVisual;
    List<GameObject> creaturesList;
    [HideInInspector]public List<Transform> pieceList;
    [HideInInspector]public Transform camp;
    [HideInInspector]public string playerId;


    [HideInInspector] public CreatureChooserUI chooserUI;
    [HideInInspector]public int scores = 0;
    [HideInInspector]public int hasina;
    int maxHasina = 30;
    void Awake() {
        creaturesList = new List<GameObject>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        particleDirt = GetComponentInChildren<ParticleSystem>();

        hasina = 5;
        if(playerId == "PLAYER1") UiManager.instance.SetHasinaUiP1(hasina);
        else UiManager.instance.SetHasinaUiP2(hasina);
        StartCoroutine("IncreaseHasina");

        //Cumulative probability initialization
        creaturesCumulativeProbability = new int[creatures.Length];
        int _cumulTmp = 0;
        for (int i = 0; i < creatures.Length; i++)
        {
            _cumulTmp+=creatures[i].GetComponent<CreaturesBase>().probability;
            creaturesCumulativeProbability[i] = _cumulTmp;
        }
    }

    public void SetAI (bool _isAI){
        isAI = _isAI;
        GetComponent<AIBehaviour>().enabled = _isAI;
        GetComponent<PlayerInput>().enabled = !_isAI;
        if(_isAI){
            gameObject.layer = 9;
        }
    }

    public IEnumerator IncreaseHasina(){
        if(hasina<maxHasina) hasina++;
        if(playerId == "PLAYER1") UiManager.instance.SetHasinaUiP1(hasina);
        else UiManager.instance.SetHasinaUiP2(hasina);
        yield return new WaitForSeconds(2f);
        StartCoroutine("IncreaseHasina");
    }
    public void OnMove(InputAction.CallbackContext context){
         axis = context.ReadValue<Vector2>();
    }


    public void OnInvokeCreature0(InputAction.CallbackContext context){
        if(context.started){
            InvokeAndUpdateCreature(0);
        }
    }
    public void OnInvokeCreature1(InputAction.CallbackContext context){
         if(context.started){
            InvokeAndUpdateCreature(1);
        }
    }
    public CreaturesBase InvokeAndUpdateCreature (int _creatureNumber){
        CreaturesBase _invokedCreature = InvokeCreature(chooserUI.items[_creatureNumber].index);
        if(_invokedCreature){
            int _creatureIndex = GenerateRandomCreature();
            CreaturesBase _tmp = creatures[_creatureIndex].GetComponent<CreaturesBase>();
            chooserUI.items[_creatureNumber].UpdateItem(_creatureIndex,_tmp.icon,_tmp.creatureName,_tmp.cost);
        }
        return _invokedCreature;
    }
    public CreaturesBase InvokeCreature(int _creatureIndex){
        bool isInCamp = true;
        if(playerId == "PLAYER1"){
            isInCamp =  transform.position.x<10;
        }
        else{
            isInCamp =  transform.position.x>10;
        }
        if(hasina>=creatures[_creatureIndex].GetComponent<CreaturesBase>().cost && isInCamp){
            GameObject spawned = Instantiate(creatures[_creatureIndex],new Vector3(transform.position.x,transform.position.y,transform.position.z),transform.rotation);
            spawned.GetComponent<CreaturesBase>().owner = gameObject;
            creaturesList.Add(spawned);
            hasina -=creatures[_creatureIndex].GetComponent<CreaturesBase>().cost;
            if(playerId == "PLAYER1") UiManager.instance.SetHasinaUiP1(hasina);
            else UiManager.instance.SetHasinaUiP2(hasina);
            return spawned.GetComponent<CreaturesBase>();
        }
        return null;
    }

    public int[] GenerateCreatureChoice(){
        int[] _tmp = new int[2];
        for (int i = 0; i < _tmp.Length; i++)
        {
            _tmp[i] = GenerateRandomCreature();
        }
        return _tmp;
    }

    public int GenerateRandomCreature(){
        int _tmprand = Random.Range(0,creaturesCumulativeProbability[creaturesCumulativeProbability.Length-1]);
        for (int i = 0; i < creaturesCumulativeProbability.Length; i++)
        {
            if(_tmprand<=creaturesCumulativeProbability[i]){
                return i;
            }
        }
        return creaturesCumulativeProbability.Length-1;
    }
    void Update() {
        if(GameManager.instance.isWinning == false && scores == 3){
            if(playerId == "PLAYER1") GameManager.instance.Win(true);
            else GameManager.instance.Win(false);
        }
        //axis = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        var emmission = particleDirt.emission;
        emmission.rateOverTime = axis.magnitude*4;
        if(!isAI){
            if(axis.magnitude > 0f){
                axis.Normalize();
                animator.SetBool("isRunning",true);
                //Rotate Player
                Vector3 _toLook = new Vector3(axis.x,0f,axis.y);
                Quaternion _targetRotation = Quaternion.LookRotation(_toLook,Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation,_targetRotation,0.25f);

                //Set velocity
                Vector3 _velocity = new Vector3(axis.x*speed,rb.velocity.y,axis.y*speed);
                rb.velocity = _velocity;

            }else{
                animator.SetBool("isRunning",false);
                rb.velocity = new Vector3 (0f,rb.velocity.y,0f);
            }
        }
        else{  
            if(new Vector3(rb.velocity.x,0f, rb.velocity.z).magnitude>0f){
                    animator.SetBool("isRunning",true);
                    Quaternion _targetRotation = Quaternion.LookRotation(new Vector3(rb.velocity.x,0,rb.velocity.z),Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation,_targetRotation,0.25f);
            }
            else{
                    animator.SetBool("isRunning",false);

            }
        }
    }
    public void PlusScores(){
        scores++;
        if(playerId == "PLAYER1"){
            UiManager.instance.SetScoreUiP1(scores);
        }
        else{
            UiManager.instance.SetScoreUiP2(scores);

        }
    }
    public void DespawnAllCreatures(){
        
        foreach (GameObject item in creaturesList)
        {
            if(item){item.GetComponent<CreaturesBase>()?.Kill();}
        }
        creaturesList.Clear();
    }
    public void DespawnCreatures(GameObject _cr){
        creaturesList.Remove(_cr);
        _cr.GetComponent<CreaturesBase>().Kill();
    }
    public Transform GetNearestPieces(Vector3 _position){
        List<Transform> bringablePieceList = new List<Transform>();
        foreach (Transform item in pieceList)
        {
            if(!item.GetComponent<AloaloBase>().isFollowing && item.GetComponent<AloaloBase>().isExisting){
                bringablePieceList.Add(item);
            }
        }
        if(bringablePieceList.Count>0)
        {
            Transform result = bringablePieceList[0];
            foreach (Transform item in bringablePieceList)
            {
                    if(Vector3.Distance(_position,item.position)<Vector3.Distance(_position,result.position))
                    {
                    result = item;
                    }   
            }
            return result;
        }
        else
        {
            return null;
        }
    }

    public void PlusHasina(int n){
        if(hasina<maxHasina){
            hasina = hasina + n;
            if(playerId == "PLAYER1") UiManager.instance.SetHasinaUiP1(hasina);
            else UiManager.instance.SetHasinaUiP2(hasina);
        }
        
    }
}
