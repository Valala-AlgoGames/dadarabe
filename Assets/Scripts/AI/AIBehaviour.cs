using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    
    public enum Difficulty {
        easy,
        hard
    }
    public enum AIState {
        gatherMoara,
        defend,
        attack,
        run
    }

    public AIState aIState = AIState.gatherMoara;
    public bool isMoving = false;
    PathFindingAgent pathFindingAgent;
    PlayerController playerController;

    //DODa
    int generatedDefender = 0;
    int generatedRunner = 0;

    void Awake() {
        pathFindingAgent = GetComponent<PathFindingAgent>();
        playerController = GetComponent<PlayerController>();
    }

    public void MoveTo(Vector3 _target){
        isMoving = true;
        pathFindingAgent.speed = 3f;
        pathFindingAgent.GoTo(_target);
    }


    void FixedUpdate()
    {
        if(pathFindingAgent.targetIndex == 0){
            isMoving = false;
        }else isMoving = true;

        UpdateState(MainMenu.difficulty);
        
        if(aIState == AIState.gatherMoara){
            GatherMoara();
        }
        if(aIState == AIState.defend){
            Defend();
        }
    }

    public void UpdateState(Difficulty _difficulty){
        if(_difficulty == Difficulty.hard){
            Hard();
        }
        if(_difficulty == Difficulty.easy){
            Easy();
        }
    }
    public void Hard(){
        if(playerController.hasina<10f){
            aIState = AIState.gatherMoara;
        }else{
            if(generatedDefender<3){
                if(Vector3.Distance(playerController.transform.position,playerController.pieceList[generatedDefender].position)>1f)MoveTo(playerController.pieceList[generatedDefender].position);
                aIState = AIState.defend;
            }else if (generatedDefender>=3 && generatedRunner <= 2){
                if(playerController.hasina>15f) Run();
            }else{
                 if(!IsInZone(playerController)) MoveTo(playerController.camp.transform.position);
                if(IsInZone(playerController))playerController.InvokeAndUpdateCreature(Random.Range(0,2));
            }
        }
    }
    public void Easy(){
        if(playerController.hasina<15f){
            aIState = AIState.gatherMoara;
        }else{
            if(!IsInZone(playerController)) MoveTo(playerController.camp.transform.position);
            if(IsInZone(playerController))playerController.InvokeAndUpdateCreature(Random.Range(0,2));
        }
    }

    public void Run(){
        Debug.Log("Runner GENERATION");
        playerController.InvokeAndUpdateCreature(CreatureChooserUI.instance.PrioritizeRunner());
        generatedRunner++;
        aIState = AIState.gatherMoara;
    }

    public void Defend(){
        if(isMoving == false){
            Debug.Log("DEFENDER GENERATION");
            playerController.InvokeAndUpdateCreature(CreatureChooserUI.instance.PrioritizeDefender());
            generatedDefender++;
        }
    }
    public void Attack(){
        Debug.Log("Runner GENERATION");
        playerController.InvokeAndUpdateCreature(CreatureChooserUI.instance.PrioritizeAttacker());
        aIState = AIState.gatherMoara;
    }

    public bool IsInZone(PlayerController _player){
        bool _isPLayer1 = _player.playerId == "PLAYER1";
        if(_isPLayer1){
            return _player.transform.position.x<9f;
        }else{
            return _player.transform.position.x>11f;
        }
    }
    public bool IsInZone(bool _isPLayer1,Vector3 _position){
        if(_isPLayer1){
            return _position.x<16f;
        }else{
            return _position.x>4f;
        }
    }

    
    public void GatherMoara (){
        if(HasinaManager.instance.moaraList.Count >0){
            Vector3 _closestMoara = GetClosestMoara(transform.position,HasinaManager.instance.moaraList);
            if(!isMoving)MoveTo(_closestMoara);
        }
    }

    public Vector3 GetClosestMoara (Vector3 _pos, List<GameObject> _listMoara){
        Vector3 _closetsMoara = _pos;
        float _closestDistance = float.PositiveInfinity;
        foreach (GameObject _moara in _listMoara)
        {
            float _dist = Vector3.Distance (_pos,_moara.transform.position);
            if(_dist<_closestDistance && IsInZone(playerController.playerId == "PLAYER1",_moara.transform.position)){
                _closetsMoara = _moara.transform.position;
            }
        }
        return _closetsMoara;
    }
}
