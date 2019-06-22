using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    // Use this for initialization
    public float verticalMouseSensitivity = 1f;
    public float horizontalMouseSensitivity = 1f;


	void Start () {
		
	}

    public void UpdatePlayerSettings()
    {
        if (GameObject.Find("Player"))
        {
            Debug.Log("HorzSens = " + horizontalMouseSensitivity);
            Debug.Log("VertSens = " + verticalMouseSensitivity);
            GameObject.Find("Player").GetComponent<PlayerController>().horizontalLookSpeed = horizontalMouseSensitivity;
            GameObject.Find("Player").GetComponent<PlayerController>().verticalLookSpeed = verticalMouseSensitivity;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
