using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    public ArcadeCarController car;
    public CameraController cam;
    public Transform tpcTransform;
    public Transform gunTransform;
    public GunManager gun;
    public GUIManager guiManager;
    public RingManager ring;
    public float horizontalLookSpeed = 1f;
    public float verticalLookSpeed = 1f;
    public float verticalLookDownThreshold = 84f;
    public float verticalLookUpThreshold = 270f;
    public float cameraTransitionTime = 1f;
    public float jumpCooldownTime = 15f;
    float m_horizontalInput = 0f;
    float m_verticalInput = 0f;
    bool bCanJump = true;
    bool bDriving = true;
    List<GameObject> hitGameObjects;
    public List<Powerup> powerups;
    public bool bPowerupSelected = false;
	void Start () {
        
        hitGameObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {



        GetInput();
        Move();

            
        
	}

    public void Move()
    {
       
        tpcTransform.Rotate(-m_verticalInput, 0f, 0f, Space.Self);
        
        
        gunTransform.Rotate(0f, 0f, m_verticalInput, Space.Self);
        
        transform.Rotate(0f, m_horizontalInput, 0f, Space.Self);
        
        

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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            car.Explode();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            car.Explode();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Powerup"))
        {

            Powerup powerup = other.gameObject.GetComponent<Powerup>();
            if (powerups.Count > 0)
            {
                powerups[0].uses += powerup.uses;
                if (bPowerupSelected)
                {
                    guiManager.UpdateAmmoCounter(powerups[0].uses);
                }
                Destroy(powerup.gameObject);
            }
            else
            {


                other.enabled = false;
                other.gameObject.transform.SetParent(transform);
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                powerup.player = this;
                powerups.Add(powerup);

            }
            
        }
    }

    public void SwitchWeapons()
    {
        if (bPowerupSelected)
        {
            Debug.Log("Switched Weapons");
            bPowerupSelected = false;
            gun.GetComponent<Renderer>().material = gun.myMat;
            guiManager.UpdateAmmoCounter(-1);
            guiManager.SwitchWeapons(0);
        }
        else
        {
            if (powerups.Count > 0)
            {
                Debug.Log("Switched Weapons");
                bPowerupSelected = true;
                gun.GetComponent<Renderer>().material = powerups[0].GetComponent<Renderer>().material;
                guiManager.UpdateAmmoCounter(powerups[0].uses);
                guiManager.SwitchWeapons(1);

            }
        }
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldownTime);
        bCanJump = true;
    }

    void GetInput()
    {
        m_horizontalInput = horizontalLookSpeed * Input.GetAxis("Mouse X");
        m_verticalInput = verticalLookSpeed * Input.GetAxis("Mouse Y");
        if (Input.GetButtonDown("Jump") && (car.touchingGroundRLW && car.touchingGroundRRW) && (bCanJump))
        {
            car.rb.AddForce(Vector3.up * car.jumpForce, ForceMode.Impulse);
            bCanJump = false;
            StartCoroutine(JumpCooldown());
            StartCoroutine(guiManager.JumpCoolDown(jumpCooldownTime));

        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (bPowerupSelected)
            {
                if (powerups.Count > 0)
                {
                    powerups[0].Use();
                    guiManager.UpdateAmmoCounter(powerups[0].uses);
                    if (powerups[0].uses <= 0)
                    {
                        powerups.RemoveAt(0);
                        SwitchWeapons();
                    }
                    
                }
            }
            else
            {
                gun.Shoot();

            }

        }
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            SwitchWeapons();
        }
        if (Input.GetKeyDown(KeyCode.G) && car.touchingGroundRLW)
        {

            ring.goFast = true;
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            ring.goFast = false;
        }
    }

    public void GunFeedback(List<GameObject> objects)
    {
        hitGameObjects = objects;
        for (int i = 0; i < hitGameObjects.Count;i++)
        {
            if (hitGameObjects[i] != null)
            {
                EnemyController enemy = hitGameObjects[i].GetComponent<EnemyController>();
                if (enemy)
                {
                    enemy.TakeDamage(gun.damagePerBullet);
                }
                else
                {
                    BombManager bomb = hitGameObjects[i].GetComponent<BombManager>();
                    if (bomb)
                    {
                        Destroy(bomb.gameObject);
                    }
                }
            }
        }
        hitGameObjects.Clear();
    }

}


