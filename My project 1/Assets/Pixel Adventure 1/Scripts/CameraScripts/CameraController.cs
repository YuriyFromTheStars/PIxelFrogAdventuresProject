using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cinemachine;
    private void OnEnable() => GlobalEventManager.MainPlayerIsDead += StopMoveCamera;
    private void OnDisable() => GlobalEventManager.MainPlayerIsDead -= StopMoveCamera;
    
    private void StopMoveCamera() => cinemachine.Follow = null;    
    
    
        
    
       
    
    
        
    
}
