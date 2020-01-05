using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour {

	Unit selectedUnit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		


        //glowna kamera

		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

		RaycastHit hitInfo;

        //zwraca true false jesli cos dotkniemy (promien zostaje wystrzelony z kamery,ktory rejestruje obiekty)
		if( Physics.Raycast(ray, out hitInfo) ) {
			GameObject ourHitObject = hitInfo.collider.transform.parent.gameObject;

            //zwraca informacje na ktory obiekt najechalismy
			if(ourHitObject.GetComponent<Hex>() != null) 
            {
				MouseOver_Hex(ourHitObject);
			}
			else if (ourHitObject.GetComponent<Unit>() != null) {
				
				MouseOver_Unit(ourHitObject);

			}


		}

		

	}

    //mysz click
	void MouseOver_Hex(GameObject ourHitObject) {
		Debug.Log("Raycast hit: " + ourHitObject.name );

		//co robimy po kliknięciu

		if(Input.GetMouseButtonDown(0)) {

			//Kolorowanie hexa
			MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();

			if(mr.material.color == Color.red) {
				mr.material.color = Color.white;
			}
			else {
				mr.material.color = Color.red; 	
			}

			

			


		}

	}

    
    
    
    
    
    
    
    
    
    //po najechaniu myszką
	void MouseOver_Unit(GameObject ourHitObject) {
		Debug.Log("Raycast hit: " + ourHitObject.name );

		if(Input.GetMouseButtonDown(0)) {
			// We have click on the unit
			selectedUnit = ourHitObject.GetComponent<Unit>();
		}

	}
}
