using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCup : MonoBehaviour
{

    private void OnEnable() => GlobalEventManager.FinishLevel += FinishLevelMethod;
    private void OnDisable() => GlobalEventManager.FinishLevel -= FinishLevelMethod;
    private void FinishLevelMethod() => GetComponent<Animator>().enabled = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainPlayer"))  { GlobalEventManager.FinishLevel?.Invoke(); }           
    }
}
