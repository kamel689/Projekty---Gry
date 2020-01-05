using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float rotSpeed = 180f;

    float shipBoundaryRadius = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Zwraca liczbę zmiennoprzecinkowoą z -1.0 na 1.0 
        //jeśli wartosc bedzie ujemna to idziemy w dół
        //jeśli dodatnia to idziemy w górę
        
     //Obróć w lewo lub prawo

        Quaternion rot = transform.rotation; //Uzycie rotracji
        float z = rot.eulerAngles.z; //kat Eulera
        z -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;//zmiana kątu Z na podstawie danych wejsciowych
        rot = Quaternion.Euler( 0, 0, z);//Odtworzenie kwaterionu
        transform.rotation = rot;//Wprowadz kwaternionu do naszej rotacji
        
        //Przeniesc w górę lub w dół
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3 (0,Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime,0); //x,y,z
        pos += rot * velocity;
        

        //Ograniczenie lotu z góry
        if (pos.y + shipBoundaryRadius > Camera.main.orthographicSize)
        {
            pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
        }
        //Ograniczenie lotu z dołu
        if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize)
        {
            pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
        }

        //Obliczamy szerokość ortograficzna na podstawie proporcji ekranu
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float width0rtho = Camera.main.orthographicSize * screenRatio;

        //ograniczenie lotu w prawo
        /*if (pos.x + shipBoundaryRadius > width0rtho)
        {
            pos.x = width0rtho - shipBoundaryRadius;
        }

        //Ograniczenie lotu w lewo
        if (pos.x - shipBoundaryRadius < -width0rtho)
        {
            pos.x = -width0rtho + shipBoundaryRadius;
        }
        */
        //aktualizowanie pozycji
        transform.position = pos;
    }
}
