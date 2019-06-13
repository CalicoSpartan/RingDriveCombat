﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour {
    public float BlastRadius = 20f;
    public Material lavaMat;
    public LayerMask lavaLayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision _col)
    {
        if (_col.collider.gameObject.tag != "Player")
        {
            Vector3 conPoint = _col.contacts[0].point;
            Collider[] colliders = Physics.OverlapSphere(conPoint, BlastRadius);
            if (colliders.Length >= 1)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    Collider col = colliders[i];
                    if (col.gameObject.tag == "Ground")
                    {
                        col.gameObject.GetComponent<MeshRenderer>().material = lavaMat;
                        col.gameObject.layer = 12;// lavaLayer.value;
                        //Destroy(col.gameObject);
                        break;
                    }
                }
            }
        }
        else
        {
            
        }
        Destroy(this.gameObject);
        
    }
}
