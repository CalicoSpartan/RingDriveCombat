using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform carTransform;
    public Transform lowTransform;
    public Transform highTransform;
    public float lowLookThreshold = .4f;
    public float highLookThreshold = -.4f;
    public float carLerpSpeed = 10f;
    public float carSlerpSpeed = 10f;
    Vector3 basePosition;
	// Use this for initialization
	void Start () {
        basePosition = carTransform.localPosition;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        if (carTransform)
        {
            if (transform.rotation.x > 0)
            {
                float percent = transform.rotation.x / lowLookThreshold;
                carTransform.localPosition = Vector3.Lerp(basePosition, highTransform.localPosition, percent);
            }
            else
            {
                float percent = transform.rotation.x / highLookThreshold;
                carTransform.localPosition = Vector3.Lerp(basePosition, lowTransform.localPosition, percent);
            }
            transform.position = Vector3.Lerp(transform.position, carTransform.position, Time.deltaTime * carLerpSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(carTransform.rotation.eulerAngles.x, carTransform.rotation.eulerAngles.y, 0f), Time.deltaTime * carSlerpSpeed);
        }


	}
}
