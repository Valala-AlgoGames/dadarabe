using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokolampyBase : MonoBehaviour
{
    AudioSource audioSource;
    static public int p1Number = 0;
    static public int p2Number = 0;
    Rigidbody rb;
    PathFindingAgent pathfinding;
    Animator animator;
    [SerializeField]float hitPoints = 75f;
    GameObject otherPlayer;
    [SerializeField]SkinnedMeshRenderer body;
    Vector3 initialPosition;
    enum BetratraState
    {
        Free,
        Follow,
        Attack,
    }
    BetratraState actualBetratraState = BetratraState.Free;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        initialPosition = transform.position;
        otherPlayer = GetComponent<CreaturesBase>().owner.GetComponent<PlayerController>().otherPlayer;
        InitColor();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        pathfinding = GetComponent<PathFindingAgent>();
        GetComponentInChildren<AttackZone>().enterAction = OnEnter;
        GetComponentInChildren<AttackZone>().stayAction = OnStay;
        attackAction = OnHit;
        if(GetComponent<CreaturesBase>().owner.GetComponent<PlayerController>().playerId == "PLAYER1"){
            KokolampyBase.p1Number++;
        }
        else{
            KokolampyBase.p2Number++;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        //pathfinding.GoTo(player.position);
        float velocityMagnitudeXZ = new Vector3(rb.velocity.x,0,rb.velocity.z).magnitude;
        if(velocityMagnitudeXZ>0.1f){
            animator.SetBool("isRunning",true);
            transform.rotation =Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(new Vector3(rb.velocity.x,0,rb.velocity.z),Vector3.up),0.25f);
        }
        else{
            animator.SetBool("isRunning",false);
        }
        if(!GetComponentInChildren<AttackZone>().GetComponent<Collider>().providesContacts){
            actualBetratraState = BetratraState.Free;
        }
    }
    IEnumerator TakeInitialPlace(){
        yield return new WaitForSeconds(2f);
        if(!animator.GetBool("isAttacking") && !animator.GetBool("isRunning")){
            pathfinding.GoTo(initialPosition);
        }
    }
    public bool canAttack = true;
    public System.Action attackAction;
    Collider attackedCollider;
    void OnEnter(Collider other){
        
    }
    void OnStay(Collider other) {
        if(other.CompareTag("creatures")){
            if(other.GetComponent<CreaturesBase>().owner!=GetComponent<CreaturesBase>().owner){
                if(attackedCollider){
                    Debug.Log(Vector3.Distance(other.transform.position,transform.position)+"----"+Vector3.Distance(attackedCollider.transform.position,transform.position));
                    if(Vector3.Distance(other.transform.position,transform.position)<Vector3.Distance(attackedCollider.transform.position,transform.position)){
                    Debug.Log("ATTACK COLLIDER CHANGE");
                    attackedCollider = other;
                    }
                }
                else{
                    attackedCollider = other;
                    Debug.Log("ATTACK COLLIDER HANGE FIRST");
                }
                
                if(Vector3.Distance(transform.position,attackedCollider.transform.position)<1.5f && attackedCollider.gameObject){     
                    actualBetratraState = BetratraState.Attack;
                    animator.SetBool("isAttacking",true);
                    audioSource.Play();
                    if(attackedCollider.CompareTag("creatures") && canAttack){
                        pathfinding.Stop();
                        transform.rotation = Quaternion.LookRotation(attackedCollider.transform.position-transform.position,Vector3.up);
                        canAttack = false;
                    }
                }
                else{
                    actualBetratraState = BetratraState.Follow;
                    animator.SetBool("isAttacking",false);
                    canAttack = true;
                    pathfinding.GoTo(attackedCollider.transform.position);
                }
            }
            
        }
    }
    void InitColor(){
        if(GetComponent<CreaturesBase>().owner.GetComponent<PlayerController>().playerId == "PLAYER1"){
            body.material.DisableKeyword("_PLAYERTWO");
        }
        else{
            body.material.EnableKeyword("_PLAYERTWO");
        }
    }
    void OnHit(){
        if(attackedCollider){
            attackedCollider?.GetComponent<CreaturesBase>().MinusLife(Random.Range(hitPoints-hitPoints/2,hitPoints));
            canAttack = true;
        }
    }
}
