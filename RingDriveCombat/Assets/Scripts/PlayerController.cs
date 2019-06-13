using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    public ArcadeCarController car;
    float m_horizontalInput = 0f;
    float m_verticalInput = 0f;
    bool bDriving = true;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (bDriving == false)
        {
            GetInput();
        }
	}

    void GetInput()
    {

    }
}
