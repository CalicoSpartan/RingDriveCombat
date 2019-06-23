using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingsScript : MonoBehaviour {
    public float vertSensSlider = 1.0f;
    public float horzSensSlider = 1.0f;
    float tempVertSens = 1.0f;
    float tempHorzSens = 1.0f;
    public bool hasApplied = false;

    public void LoadPreviousScene()
    {

        string prevScene = GameObject.Find("_app").GetComponent<GameData>().previousSceneName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(prevScene);
    }


    /*
    public void LoadedSettings()
    {
        tempHorzSens = horzSensSlider;
        tempVertSens = vertSensSlider;
        vertSlider.value = tempVertSens;
        horzSlider.value = tempHorzSens;

    }
    */

    public void SetVertAndHorzSensStatic(float vert, float horz)
    {
        tempVertSens = vert;
        tempHorzSens = horz;
    }

    /*
    public void SetVertandHorzSens()
    {
        tempVertSens = vertSlider.value;
        tempHorzSens = horzSlider.value;
    }
    */


    public void ApplyChanges()
    {
        hasApplied = true;
        horzSensSlider = tempHorzSens;
        vertSensSlider = tempVertSens;
        Debug.Log("vertical" + vertSensSlider);
        Debug.Log("horizontal:" +  horzSensSlider);
        
        GameObject.Find("_app").GetComponent<GameSettings>().horizontalMouseSensitivity = horzSensSlider;
        GameObject.Find("_app").GetComponent<GameSettings>().verticalMouseSensitivity = vertSensSlider;
        GameObject.Find("_app").GetComponent<GameSettings>().UpdatePlayerSettings();
    }
}
