using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float startingHealth;
    private float currentHealth;
    public float maxThrowForce;
    public float minThrowForce;
    public float horizontalAngleThreshold = 45f;
    public float verticalAngleThreshold = 30f;
    public float maxHorizontalTurnTime = 7f;
    public float minHorizontalTurnTime = 4f;
    public float maxVerticalTurnTime = 7f;
    public float minVerticalTurnTime = 4f;
    public float horizontalTurnTime = 0f;
    public float verticalTurnTime = 0f;
    public GameObject bombPrefab;
    public Transform lookCam;
    public Transform muzzlePoint;
    public float shotFrequency;
    float verticalValue = 0f;
    bool bActive = false;
    bool dead = false;
    bool bCanShoot = true;
    public float shootDelay = 0.8f;
    int turnRight = 1;
    int lookUp = 1;
    Material myMat;
    Color initColor = new Color(1f,1f,1f,1f);
    Color finalColor = new Color(0.75f, 0f, 0f, 1f);
    Color disabledColor = new Color(.8f, 1f, .8f, 1f);
    

	// Use this for initialization
	void Start () {
        currentHealth = startingHealth;
        myMat = GetComponent<Renderer>().material;
        myMat.color = disabledColor;
        horizontalTurnTime = Random.Range(minHorizontalTurnTime, maxHorizontalTurnTime);
        verticalTurnTime = Random.Range(minVerticalTurnTime, maxVerticalTurnTime);
        StartCoroutine(horizontalTurnCoroutine(lookCam.localEulerAngles.y, horizontalAngleThreshold, horizontalTurnTime));
        StartCoroutine(verticalTurnCoroutine(lookCam.localEulerAngles.x, verticalAngleThreshold, verticalTurnTime));
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp("DebugButton"))
        {
            LaunchBomb();
        }
        if (bActive)
        {
            if (bCanShoot)
            {
                int random = Random.Range(1, 1000);
                if (shotFrequency > random)
                {
                    LaunchBomb();
                }
            }
            
        }
        //Debug.Log(lookCam.localRotation);
        
	}



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyTrigger")
        {
            bActive = true;
            myMat.color = Color.Lerp(initColor, finalColor, 1f - currentHealth / startingHealth);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemyTrigger")
        {
            bActive = false;
            myMat.color = disabledColor;
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
        StartCoroutine(horizontalTurnCoroutine(turnRight * -1 * horizontalAngleThreshold,turnRight * horizontalAngleThreshold,horizontalTurnTime));

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
        StartCoroutine(verticalTurnCoroutine(lookUp * -1 * verticalAngleThreshold, lookUp * verticalAngleThreshold, verticalTurnTime));

    }


    void LaunchBomb()
    {
        bCanShoot = false;
        GameObject bomb = Instantiate(bombPrefab, lookCam.position + lookCam.forward * 12f, Quaternion.identity);
        Rigidbody bombRB = bomb.GetComponent<Rigidbody>();
        float power = 0f;
        power = Mathf.Abs(((verticalValue + verticalAngleThreshold) / (verticalAngleThreshold + verticalAngleThreshold)));
        float finalPower = Mathf.Lerp(minThrowForce, maxThrowForce, power);
        bombRB.AddForce(lookCam.forward * finalPower, ForceMode.Impulse);
        
        StartCoroutine(shootDelayTimer());
    }

    IEnumerator shootDelayTimer()
    {
        yield return new WaitForSeconds(shootDelay);
        bCanShoot = true;
    }

    public void TakeDamage(float Damage)
    {
        if (bActive)
        {
            currentHealth -= Damage;
            Debug.Log("Took Damage, health: " + currentHealth);
            myMat.color = Color.Lerp(initColor, finalColor, 1f - currentHealth / startingHealth);
            if (currentHealth <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
