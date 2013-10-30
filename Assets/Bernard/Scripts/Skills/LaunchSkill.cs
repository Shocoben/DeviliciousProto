using UnityEngine;
using System.Collections;

public class LaunchSkill : MonoBehaviour {

    public ParticleSystem lightning;
    public ParticleSystem lightning2;
    private Vector3 pos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("mouse" + Input.mousePosition);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point);
            //Instantiate(impact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal), 0);

            //Debug.Log("mouse" + hit.point);

            pos = hit.point;
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            /*
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.position = pos;
            
            ParticleSystem clone;
            clone = Instantiate(lightning, pos, Quaternion.identity) as ParticleSystem;
            clone.Play();
			*/
			lightning2.transform.position = pos;
			lightning2.Stop();
			lightning2.Play();
			
			
		}

        /*
        if (collider.Raycast(ray,out hit, 1000.0f))
        {
	        Debug.DrawLine (ray.origin, hit.point);
	    }
        */
    }
}
