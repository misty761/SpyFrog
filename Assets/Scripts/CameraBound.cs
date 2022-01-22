using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    BoxCollider2D bound;
    CameraManager cameraManager;
    
    // Start is called before the first frame update
    void Start()
    {
        bound = GetComponent<BoxCollider2D>();
        cameraManager = FindObjectOfType<CameraManager>();
        cameraManager.SetBound(bound);
    }
}
