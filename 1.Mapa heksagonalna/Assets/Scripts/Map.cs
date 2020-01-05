using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public GameObject hexPrefab;

	
	int width = 31;
	int height = 31;

	float xOffset = 0.882f;
	float zOffset = 0.764f;

	
	void Start () {
	
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {

				float xPos = x * xOffset;

				
				if( y % 2 == 1 ) {
					xPos += xOffset/2f;
				}

				GameObject hex_go = (GameObject)Instantiate(hexPrefab, new Vector3( xPos,0, y * zOffset  ), Quaternion.identity  ); // klonujemy nasz obiekt

				// Nadanie nazwy
				hex_go.name = "Hex_" + x + "_" + y;

				// Upewnienie że hex jest prawidołowo umieszczony na mapie
				hex_go.GetComponent<Hex>().x = x;
				hex_go.GetComponent<Hex>().y = y;

				// For a cleaner hierachy, parent this hex to the map
				hex_go.transform.SetParent(this.transform);

				// Ustawienie statycznych flag na wartosc true
				hex_go.isStatic = true;

			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
