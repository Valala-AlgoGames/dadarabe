using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerManager : MonoBehaviour
{
    [SerializeField]GameObject player;
    [SerializeField] CreatureChooserUI creatureChooserP1;
    [SerializeField] CreatureChooserUI creatureChooserP2;
    [SerializeField] public List<Transform> pieceListP1;
    [SerializeField] public List<Transform> pieceListP2;
    public Transform campBlue;
    public Transform campRed;
    public PlayerController playerController1,playerController2;

    public static PlayerManager instance = null;

    void Awake()
    {
        if(instance!=null){
            Destroy(instance);
        }
        instance = this;


        //PLAYER1 INIT
       PlayerInput p1 = PlayerInput.Instantiate(player,controlScheme:"Keyboard2",pairWithDevice:Keyboard.current);
       p1.GetComponent<PlayerController>().playerId = "PLAYER1";
       p1.transform.position = new Vector3(0,0.5f,0);
       p1.GetComponent<PlayerController>().bodyVisual.material.DisableKeyword("_PLAYERTWO");
       p1.GetComponent<PlayerController>().pieceList = pieceListP1;
       foreach (Transform item in pieceListP1)
       {
           item.GetComponent<AloaloBase>().owner = p1.gameObject;
       }
       p1.GetComponent<PlayerController>().camp = campBlue;
       campBlue.GetComponent<CampBase>().owner = p1.gameObject;
       p1.gameObject.GetComponent<CreaturesBase>().owner = p1.gameObject;
       creatureChooserP1.InitChooser(p1.gameObject);
       p1.GetComponent<PlayerController>().chooserUI = creatureChooserP1;
       p1.GetComponent<PlayerController>().SetAI(false);
       playerController1 =  p1.GetComponent<PlayerController>();

        //PLAYER2 INIT
       PlayerInput p2 = PlayerInput.Instantiate(player,controlScheme:"Keyboard1",pairWithDevice:Keyboard.current);
       p2.GetComponent<PlayerController>().playerId = "PLAYER2";
       p2.transform.position = new Vector3(19,0.5f,0);
       p2.GetComponent<PlayerController>().bodyVisual.material.EnableKeyword("_PLAYERTWO");
       p2.GetComponent<PlayerController>().pieceList = pieceListP2;
       foreach (Transform item in pieceListP2)
       {
           item.GetComponent<AloaloBase>().owner = p2.gameObject;
       }
       p2.GetComponent<PlayerController>().camp = campRed;
       campRed.GetComponent<CampBase>().owner = p2.gameObject;
       p2.gameObject.GetComponent<CreaturesBase>().owner = p2.gameObject;
       creatureChooserP2.InitChooser(p2.gameObject);
       p2.GetComponent<PlayerController>().chooserUI = creatureChooserP2;
       p2.GetComponent<PlayerController>().SetAI(MainMenu.isAiPlay);
       playerController2 =  p2.GetComponent<PlayerController>();


       p1.gameObject.GetComponent<PlayerController>().otherPlayer = p2.gameObject;
       p2.gameObject.GetComponent<PlayerController>().otherPlayer = p1.gameObject;
    }

    
}
