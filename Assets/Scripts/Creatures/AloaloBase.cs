using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AloaloBase : MonoBehaviour
{
    public bool isFollowing = false;
    public bool isExisting = true;
    public GameObject owner;
    Transform toFollow;
    [SerializeField]public int id = 0;
    public void SetFollow(Transform _transform){
        isFollowing = true;
        toFollow = _transform;
    }
    void Update() {
        if(toFollow==null){
            if(isExisting){
                isFollowing=false;
                transform.position = new Vector3(transform.position.x,1f,transform.position.z);
            }
        }
        if(isFollowing && isExisting){
            transform.position = new Vector3(toFollow.position.x,toFollow.position.y+2,toFollow.position.z);
        }
        
    }
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("camp") && other.GetComponent<CampBase>().owner!=owner){
            other.GetComponent<AudioSource>().Play();
            other.GetComponent<CampBase>().owner.GetComponent<PlayerController>().DespawnCreatures(toFollow.gameObject);
            other.GetComponent<CampBase>().owner.GetComponent<PlayerController>().PlusScores();
            //gameObject.SetActive(false);
            isExisting = false;
            if(other.GetComponent<CampBase>().owner.GetComponent<PlayerController>().playerId == "PLAYER1"){
                other.GetComponent<CampBase>().AddCapturedP1(this);
            }
            else{
                other.GetComponent<CampBase>().AddCapturedP2(this);
            }
        }
    }
}
