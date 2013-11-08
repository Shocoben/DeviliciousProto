using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerControls : MonoBehaviour {

	// Update is called once per frame
	void Update () 
    {
        if (UICamera.hoveredObject)
        {
            return;
        }
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
        SpellManager.instance.castCSpell(inputPos);

	}
	

}
