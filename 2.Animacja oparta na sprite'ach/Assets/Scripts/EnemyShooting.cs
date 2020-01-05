using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    
    public Vector3 bulletOffset = new Vector3(0, 0.5f, 0); // przesuniecie pocisku
    public GameObject bulletPrefab;

    public float fireDelay = 5;
    float cooldownTimer = 1; //czas odnowienia

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            Debug.Log("Fire!");
            cooldownTimer = fireDelay;
            Vector3 offset = transform.rotation * bulletOffset; // dokonanie przesuniecia pocisku

            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position + offset, transform.rotation); //tworzymy kopie
            bulletGO.layer = gameObject.layer; // ustrawiamy wastwe aby przeciwnik nie zabijal sie swoimi pociskami
        }
    }
}

