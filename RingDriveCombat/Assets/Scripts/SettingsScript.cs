using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour {

    public void LoadPreviousScene()
    {
        string prevScene = GameObject.Find("_app").GetComponent<GameData>().previousSceneName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(prevScene);
    }
}
