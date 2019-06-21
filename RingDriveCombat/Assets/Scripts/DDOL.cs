using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DDOL : MonoBehaviour {

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //load logo screens
        SceneManager.LoadScene("StartMenu");

    }

    public void StartGame()
    {
        
        SceneManager.LoadScene("MainLevel");
    }
}
