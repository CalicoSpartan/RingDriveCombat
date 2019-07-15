using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderLabel;
    public TextMeshProUGUI startAnnouncement;
    bool isLoading = false;
    bool finishedLoading = false;
    AsyncOperation operation;

    public void Start()
    {
        LoadLevel(4);
    }

    public void LoadLevel(int sceneIndex)
    {
        if (!isLoading)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
            isLoading = true;
        }
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        yield return null;
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {



            sliderLabel.text = (operation.progress * 100) + 10 + "%";
            slider.value = operation.progress + .1f;
            if (operation.progress >= .9f)
            {
                startAnnouncement.text = "Press SPACE to start.";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }




}
