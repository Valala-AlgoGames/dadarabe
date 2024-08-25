using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public static AIBehaviour.Difficulty difficulty = AIBehaviour.Difficulty.hard;
    public static bool isAiPlay = false;
    public void PlaySingle(){
        isAiPlay = false;
        SceneManager.LoadScene(1);
    }
    public void PlayEasy(){
        difficulty = AIBehaviour.Difficulty.easy;
        isAiPlay = true;
        SceneManager.LoadScene(1);
    }
    public void PlayHard(){
        difficulty = AIBehaviour.Difficulty.hard;
        isAiPlay = true;
        SceneManager.LoadScene(1);
    }
}
