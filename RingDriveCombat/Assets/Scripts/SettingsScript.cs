using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingsScript : MonoBehaviour {
    public float vertSensSlider;
    public float horzSensSlider;
    public Slider horzSlider;
    public Slider vertSlider;
    public bool hasApplied = false;

    public void LoadPreviousScene()
    {

        string prevScene = GameObject.Find("_app").GetComponent<GameData>().previousSceneName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(prevScene);
    }

    public void GoBackFromSettings(float origHorz, float origVert)
    {
        if (!hasApplied)
        {
            vertSensSlider = origVert;
            horzSensSlider = origHorz;
            horzSlider.value = horzSensSlider;
            vertSlider.value = vertSensSlider;
        }
    }

    public void LoadedSettings()
    {
        vertSlider.value = 1f;

    }


    public void SetVertandHorzSens()
    {
        vertSensSlider = vertSlider.value;
        horzSensSlider = horzSlider.value;
    }

    public void SetApplied()
    {
        hasApplied = !hasApplied;
    }

    public void ApplyChanges()
    {
        hasApplied = true;
        Debug.Log(horzSensSlider);
        Debug.Log(vertSensSlider);
        GameObject.Find("_app").GetComponent<GameSettings>().horizontalMouseSensitivity = horzSensSlider;
        GameObject.Find("_app").GetComponent<GameSettings>().verticalMouseSensitivity = vertSensSlider;
        GameObject.Find("_app").GetComponent<GameSettings>().UpdatePlayerSettings();
    }
}
