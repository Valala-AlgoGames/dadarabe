using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureUiItem : MonoBehaviour
{
    public bool isSelected = false;
    [SerializeField] Image creatureIcon;
    [SerializeField] TMP_Text creatureName;
    [SerializeField] TMP_Text creatureCost;
    public int index = 0;

    public void UpdateItem (int _index, Sprite _creatureIcon, string _creatureName, float _creatureCost, bool _isSelected = false){
        isSelected = _isSelected;
        creatureIcon.sprite = _creatureIcon;
        creatureName.text = _creatureName;
        creatureCost.text = _creatureCost.ToString();
        index = _index;
    }
}
