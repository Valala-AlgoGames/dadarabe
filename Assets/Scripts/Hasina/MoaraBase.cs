using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoaraBase : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerController>().PlusHasina(1);
            HasinaManager.instance.GetMoara(this.gameObject);
        }
    }
}
