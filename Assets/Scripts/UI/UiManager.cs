using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    [SerializeField]TMP_Text p1Score;
    [SerializeField]TMP_Text p2Score;

    [SerializeField]TMP_Text p1Hasina;
    [SerializeField]TMP_Text p2Hasina;

     [SerializeField]Image p1HasinaGauge;
    [SerializeField]Image p2HasinaGeuge;

    void Awake() {
        if(instance!=null){
            Destroy(instance);
        }
        instance = this;
    }
    public void SetHasinaUiP1(int n){
        p1Hasina.text = n.ToString();
        p1HasinaGauge.fillAmount = (float)n/30f;
    }
    public void SetHasinaUiP2(int n){
        p2Hasina.text = n.ToString();
        p2HasinaGeuge.fillAmount = (float)n/30f;
    }
    public void SetScoreUiP1(int n){
        p1Score.text = n.ToString();
    }
    public void SetScoreUiP2(int n){
        p2Score.text = n.ToString();
    }
}
