using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacesPlayer : MonoBehaviour
{
    public float rotSpeed = 90f; // szybkosc z jaka obraca się przeciwnik

    Transform player; // zmienna sluzaca do sledzenia gracza
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            //Znajdz statek gracza
            GameObject go = GameObject.Find("PlayerShip");

            if (go != null)
            {
                player = go.transform;
            }
        }
            //W tym momencie znaleźlismy gracza
            // albo nie istnieje w tym momencie
        if (player == null)
        {
            return; // sprobuj ponownie znalezc 
        }

        //tutaj wiemy na pewno, że znalezliśmy gracza. odwróć się twarzą do niego

        Vector3 dir = player.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        Quaternion desiredRot = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, rotSpeed * Time.deltaTime);//funkcja ktora obraca z naszego bieżącego obrotu na nasz pożądany obrót nastepnie ustalamy o ile stopni ma sie obracać która zalezy od naszej zmiennej RotSpeed
        
    }
}
