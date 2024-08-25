using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Image timerFill;
    float timer = 900f;
    [SerializeField] GameObject endPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] TMP_Text winText;
    public bool isWinning = false;

    public static GameManager instance = null;
    void Awake() {
        if(instance!=null){
            Destroy(instance);
        }
        instance = this;
    }
    bool isPAuse = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPAuse)Pause();
            else Resume();
        }
        if(timer<=0f){
            timer = 0f;
            if(PlayerManager.instance.playerController1.scores == PlayerManager.instance.playerController2.scores){
                Draw();
            }else if(PlayerManager.instance.playerController1.scores > PlayerManager.instance.playerController2.scores){
                Win(true);
            }else if(PlayerManager.instance.playerController1.scores < PlayerManager.instance.playerController2.scores){
                Win(false);
            }
        }else{
            timer = timer-Time.deltaTime; 
        }
        timerFill.fillAmount = timer/900f;
    }

    public void Draw(){
        if (isWinning) return;
        isWinning = true;
        winText.text = "It's a Draw";
        winText.color = Color.green;
        
        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win(bool isPlayer1){
        if (isWinning) return;
        isWinning = true;
        if(isPlayer1){
            winText.text = "Blue Player Win";
            winText.color = Color.blue;
        }else{
            winText.text = "Red Player Win";
            winText.color = Color.red;
        }
        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Pause(){
        isPAuse = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume (){
        isPAuse = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReturnToMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
