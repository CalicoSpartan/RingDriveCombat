using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float startingHealth = 100f;
    private float currentHealth = 1f;
    public float maxThrowForce = 100f;
    public float minThrowForce = 3f;
    public float horizontalAngleThreshold = 45f;
    public float verticalAngleThreshold = 30f;
    public float maxHorizontalTurnSpeed = 7f;
    public float minHorizontalTurnSpeed = 4f;
    public float maxVerticalTurnSpeed = 7f;
    public float minVerticalTurnSpeed = 4f;
    public float horizontalTurnSpeed = 0f;
    public float verticalTurnSpeed = 0f;
    public GameObject bombPrefab;
    public Transform lookCam;
    public Transform muzzlePoint;
    float verticalValue = 0f;
    bool bActive = false;
    bool dead = false;
    int turnRight = 1;
    int lookUp = 1;
    

	// Use this for initialization
	void Start () {
        currentHealth = startingHealth;
        horizontalTurnSpeed = Random.Range(minHorizontalTurnSpeed, maxHorizontalTurnSpeed);
        verticalTurnSpeed = Random.Range(minVerticalTurnSpeed, maxVerticalTurnSpeed);
        //StartCoroutine(horizontalTurnCoroutine(lookCam.localEulerAngles.y, horizontalAngleThreshold, horizontalTurnSpeed));
        StartCoroutine(verticalTurnCoroutine(lookCam.localEulerAngles.x, verticalAngleThreshold, verticalTurnSpeed));
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp("Fire1"))
        {
            LaunchBomb();
        }
        //Debug.Log(lookCam.localRotation);
        
	}



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyTrigger")
        {
            bActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemyTrigger")
        {
            bActive = false;
        }
    }

    IEnumerator horizontalTurnCoroutine(float initialHorizontalValue,float finalHorizontalValue,float time)
    {

        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            float val = Mathf.Lerp(initialHorizontalValue, finalHorizontalValue, i);
            //Debug.Log(val);
            lookCam.localEulerAngles = new Vector3(lookCam.localEulerAngles.x, val, lookCam.localEulerAngles.z);
            
            yield return null;
        }
        turnRight *= -1;
        StartCoroutine(horizontalTurnCoroutine(turnRight * -1 * horizontalAngleThreshold,turnRight * horizontalAngleThreshold,horizontalTurnSpeed));

    }

    IEnumerator verticalTurnCoroutine(float initialVerticalValue, float finalVerticalValue, float time)
    {

        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            verticalValue = Mathf.Lerp(initialVerticalValue, finalVerticalValue, i);
            //Debug.Log(val);
            lookCam.localEulerAngles = new Vector3(verticalValue,lookCam.localEulerAngles.y, lookCam.localEulerAngles.z);

            yield return null;
        }
        lookUp *= -1;
        StartCoroutine(verticalTurnCoroutine(lookUp * -1 * verticalAngleThreshold, lookUp * verticalAngleThreshold, verticalTurnSpeed));

    }


    void LaunchBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, lookCam.position + lookCam.forward * 12f, Quaternion.identity);
        Rigidbody bombRB = bomb.GetComponent<Rigidbody>();
        float power = 0f;
        power = Mathf.Abs(((verticalValue + verticalAngleThreshold) / (verticalAngleThreshold + verticalAngleThreshold)));
        float finalPower = Mathf.Lerp(minThrowForce, maxThrowForce, power);
        bombRB.AddForce(lookCam.forward * finalPower, ForceMode.Impulse);
    }
}
