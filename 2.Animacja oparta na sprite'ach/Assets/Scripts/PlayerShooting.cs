using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Vector3 bulletOffset = new Vector3(0, 0.5f, 0); // przesuniecie pocisku
    public GameObject bulletPrefab;

    public float fireDelay = 0.25f;
    float cooldownTimer = 0; //czas odnowienia

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetButton("Fire1") && cooldownTimer <= 0)
        {
            Debug.Log("Fire!");
            cooldownTimer = fireDelay;
            Vector3 offset = transform.rotation * bulletOffset; // dokonanie przesuniecia pocisku

            Instantiate(bulletPrefab, transform.position + offset, transform.rotation); //tworzymy kopie
        }
    }
}
