using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    // Use this for initialization
    public float verticalMouseSensitivity = 1f;
    public float horizontalMouseSensitivity = 1f;
    public float masterVolume = .5f;
    public bool bInputEnabled = true;


	void Start () {
        UpdateMasterVolume(masterVolume);
	}

    public void UpdatePlayerSettings()
    {
        if (GameObject.Find("Player"))
        {
            //Debug.Log("HorzSens = " + horizontalMouseSensitivity);
            //Debug.Log("VertSens = " + verticalMouseSensitivity);
            GameObject.Find("Player").GetComponent<PlayerController>().horizontalLookSpeed = horizontalMouseSensitivity;
            GameObject.Find("Player").GetComponent<PlayerController>().verticalLookSpeed = verticalMouseSensitivity;
        }
    }

    public void UpdateMasterVolume(float newVolume)
    {
        masterVolume = newVolume;
        AudioListener.volume = masterVolume;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
