using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingManager : MonoBehaviour {
    public float rotationSpeed = 5f;
    public float fastSpeedMultiplier = 1.4f;
    public bool goFast = false;
	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!LevelManager.bPaused)
        {
            if (goFast == false)
            {
                transform.Rotate(0f, 0f, rotationSpeed, Space.Self);
            }
            else
            {
                transform.Rotate(0f, 0f, rotationSpeed * fastSpeedMultiplier, Space.Self);
            }
        }
	}
}
