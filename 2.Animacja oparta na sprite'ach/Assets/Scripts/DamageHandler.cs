using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    int health = 1;

    public float invulnPeriod = 0;
    
    int correctLayer;

    void Start()
    {
        correctLayer = gameObject.layer; 
    }

    void OnTriggerEnter2D()
    {
        Debug.Log("Trigger!");

        
            health--;
            gameObject.layer = 10;

      

       }
    void Update()
    {
        
        if (health <= 0)
        {
            Die();
        }
    }
        void Die()
        {
            Destroy(gameObject);
        }
    }

