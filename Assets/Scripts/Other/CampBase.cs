using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampBase : MonoBehaviour
{
    [SerializeField]Transform[] aloaloListP1;
    [SerializeField]Transform[] aloaloListP2;


    void Awake() {
    }
    public void AddCapturedP1(AloaloBase _al){
        _al.transform.position = aloaloListP1[_al.id].position;
    }
    public void AddCapturedP2(AloaloBase _al){
        _al.transform.position = aloaloListP2[_al.id].position;

    }
   
    [HideInInspector]public GameObject owner;
}
