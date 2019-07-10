using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour {

    // Use this for initialization
    public TextMeshProUGUI waveCounterGUI;
    public TextMeshProUGUI pointCounterGUI;
    public TextMeshProUGUI waveAnouncementGUI;
    public TextMeshProUGUI waveAnouncementPointsGUI;
    public TextMeshProUGUI ammoCounterGUI;
    public Image weaponHighlightSquare;
    public Image jumpCooldownBar;
    public Image jumpCooldownBack;
    public Image gameOverPanel;
    public TextMeshProUGUI finalWaveText;
    public TextMeshProUGUI finalScoreText;
    public Image pauseMenuPanel;
    public Image settingsMenuPanel;
    public Image powerupImage;

    public bool bJumpCooldown = false;
    public float waveAnnouncmentDuration = 3f;

    void Start() {
        jumpCooldownBack.enabled = false;
        jumpCooldownBar.enabled = false;
        gameOverPanel.gameObject.SetActive(false);
        pauseMenuPanel.gameObject.SetActive(false);
        StartCoroutine(DisplayWaveNumberCoroutine());
    }

    // Update is called once per frame
    void Update() {

    }


    public void ShowPauseMenu(bool pause)
    {
        if (pause)
        {
            pauseMenuPanel.gameObject.SetActive(true);
        }
        else
        {
            pauseMenuPanel.gameObject.SetActive(false);
        }
    }

    public void OnPlayAgainClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ShowGameOverGUI(int wave, int points)
    {
        finalWaveText.text = "Final Wave: " + wave.ToString();
        finalScoreText.text = "Score: " + points.ToString();
        gameOverPanel.gameObject.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void GoToSettings()
    {
        pauseMenuPanel.gameObject.SetActive(false);
        settingsMenuPanel.gameObject.SetActive(true);
        GameObject.Find("_app").GetComponent<SettingsScript>().hasApplied = false;
       
    }

    public void GoBackToPauseMenu()
    {
        
        settingsMenuPanel.gameObject.SetActive(false);
        pauseMenuPanel.gameObject.SetActive(true);
        
    }

    public IEnumerator JumpCoolDown(float time)
    {
        
        jumpCooldownBack.enabled = true;
        jumpCooldownBar.enabled = true;
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            while (LevelManager.bPaused)
            {
                yield return null;
            }
            i += Time.deltaTime * rate;
            jumpCooldownBar.GetComponent<RectTransform>().localScale = new Vector3(1f, i, 1f);

            yield return null;
        }
        jumpCooldownBack.enabled = false;
        jumpCooldownBar.enabled = false;

    }
    

    public void UpdateAmmoCounter(int ammo)
    {
        ammoCounterGUI.text = "Ammo: " + ammo.ToString();
        if (ammo <= 3 && ammo != -1)
        {
            ammoCounterGUI.color = Color.red;
        }
        else
        {
            ammoCounterGUI.color = Color.white;
        }
    }

    public void UpdateStatGUI(int waveCount, int pointCount)
    {
        waveCounterGUI.text = "Wave " + waveCount.ToString();
        waveAnouncementGUI.text = "Wave " + waveCount.ToString();
        pointCounterGUI.text = "Points: " + pointCount.ToString();
        waveAnouncementPointsGUI.text = "Points: " + pointCount.ToString();
    }

    public void SwitchWeapons(int choice)
    {
        if (choice == 1)
        {
            weaponHighlightSquare.GetComponent<RectTransform>().anchoredPosition = new Vector2(-5f, -10f);

        }
        else if (choice == 0)
        {
            weaponHighlightSquare.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75f, -10f);
        }
    }

    public void DisplayPowerupImg(bool _display)
    {
        if (_display)
        {
            powerupImage.GetComponent<Image>().enabled = true;
        }
        else
        {
            powerupImage.GetComponent<Image>().enabled = false;
        }
    }

    public void DisplayWaveNumber()
    {
        StartCoroutine(DisplayWaveNumberCoroutine());
    }

    IEnumerator DisplayWaveNumberCoroutine()
    {
        waveAnouncementGUI.enabled = true;
        waveAnouncementPointsGUI.enabled = true;
        yield return new WaitForSeconds(waveAnnouncmentDuration);
        waveAnouncementGUI.enabled = false;
        waveAnouncementPointsGUI.enabled = false;
    }


}
