using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalanoroBase : MonoBehaviour
{
    static public int p1Number = 0;
    static public int p2Number = 0;
    Rigidbody rb;
    PathFindingAgent pathfinding;
    Animator animator;
    [SerializeField] Timer timer;
    [SerializeField] SkinnedMeshRenderer hair;
    GameObject otherPlayer;
    bool waiting = false;
    enum KalanoroState
    {
        Free,
        Waiting,
        Bring
    }
    KalanoroState kalanoroActualState = KalanoroState.Free;
    void Start()
    {
        otherPlayer = GetComponent<CreaturesBase>().owner.GetComponent<PlayerController>().otherPlayer;
        rb = GetComponent<Rigidbody>();
        InitColor();
        animator = GetComponentInChildren<Animator>();
        pathfinding = GetComponent<PathFindingAgent>();
        //FindNearestPiece();
        if(GetComponent<CreaturesBase>().owner.GetComponent<PlayerController>().playerId == "PLAYER1"){
            KalanoroBase.p1Number++;
        }
        else{
            KalanoroBase.p2Number++;
        }
    }
    void FindNearestPiece(){
        Transform piecePosition = otherPlayer.GetComponent<PlayerController>().GetNearestPieces(transform.position);
        if(piecePosition){
            pathfinding.GoTo(piecePosition.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        float velocityMagnitudeXZ = new Vector3(rb.velocity.x,0,rb.velocity.z).magnitude;
        if(velocityMagnitudeXZ>0.1f){
            animator.SetBool("isRunning",true);
            transform.rotation =Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(new Vector3(rb.velocity.x,0,rb.velocity.z),Vector3.up),0.25f);
        }
        else{
            animator.SetBool("isRunning",false);
            if(kalanoroActualState ==  KalanoroState.Free){
                StartCoroutine("TakeAnotherPiece");
            }
        }
    }
    IEnumerator TakeAnotherPiece(){
    yield return new WaitForSeconds(1f);
     if(!waiting){
                FindNearestPiece();
     }
    }
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("aloalo") && GetComponent<CreaturesBase>().owner!=other.GetComponent<AloaloBase>()?.owner){
            if(!other.GetComponent<AloaloBase>().isFollowing){
                kalanoroActualState = KalanoroState.Waiting;
                timer.SetAction(()=>{
                     if(!other.GetComponent<AloaloBase>().isFollowing){
                        kalanoroActualState = KalanoroState.Bring;
                        ReturnAction(other.gameObject);
                     }
                     else{
                        kalanoroActualState = KalanoroState.Free;
                        FindNearestPiece();
                     }
                });
                timer.SetTime(3f); 
            }
            
        }
    }

    void ReturnAction(GameObject _go){
        _go.GetComponent<AloaloBase>().SetFollow(transform);
        pathfinding.GoTo(new Vector3(GetComponent<CreaturesBase>().owner.GetComponent<PlayerController>().camp.position.x,0,transform.position.z));
    }

    void InitColor(){
        if(GetComponent<CreaturesBase>().owner.GetComponent<PlayerController>().playerId == "PLAYER1"){
            hair.material.DisableKeyword("_PLAYERTWO");
        }
        else{
            hair.material.EnableKeyword("_PLAYERTWO");
        }
    }
}
