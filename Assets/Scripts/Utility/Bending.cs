using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bending : MonoBehaviour
{
     Transform parent;
    Vector3 camPos;
    public float factor  = 1f;
    // Start is called before the first frame update
    void Start()
    {
         parent = transform.parent;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = RealBend();

    }
    Vector3 RealBend(){
        float ybend = Mathf.Pow(transform.position.z - Camera.main.transform.position.z,2)*(-Camera.main.GetComponent<ShaderManager>().curve);
        Vector3 bendedPosition = new Vector3(transform.position.x,ybend+parent.position.y,transform.position.z);
        transform.rotation = Quaternion.Euler(ybend*factor-90,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
        return bendedPosition;
    }
}
