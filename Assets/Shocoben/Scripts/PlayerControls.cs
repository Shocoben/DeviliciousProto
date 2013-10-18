using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerControls : MonoBehaviour {

	public LayerMask villagersLayerMask;
	Terrain terrain;
	

	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown(0) )
		{
			Ray camRay= Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(camRay,out hit, 100, villagersLayerMask))
			{
				Villager hitted = hit.collider.gameObject.GetComponent<Villager>();
				hitted.onHitted();
			}
		}
	}
}
