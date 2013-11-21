using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerControls : MonoBehaviour {

    private bool fingerMoving = false;
    private float lastTouch;
    private float longTouch = 0.100f;

    public GUIText guiText;

    public ControlCamera controlCamera;


	// Update is called once per frame
	void Update () 
    {
		if (PlayerManager.cState == PlayerManager.GameStates.gameOver)
			return;
        if (UICamera.hoveredObject)
        {
            return;
        }

        #if UNITY_EDITOR
            mouseInput();
        #endif

        #if UNITY_IPHONE || UNITY_ANDROID
              touchInput();
        #endif

    }
	
	
	void touchInput()
    {
        if (Input.touches.Length > 0)
        {
            // 
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                lastTouch = Time.time;
            }

            if ((Time.time - lastTouch) < longTouch)
            {
                //guiText.text = "touch " + (Time.time - lastTouch);

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    onCastSpell(Input.GetTouch(0).position);
                }
            }
        }




        /*
        // The player hit the screen
        if (Input.touches.Length > 0)
        {
            // The player moved his finger on the screen. He tries to move the camera
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                lastTouch = Time.time;
            }

            if ((Time.time - lastTouch) < longTouch)
            {
                guiText.text = "touch " + (Time.time - lastTouch);


                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    onCastSpell(Input.GetTouch(0).position);
                }

            }
            else
            {
                guiText.text = "long touch " + (Time.time - lastTouch);
                controlCamera.TouchControls();
                
            }

        }*/
         
         
        /*
        // The player hit the screen
        if (Input.touches.Length > 0)
        {
            // The player moved his finger on the screen. He tries to move the camera
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                fingerMoving = true;
            }

            // User lift his finger
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                // The player lift his finger without moved it. He tried to launch a spell
                if (Input.GetTouch(0).phase == TouchPhase.Ended && fingerMoving == false)
                {
                    onCastSpell(Input.GetTouch(0).position);

                }

                fingerMoving = false;
            }

        }
       */
        /*
        // The player hit the screen
        if (Input.touches.Length > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                onCastSpell(Input.GetTouch(0).position);

            }
        }
        */

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
