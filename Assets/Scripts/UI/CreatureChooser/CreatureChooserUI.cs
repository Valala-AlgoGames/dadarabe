using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CreatureChooserUI : MonoBehaviour
{
    [SerializeField] public List<CreatureUiItem> items;
    public GameObject owner;
    PlayerController controller;

    bool isSelecting = false;

    public static CreatureChooserUI instance = null;

    void Awake() {
        if(instance!=null){
            Destroy(instance);
        }
        instance = this;
    }

    public int PrioritizeDefender(){
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].index == 1)return i;
        }
        return Random.Range(0,2);
    }
    public int PrioritizeRunner(){
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].index == 0)return i;
        }
        return Random.Range(0,2);
    }
    public int PrioritizeAttacker(){
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].index == 2)return i;
        }
        return Random.Range(0,2);
    }

    public void InitChooser(GameObject _owner){
        owner = _owner;
         controller = owner.GetComponent<PlayerController>();
        for (int i = 0; i < items.Count; i++)
        {
            int _creatureIndex = controller.GenerateRandomCreature();
            CreaturesBase _tmp = controller.creatures[_creatureIndex].GetComponent<CreaturesBase>();
            items[i].UpdateItem(_creatureIndex,_tmp.icon,_tmp.creatureName,_tmp.cost);
        }
    }
}
