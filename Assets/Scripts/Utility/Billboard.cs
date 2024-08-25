using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour

{   
    Transform parent;
    Vector3 camPos;
    public bool bendOnly = false;
    void Start() {
       
        camPos = Camera.main.transform.position;
    }
    void Update()
    {
        parent = transform.parent;
         transform.position = RealBend();
         if(bendOnly)return;
        transform.rotation = Quaternion.LookRotation(new Vector3(camPos.x,0,camPos.z) -transform.position,Vector3.up);
    }
    Vector3 RealBend(){
        float ybend = Mathf.Pow(transform.position.z - Camera.main.transform.position.z,2)*(-Camera.main.GetComponent<ShaderManager>().curve);
        Vector3 bendedPosition = new Vector3(transform.position.x,ybend+parent.position.y,transform.position.z);
        return bendedPosition;
    }
}
