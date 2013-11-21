using UnityEngine;
using System.Collections;

public class TouchEvent : MonoBehaviour {

    public GUIText gui;
    public string textes = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.touches.Length > 0)
        {
            textes = Input.GetTouch(0).phase.ToString();

            
        }

        gui.text = textes;
	}
}
