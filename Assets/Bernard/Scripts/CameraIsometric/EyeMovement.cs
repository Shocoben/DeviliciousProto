using UnityEngine;
using System.Collections;

public class EyeMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
   
        /////////////////////
        //keyboard scrolling
       
        float translationX = Input.GetAxis("Horizontal");
        float translationY = Input.GetAxis("Vertical");
        float fastTranslationX = 2 * Input.GetAxis("Horizontal");
        float fastTranslationY = 2 * Input.GetAxis("Vertical");
       
        if (Input.GetKey(KeyCode.LeftShift))
            {
            transform.Translate(fastTranslationX + fastTranslationY, 0, fastTranslationY - fastTranslationX);
            }
        else
            {
            transform.Translate(translationX + translationY, 0, translationY - translationX);
            }
     
        ////////////////////
        //mouse scrolling
       
        var mousePosX = Input.mousePosition.x;
        var mousePosY = Input.mousePosition.y;
        int scrollDistance = 5;
        float scrollSpeed = 70;
     
        //Horizontal camera movement
        if (mousePosX < scrollDistance)
            //horizontal, left
            {
            transform.Translate(-1, 0, 1);
            }
        if (mousePosX >= Screen.width - scrollDistance)
            //horizontal, right
            {
            transform.Translate(1, 0, -1);
            }
     
        //Vertical camera movement
        if (mousePosY < scrollDistance)
            //scrolling down
            {
            transform.Translate(-1, 0, -1);
            }
        if (mousePosY >= Screen.height - scrollDistance)
            //scrolling up
            {
            transform.Translate(1, 0, 1);
            }
       
        ////////////////////
        //zooming
        GameObject Eye = GameObject.Find("Eye");
       
        //
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Eye.camera.orthographicSize > 4)
            {
            Eye.camera.orthographicSize = Eye.camera.orthographicSize - 4;
            }
       
        //
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Eye.camera.orthographicSize < 80)
            {
            Eye.camera.orthographicSize = Eye.camera.orthographicSize + 4;
            }
     
        //default zoom
        if (Input.GetKeyDown(KeyCode.Mouse2))
            {
            Eye.camera.orthographicSize = 10;
            }
           
    }
}
