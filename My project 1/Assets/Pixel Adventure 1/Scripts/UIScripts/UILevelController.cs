using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILevelController : MonoBehaviour
{
    [SerializeField] private Text textLevel;

    private GameObject textLevelObject;
    private int currentScene;
    private void OnEnable() => GlobalEventManager.TextLevel += ShowTextLevelActivation;
    private void OnDisable() => GlobalEventManager.TextLevel -= ShowTextLevelActivation;

    private void Start()
    {
        currentScene = SceneController.PropertyCurrentScene + 1;
        textLevelObject = GameObject.Find(textLevel.text);
    } 
    private void ShowTextLevelActivation()
    {
        textLevel.gameObject.SetActive(true);
        textLevel.text = "LEVEL: " + currentScene;
        Invoke("ShowTextLevelDeactivation", 3);
    }     
    private void ShowTextLevelDeactivation() => textLevel.gameObject.SetActive(false);
    
       
    
   

    
    

    
    
        
    

   
}
