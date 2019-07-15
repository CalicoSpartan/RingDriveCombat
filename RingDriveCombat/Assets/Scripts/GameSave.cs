using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameSave
{
    public float horzSens;
    public float vertSens;
    public float masterVolume;

    public GameSave (GameSettings gameSettings)
    {
        horzSens = gameSettings.horizontalMouseSensitivity;
        vertSens = gameSettings.verticalMouseSensitivity;
        masterVolume = gameSettings.masterVolume;
    }
}
