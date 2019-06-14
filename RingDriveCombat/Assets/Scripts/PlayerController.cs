using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    public ArcadeCarController car;
    public CameraController cam;
    public Transform tpcTransform;
    public Transform gunTransform;
    public Transform carCameraTransform;
    public GunManager gun;
    public float horizontalLookSpeed = 1f;
    public float verticalLookSpeed = 1f;
    public float cameraTransitionTime = 1f;
    float m_horizontalInput = 0f;
    float m_verticalInput = 0f;
    bool bDriving = true;
    List<GameObject> hitGameObjects;
	void Start () {
        
        hitGameObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Jump"))
        {
            if (car.bDriving)
            {
                car.bDriving = false;
                bDriving = false;
                cam.bDriving = false;
                //StartCoroutine(LerpCamera(tpcTransform.position));
            }
            else
            {
                bDriving = true;
                car.bDriving = true;
                cam.bDriving = true;
                //StartCoroutine(LerpCamera(carCameraTransform.position));
            }
            
            
            
        }
		if (bDriving == false)
        {
            GetInput();
            Move();
            
        }
	}

    public void Move()
    {
        transform.Rotate(0f, m_horizontalInput, 0f, Space.Self);
        gunTransform.Rotate(0f, 0f, m_verticalInput, Space.Self);
        tpcTransform.Rotate(-m_verticalInput, 0f, 0f, Space.Self);

    }
    /*
    IEnumerator LerpCamera(Vector3 finalDestination)
    {
        float i = 0.0f;
        float rate = 1.0f / cameraTransitionTime;
        Vector3 initPosition = cam.transform.position;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            cam.transform.position = Vector3.Lerp(initPosition, finalDestination, i);

            yield return null;
        }
        Debug.Log("Finished");
        if (!car.bDriving)
        {
            bDriving = true;
        }
    }
    */

    void GetInput()
    {
        m_horizontalInput = horizontalLookSpeed * Input.GetAxis("Mouse X");
        m_verticalInput = verticalLookSpeed * Input.GetAxis("Mouse Y");
        if (Input.GetButtonUp("Fire1"))
        {
            gun.Shoot();

        }
    }

    public void GunFeedback(List<GameObject> objects)
    {
        hitGameObjects = objects;
        Debug.Log("Received feedback");
        hitGameObjects.Clear();
    }

}
