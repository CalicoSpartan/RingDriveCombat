using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DummySettings : MonoBehaviour {
    public float vertSensSlider;
    public float horzSensSlider;
    float tempVertSens;
    float tempHorzSens;
    float tempMasterVolume;
    public Slider horzSlider;
    public Slider vertSlider;
    public Slider masterVolumeSlider;
    public bool hasApplied = false;
    // Use this for initialization
    void Start () {
        LoadedSettings();
        tempHorzSens = GameObject.Find("_app").GetComponent<SettingsScript>().horzSensSlider;
        horzSlider.value = tempHorzSens;
        tempMasterVolume = GameObject.Find("_app").GetComponent<SettingsScript>().masterVolume;
        masterVolumeSlider.value = tempMasterVolume;

        //Debug.Log("Dummy started");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadPreviousScene()
    {
        GameObject.Find("_app").GetComponent<SettingsScript>().LoadPreviousScene();
    }

    public void SetVertandHorzSens()
    {
        tempVertSens = vertSlider.value;
        tempHorzSens = horzSlider.value;
        tempMasterVolume = masterVolumeSlider.value;
        GameObject.Find("_app").GetComponent<SettingsScript>().SetSettings(tempVertSens, tempHorzSens,tempMasterVolume);
    }

    public void LoadedSettings()
    {
        tempVertSens = GameObject.Find("_app").GetComponent<SettingsScript>().vertSensSlider;
        tempHorzSens = GameObject.Find("_app").GetComponent<SettingsScript>().horzSensSlider;
        tempMasterVolume = GameObject.Find("_app").GetComponent<SettingsScript>().masterVolume;
        vertSlider.value = tempVertSens;
        horzSlider.value = tempHorzSens;
        masterVolumeSlider.value = tempMasterVolume;

    }

    public void ApplyChanges()
    {
        GameObject.Find("_app").GetComponent<SettingsScript>().ApplyChanges();
    }
}
