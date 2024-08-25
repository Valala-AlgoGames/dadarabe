using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromTo : MonoBehaviour
{
    [SerializeField]GameObject from;
    [SerializeField]GameObject to;
    public void GoTo(){
        from.SetActive(false);
        to.SetActive(true);
    }
    public void Quit(){
        Application.Quit();
    }
}
