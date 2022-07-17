using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalEventManager : MonoBehaviour
{

    [SerializeField] private GameObject mainPlayer;
    [SerializeField] private GameObject startFlag;
    [SerializeField] private GameObject finishCup;
    private bool startLevel;

    private void Start()
    {
        startLevel = true;
    }
    private void Update()
    {
        if ((int)mainPlayer.transform.position.x == (int)startFlag.transform.position.x && MainPlayer.PropertyMainPlayerIsLive)
        {  
            StartLevel?.Invoke();
            if (startLevel)
            {
                TextLevel?.Invoke();
                startLevel = false;
            }   
        }

    }
    public static Action MainPlayerIsDead;
    public static Action StartLevel;
    public static Action FinishLevel;
    public static Action TextLevel;
    
  
}
