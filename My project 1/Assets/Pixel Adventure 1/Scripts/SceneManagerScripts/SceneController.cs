using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static int currentScene;
    public static int PropertyCurrentScene { get { return currentScene; } }
    private void OnEnable()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        GlobalEventManager.MainPlayerIsDead += InvokeRestartLevel;
        GlobalEventManager.FinishLevel += InvokeNextLevel;
    }
    private void OnDisable()
    { 
        
        GlobalEventManager.MainPlayerIsDead -= InvokeRestartLevel;
        GlobalEventManager.FinishLevel -= InvokeNextLevel;
    }
   
    private void InvokeRestartLevel() => Invoke(nameof(RestartLevel), 3);
    private void InvokeNextLevel() => Invoke(nameof(NextLevel), 3);   
    private void RestartLevel() => SceneManager.LoadScene(currentScene);    
    private void NextLevel() => SceneManager.LoadScene(currentScene + 1);
    

    
    
    

    








}
