using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float speed;

    void Update()
    {
        transform.LookAt(target);
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
