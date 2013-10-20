using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningManager : MonoBehaviour {

	public static LightningManager instance;
	public GameObject lightningPrefab;
	public List<GameObject> objectsDead = new List<GameObject>();
	public List<GameObject> objectsAvailable = new List<GameObject>();
	public List<GameObject> objectsAlive = new List<GameObject>();
	
	public int nbStartObjects = 3;
	
	void Awake () {
		
		instance = this;
		
		GameObject clone;
		
		for (int i=0;i<nbStartObjects;i++) {
			
			clone = Instantiate(lightningPrefab, transform.position, transform.rotation) as GameObject;
			clone.transform.parent = gameObject.transform;
			clone.SetActive(false);
			objectsAvailable.Add(clone);
		}
		
	}
	
	public void Recycle (int index) {
		
		Debug.Log("Recycle index :"+index);
		
		var obj = objectsAlive[index];
		
		objectsAlive.RemoveAt(index);
		
		obj.SetActive(false);
		
		objectsAvailable.Add(obj);
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update() {
	
		MouseClickDetected ();
		
	}
	
	// Check if the player click
	void MouseClickDetected () {
		
		if (Input.GetMouseButtonDown(0) ) {
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100))
			{
				Debug.DrawLine(ray.origin, hit.point);
				
				// if there is at least one object available
				if ( objectsAvailable.Count > 0 ) {
					Debug.Log("objects lightning available"+objectsAvailable.Count);
					var obj = objectsAvailable[0];
					objectsAlive.Add(obj);
					objectsAvailable.RemoveAt(0);
					obj.SetActive(true);
					obj.GetComponent<Lightning>().Execute(hit.point);
					//obj.Execute(hit.point);
					
					
				} else {
					Debug.Log("objects lightning available"+objectsAvailable.Count);
					Debug.Log("none object available. Instantiate a new object");
					GameObject obj = Instantiate(lightningPrefab, transform.position, transform.rotation) as GameObject;
					obj.transform.parent = gameObject.transform;
					objectsAlive.Add(obj);
					obj.SetActive(true);
					obj.GetComponent<Lightning>().Execute(hit.point);
					//obj.Execute(hit.point);
				}
				
			}
			
			
			
			
		}
		
	}
	
}
