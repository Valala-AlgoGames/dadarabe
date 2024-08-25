using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float timeMax;
    float timeCount;
    bool isFinished = true;
    Image visual;
    public bool haveVisual = true;
    System.Action OnFinish;
    void Start()
    {
        visual = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFinished){
            if(timeCount>0){
                timeCount-=Time.deltaTime;
                if(haveVisual){
                visual.enabled = (true);
                visual.fillAmount = timeCount/timeMax;
                }
            }
            else{
                OnFinish?.Invoke();
                isFinished = true;
                if(haveVisual){
                visual.enabled = (false);
                }
            }
        }
       
    }
    public void SetAction(System.Action _action){
        OnFinish = _action;
    }
    public void SetTime(float _time){
        timeMax = _time;
        timeCount = _time;
        isFinished = false;
    }
}
