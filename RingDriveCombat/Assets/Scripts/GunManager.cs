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
    public bool bReloading = false;
    public bool bFiring = false;
    public Transform muzzlePoint;
    public PlayerController player;
    public CameraController cam;
    public ParticleSystem muzzleFlash;
    int ammoInMag;
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

        StartCoroutine(Shooty());


        
    }

    IEnumerator Shooty()
    {
        List<GameObject> hitObjects = new List<GameObject>();
        if (bReloading)
        {
            Debug.Log("RELOADING STILL");
            yield break;
        }
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
                Debug.DrawLine(muzzlePoint.position, shotDestination, Color.cyan,.5f);
            }
            else
            {

                shotDestination = cam.transform.position + cam.transform.forward * 2000f;
                hitObjects.Add(null);
                Debug.DrawLine(muzzlePoint.position, shotDestination, Color.cyan, .5f);

            }
            yield return new WaitForSeconds(shotDelay);
        }

        player.GunFeedback(hitObjects);
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
