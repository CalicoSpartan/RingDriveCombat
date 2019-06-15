using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour {

    public int totalAmmo = 100;
    public int magazineSize = 27;
    public float reloadTime = 2f;
    public int shotsPerBurst = 3;
    public float burstDelay = 1.2f;
    public float shotDelay = .1f;
    public float damagePerBullet = 34f;
    public bool bReloading = false;
    public bool bFiring = false;
    public Transform muzzlePoint;
    public PlayerController player;
    public CameraController cam;
    public ParticleSystem muzzleFlash;
    public GameObject bulletPrefab;
    int ammoInMag;
    bool bCanShoot = true;
	// Use this for initialization
	void Start () {
        ammoInMag = magazineSize;
        totalAmmo -= magazineSize;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shoot()
    {
        if (bFiring == false && bCanShoot == true)
        {
            StartCoroutine(Shooty());
        }
        


        
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(burstDelay);
        bCanShoot = true;
    }

    IEnumerator Shooty()
    {
        List<GameObject> hitObjects = new List<GameObject>();
        if (bReloading)
        {
            Debug.Log("RELOADING STILL");
            yield break;
        }
        bFiring = true;
        bCanShoot = false;
        int shotsToFire = shotsPerBurst;
        if (ammoInMag < magazineSize)
        {
            shotsToFire = ammoInMag;
        }
        for (int i = 0; i < shotsToFire; i++)
        {
            Vector3 shotDestination = Vector3.zero;
            RaycastHit hit;
            muzzleFlash.Play();
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2000f))
            {
                shotDestination = hit.point;
                hitObjects.Add(hit.collider.gameObject);
                
            }
            else
            {

                shotDestination = cam.transform.position + cam.transform.forward * 2000f;
                hitObjects.Add(null);
                

            }
            SpawnBullet(muzzlePoint.position, shotDestination, .2f);
            yield return new WaitForSeconds(shotDelay);
        }
        bFiring = false;
        StartCoroutine(FireDelay());
        player.GunFeedback(hitObjects);
    }

    void SpawnBullet(Vector3 start, Vector3 end, float time)
    {
        GameObject bulletObject = Instantiate(bulletPrefab, start, Quaternion.identity);
        BulletSimulator bulletComp = bulletObject.GetComponent<BulletSimulator>();
        bulletComp.SetValues(start, end, time);
        
    }

    IEnumerator Reload()
    {
        if (totalAmmo <= 0)
        {
            
            Debug.Log("Can't reload, out of ammo");
            yield break;
        }
        if (bReloading)
        {
            Debug.Log("Already reloading");
            yield break;
        }
        bReloading = true;
        yield return new WaitForSeconds(reloadTime);
        if (totalAmmo > magazineSize)
        {
            totalAmmo -= magazineSize;
            totalAmmo += ammoInMag;
            ammoInMag = magazineSize;
        }
        else
        {
            if (totalAmmo + ammoInMag > magazineSize)
            {
                totalAmmo -= (magazineSize - ammoInMag);
                ammoInMag = magazineSize;
            }
            else
            {
                ammoInMag += totalAmmo;
                totalAmmo = 0;
            }
        }
        bReloading = false;
    }
}
