using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AttackZone : MonoBehaviour
{
    public UnityAction<Collider> stayAction;
    public UnityAction<Collider> enterAction;
    void OnTriggerStay(Collider other) {
        stayAction?.Invoke(other);
    }
    void OnTriggerEnter(Collider other) {
        
    }
}
