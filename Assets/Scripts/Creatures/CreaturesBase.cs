using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreaturesBase : MonoBehaviour
{
    [SerializeField] public float MaxLife;
    [SerializeField] public int probability = 5;
    [SerializeField] public int cost = 10;
    [SerializeField] public Sprite icon;
    [SerializeField] public string creatureName = "";

    float life;
    public GameObject owner;
    [SerializeField]ParticleSystem deathParticle;
    [SerializeField]Image lifeUi;
    void Awake() {
        life = MaxLife;
    }
    void Start() {
        if(owner.GetComponent<PlayerController>().playerId=="PLAYER1"){lifeUi.color = new Color(0,125,255);}
        else{lifeUi.color = new Color(255,0,0);}
    }
    public void Kill(){
        if(!CompareTag("Player")){
            GameObject.Instantiate(deathParticle,transform.position,Quaternion.identity);
            if(creatureName=="Kalanoro"){
                if(owner.GetComponent<PlayerController>().playerId == "PLAYER1"){
                    KalanoroBase.p1Number--;
                }
                else{
                    KalanoroBase.p2Number--;
                }
            }
            else if(creatureName == "Kely be tratra"){
                if(owner.GetComponent<PlayerController>().playerId == "PLAYER1"){
                    BetratraBase.p1Number--;
                }
                else{
                    BetratraBase.p2Number--;
                }
            }
            else if(creatureName == "Kokolampy"){
                if(owner.GetComponent<PlayerController>().playerId == "PLAYER1"){
                    KokolampyBase.p1Number--;
                }
                else{
                    KokolampyBase.p2Number--;
                }
            }
            Destroy(gameObject);
        }
        else{
            RemaxLife();
            owner.GetComponent<PlayerController>().DespawnAllCreatures();
            owner.transform.position = new Vector3(owner.GetComponent<PlayerController>().camp.position.x,0.5f,owner.GetComponent<PlayerController>().camp.position.z);
        }
        
    }
    public void MinusLife(float _degat){
        life-=_degat;
        lifeUi.fillAmount = life/MaxLife;
        if(life<=0){
            Kill();
        }
    }
     public void RemaxLife(){
        life = MaxLife;
        lifeUi.fillAmount = MaxLife/MaxLife;
    }
}
