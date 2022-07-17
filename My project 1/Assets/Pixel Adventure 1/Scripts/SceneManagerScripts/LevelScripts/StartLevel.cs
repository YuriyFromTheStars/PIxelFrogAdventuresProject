using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{     
    
    private void OnEnable() => GlobalEventManager.StartLevel += StartLevelMethod;
    private void OnDisable() => GlobalEventManager.StartLevel -= StartLevelMethod;
    private void StartLevelMethod() => GetComponent<Animator>().enabled = true;
      
}
