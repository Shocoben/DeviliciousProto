using UnityEngine;
using System.Collections;

public class SkillManager : MonoBehaviour {
	
	public GameObject[] ListOfSkills;
	
	void Awake () {
		GameObject clone;
		for (int i=0;i<ListOfSkills.Length;i++) {
			clone = Instantiate(ListOfSkills[i], transform.position, transform.rotation) as GameObject;
			clone.transform.parent = gameObject.transform;
		}
		
	}
	
}