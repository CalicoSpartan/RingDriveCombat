using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour {

    // Use this for initialization
    public TextMeshProUGUI waveCounterGUI;
    public TextMeshProUGUI pointCounterGUI;
    public TextMeshProUGUI waveAnouncementGUI;

    public float waveAnnouncmentDuration = 3f;
    
	void Start () {
        StartCoroutine(DisplayWaveNumberCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateStatGUI(int waveCount, int pointCount)
    {
        waveCounterGUI.text = "Wave: " + waveCount.ToString();
        waveAnouncementGUI.text = "Wave " + waveCount.ToString();
        pointCounterGUI.text = "Points: " + pointCount.ToString();
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
