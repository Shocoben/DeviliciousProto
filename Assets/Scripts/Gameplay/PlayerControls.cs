using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerControls : MonoBehaviour {

    private bool fingerMoving = false;
    private float lastInputTime;
    private Vector2 lastInputPosition;
    public float distanceCameraActive = 0.5f;

    public float inputCastSpellTime = 0.2f;
    private bool movingCamera = false;

    public float cameraSpeed = 5;

    public GUIText guiText;
    public float varianceInDistances = 5.0F;
    public float minPinchSpeed = 5.0F;
    public float pinchSpeed = 4;

    private float prevDist = 0;
    private bool alreadyPinched = false;

    public float scrollSpeed = 10;
    public ControlCamera controlCamera;

    public float minFOV = 15;
    public float maxFOV = 90;
	// Update is called once per frame
	void Update () 
    {
		if (PlayerManager.cState == PlayerManager.GameStates.gameOver)
			return;
        if (UICamera.hoveredObject)
        {
            return;
        }

        #if UNITY_STANDALONE || UNITY_EDITOR
            mouseInput();
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * scrollSpeed, minFOV, maxFOV);
        #endif

        #if UNITY_IPHONE || UNITY_ANDROID && !UNITY_EDITOR
              touchInput();
        #endif

    }


	void touchInput()
    {
        float deltaTimeForLastInput = Time.time - lastInputTime;
        if (Input.touchCount > 0)
        {
            if (Input.touchCount < 2)
            {
                
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    lastInputPosition = Input.touches[0].position;
                    lastInputTime = Time.time;
                    movingCamera = false;
                    alreadyPinched = false;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    if (!movingCamera && deltaTimeForLastInput < inputCastSpellTime)
                    {
                        onCastSpell(Input.mousePosition);
                    }
                    movingCamera = false;
                    alreadyPinched = false;
                }
                else if (Input.touches[0].phase != TouchPhase.Canceled)
                {
                    if (!movingCamera)
                    {
                        if (Vector2.Distance(Input.touches[0].position, lastInputPosition) > distanceCameraActive)
                        {
                            movingCamera = true;
                        }
                    }
                    else
                    {
                        moveCamera(Input.touches[0].deltaPosition);
                        lastInputPosition = Input.touches[0].position;
                    }
                }
            }
            else
            {
                movingCamera = true;

                float curDist = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position); //current distance between finger touches
                float touchDelta = curDist - prevDist;
                prevDist = curDist;

                if (alreadyPinched)
                    Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView + touchDelta * Time.deltaTime * pinchSpeed, minFOV, maxFOV);
                alreadyPinched = true;

            }
        }
        else
        {
            alreadyPinched = false;
            movingCamera = false;
        }

	}

	void mouseInput()
	{
       
        Vector2 inputPos2D = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        float deltaTimeForLastInput = Time.time - lastInputTime;
		if (Input.GetMouseButtonDown(0) )
		{
            lastInputTime = Time.time;
            lastInputPosition = inputPos2D;
		}
        else if (Input.GetMouseButton(0))
        {
            
            if (!movingCamera)
            {
                if (Vector2.Distance(inputPos2D, lastInputPosition) > distanceCameraActive)
                {
                    movingCamera = true;
                }
            }
            else
            {
                
                Vector2 dPos = inputPos2D - lastInputPosition;
                moveCamera(dPos);
                lastInputPosition = inputPos2D;
            }
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!movingCamera && deltaTimeForLastInput < inputCastSpellTime)
            {
                onCastSpell(Input.mousePosition);
            }
            movingCamera = false;
        }
	}
	
	public void onCastSpell(Vector3 inputPos)
	{
        SpellManager.instance.castCSpell(inputPos);
	}

    public void moveCamera(Vector2 diff)
    {
        Vector3 toMove = new Vector3(diff.x,0,diff.y);
        float reducField =  Camera.main.fieldOfView / maxFOV ;
        Debug.Log(Camera.main.fieldOfView + " maxFov " + maxFOV );
        Camera.main.transform.Translate(toMove.normalized * cameraSpeed * reducField, Space.World);
    }


}
