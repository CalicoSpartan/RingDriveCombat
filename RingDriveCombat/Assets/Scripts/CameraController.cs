using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform tpcTransform;
    public Transform carTransform;
    public bool bDriving = true;
    public float thirdPersonLerpSpeed = 10f;
    public float thirdPersonSlerpSpeed = 10f;
    public float carLerpSpeed = 10f;
    public float carSlerpSpeed = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (bDriving)
        {
            if (carTransform)
            {
                transform.position = Vector3.Lerp(transform.position, carTransform.position, Time.deltaTime * carLerpSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(carTransform.rotation.eulerAngles.x, carTransform.rotation.eulerAngles.y, 0f), Time.deltaTime * carSlerpSpeed);
            }
        }
        else
        {
            if (tpcTransform)
            {
                transform.position = Vector3.Lerp(transform.position, tpcTransform.position, Time.deltaTime * thirdPersonLerpSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, tpcTransform.rotation, Time.deltaTime * thirdPersonSlerpSpeed);

            }
        }
	}
}
