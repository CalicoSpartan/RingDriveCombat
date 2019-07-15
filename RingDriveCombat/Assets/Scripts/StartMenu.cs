using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

	// Use this for initialization


    // Update is called once per frame
    void Update () {
		
	}

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("InstructionsScene");
    }

    public void GoToSettings()
    {
        GameObject.Find("_app").GetComponent<GameData>().previousSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameSettings");
    }
}
