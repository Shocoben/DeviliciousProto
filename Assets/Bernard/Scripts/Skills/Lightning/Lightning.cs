using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {

	public ParticleSystem particle;
	public float birthTime;
	public float lifeTime = 1;
	public float deathTime = 0;
	public int index;
	
	// Use this for initialization
	void Start () {
		
	    //Execute();
		
	}
	
	// Update is called once per frame
	void Update () {
	    if ( Time.time > deathTime ) {
			//Debug.Log("Time over");
			Reset();
			LightningManager.instance.Recycle(index);
		}
	}
	
	public void Execute (Vector3 target) {
		
		birthTime = Time.time;
		deathTime = Time.time + lifeTime;
		particle.transform.position = target;
		particle.Stop();
		particle.Play();
		
	}
	
	public void Reset () {
		particle.Stop();
	}
}
