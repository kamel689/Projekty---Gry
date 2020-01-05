using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public Transform sphereTransform;

    void Start()
    {
        sphereTransform.parent = transform;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 45, Space.World);
        
    }
}
