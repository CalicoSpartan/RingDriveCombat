using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DummySettings : MonoBehaviour {
    public float vertSensSlider;
    public float horzSensSlider;
    float tempVertSens;
    float tempHorzSens;
    public Slider horzSlider;
    public Slider vertSlider;
    public bool hasApplied = false;
    // Use this for initialization
    void Start () {
        LoadedSettings();
        tempHorzSens = GameObject.Find("_app").GetComponent<SettingsScript>().horzSensSlider;
        horzSlider.value = tempHorzSens;
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
        GameObject.Find("_app").GetComponent<SettingsScript>().SetVertAndHorzSensStatic(tempVertSens, tempHorzSens);
    }

    public void LoadedSettings()
    {
        Debug.Log("Loaded Settings");
        tempVertSens = GameObject.Find("_app").GetComponent<SettingsScript>().vertSensSlider;
        tempHorzSens = GameObject.Find("_app").GetComponent<SettingsScript>().horzSensSlider;
        
        Debug.Log("vertical" + tempVertSens);
        Debug.Log("horizontal:" + tempHorzSens);
        vertSlider.value = tempVertSens;
        horzSlider.value = tempHorzSens;

    }

    public void ApplyChanges()
    {
        GameObject.Find("_app").GetComponent<SettingsScript>().ApplyChanges();
    }
}
