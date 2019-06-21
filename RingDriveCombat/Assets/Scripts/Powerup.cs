using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    // Use this for initialization
    public PlayerController player;
    public LayerMask layers;
    public Material groundMat;
    public int uses = 2;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Use()
    {
        if (player)
        {
            if (uses > 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(player.cam.transform.position, player.cam.transform.forward, out hit, 2000f, layers))
                {
                   
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Lava"))
                    {
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = groundMat;
                        hit.collider.gameObject.layer = 9;
                    }
                }
                uses -= 1;
            }
        }
    }
}
