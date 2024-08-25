using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,360*Time.fixedDeltaTime,0));
    }
}
