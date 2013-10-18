using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Villager : MonoBehaviour 
{
	public static int instances = 0;
	private int _id = 0;
	
	public float stress;
	public float houseRadius = 8;
	
	public LayerMask houseLayer;
	public LayerMask cityHallLayer;
    public List<CityHall> cityHalls;
	
	
	public bool cityHallMode = true;
	public void Awake()
	{
		_id = instances;
		instances++;
		GameObject[] cityHallsObj = GameObject.FindGameObjectsWithTag("CityHall");
        for (int i = 0; i < cityHallsObj.Length; ++i)
        {
            cityHalls.Add( cityHallsObj[i].GetComponent<CityHall>() );
        }
	}

    public void onHitted()
	{

        Collider[] houses = Physics.OverlapSphere(transform.position, houseRadius, houseLayer);
        for (int i = 0; i < houses.Length; ++i)
        {
            House house = houses[i].gameObject.GetComponent<House>();
            house.addStressByDistance(Vector3.Distance(transform.position, house.transform.position));
        }
        active = false;
        GameObject.Destroy(this.gameObject);
	}
	
}
