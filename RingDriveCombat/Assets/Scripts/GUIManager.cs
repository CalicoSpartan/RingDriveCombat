using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour {

    // Use this for initialization
    public TextMeshProUGUI waveCounterGUI;
    public TextMeshProUGUI pointCounterGUI;
    public TextMeshProUGUI waveAnouncementGUI;
    public TextMeshProUGUI ammoCounterGUI;
    public Image weaponHighlightSquare;
    public Image jumpCooldownBar;
    public Image jumpCooldownBack;
    
    public bool bJumpCooldown = false;

    public float waveAnnouncmentDuration = 3f;
    
	void Start () {
        jumpCooldownBack.enabled = false;
        jumpCooldownBar.enabled = false;
        StartCoroutine(DisplayWaveNumberCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator JumpCoolDown(float time)
    {
        
        jumpCooldownBack.enabled = true;
        jumpCooldownBar.enabled = true;
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
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
    }

    public void UpdateStatGUI(int waveCount, int pointCount)
    {
        waveCounterGUI.text = "Wave: " + waveCount.ToString();
        waveAnouncementGUI.text = "Wave " + waveCount.ToString();
        pointCounterGUI.text = "Points: " + pointCount.ToString();
    }

    public void SwitchWeapons(int choice)
    {
        if (choice == 1)
        {
            weaponHighlightSquare.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2.5f, -2.5f);

        }
        else if (choice == 0)
        {
            weaponHighlightSquare.GetComponent<RectTransform>().anchoredPosition = new Vector2(-52.5f, -2.5f);
        }
    }

    public void DisplayWaveNumber()
    {
        StartCoroutine(DisplayWaveNumberCoroutine());
    }

    IEnumerator DisplayWaveNumberCoroutine()
    {
        waveAnouncementGUI.enabled = true;
        yield return new WaitForSeconds(waveAnnouncmentDuration);
        waveAnouncementGUI.enabled = false;
    }


}
