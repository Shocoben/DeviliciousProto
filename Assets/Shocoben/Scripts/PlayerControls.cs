using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerControls : MonoBehaviour {

	public LayerMask villagerLayerMask;
    public LayerMask terrainLayerMask;

    //Thunder
    public GameObject thunderImpactPrefab;
    public float houseRadius = 8;
    public float missAttenuation = 1.5f;
    public LayerMask houseLayer;


	// Update is called once per frame
	void Update () 
    {
	  #if UNITY_STANDALONE 
		mouseInput();
	  #else
		touchInput();
	  #endif
	}
	
	
	void touchInput()
	{
		if (Input.touches.Length > 0)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				onCastSpell(touch.position);	
			}
		}
	}
	
	void mouseInput()
	{
		if (Input.GetMouseButtonDown(0) )
		{
			onCastSpell(Input.mousePosition);
		}
	}
	
	public void onCastSpell(Vector3 inputPos)
	{
		Ray camRay= Camera.main.ScreenPointToRay(inputPos);
		RaycastHit hit;
        if (Statue.activeStatues["antithunder"].Count > 0)
            return;

		if ( Physics.Raycast(camRay,out hit, 100, villagerLayerMask.value | terrainLayerMask.value ) )
		{
            if (hit.transform.CompareTag("Villager"))
            {
                Villager hitted = hit.collider.gameObject.GetComponent<Villager>();
                hitted.onHitted(hit.point);
            }
            else
            {
                Vector3 impactPos = hit.point;
                impactPos.y += 0.2f;
                Quaternion impactRot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                GameObject.Instantiate(thunderImpactPrefab, impactPos, impactRot);
            }

            impactStress(hit.point, ( hit.transform.tag == "Villager" )? 1 : missAttenuation);
		}
	}
	
    public float panicDistance = 3;
    public void impactStress(Vector3 impactPos, float attenuation = 1)
    {
        Collider[] houses = Physics.OverlapSphere(impactPos, houseRadius, houseLayer);
        
        for (int i = 0; i < houses.Length; ++i)
        {
            House house = houses[i].gameObject.GetComponent<House>();
            house.addStressByDistance(Vector3.Distance(impactPos, house.transform.position) / attenuation);
        }

        Collider[] villagers = Physics.OverlapSphere(impactPos, panicDistance, villagerLayerMask);
        Villager cVillager = null;
        Vector3 cPanicDirection = new Vector3();
        for (int i = 0; i < villagers.Length; ++i)
        {
            cVillager = villagers[i].GetComponent<Villager>();
            cPanicDirection = villagers[i].transform.position - impactPos;
            cVillager.panicDirection = new Vector3(cPanicDirection.x, 0, cPanicDirection.z);
            cVillager.state = Villager.States.panic;
        }
    }
}
