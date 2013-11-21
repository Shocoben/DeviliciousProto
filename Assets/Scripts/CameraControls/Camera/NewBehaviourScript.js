#pragma strict

var x:float;
var y:float;
     
var rotation:Quaternion;
var position:Vector3;
     
var xSpeed:float;
var ySpeed:float;
     
var pinchSpeed:float;
var distance:float = 15;
     
var minimumDistance:float = 5;
var maximumDistance:float = 100;
     
private var lastDist:float = 0;
private var curDist:float = 0;
     
     
//new variables realted to attempt to move camera
var touchA:Touch;
var touchB:Touch;
     
var touchToWorldA: Vector3;
var touchToWorldB: Vector3;
     
var center: Vector3;
     
     
     
     
function Update ()
     
{ //One finger touch orbits camera
    if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
    {
     
        var touch = Input.touches[0];
        x += touch.deltaPosition.x * xSpeed;
        y -= touch.deltaPosition.y * ySpeed;
    }
     
    //Two finger pinch to zoom in/out
    if (Input.touchCount > 1 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved))
    {
     
        var touch1 = Input.touches[0];
        var touch2 = Input.touches[1];
        curDist = Vector2.Distance(touch1.position, touch2.position);
        if(curDist > lastDist)
        {
            distance -= Vector2.Distance(touch1.deltaPosition, touch2.deltaPosition)*pinchSpeed/10;
        }else{
            distance += Vector2.Distance(touch1.deltaPosition, touch2.deltaPosition)*pinchSpeed/10;
        }
        lastDist = curDist;
    }
     
     
    if(distance <= minimumDistance)
    {
        //minimum camera distance
        distance = minimumDistance;
    }
     
    if(distance >= maximumDistance)
    {
        //maximum camera distance
        distance = maximumDistance;
    }
     
     
     
    //this is what I have tried so far
    if (Input.touchCount > 1 && Input.GetTouch(0).phase == TouchPhase.Began)
    {
        touchA = Input.touches[0];
        touchB = Input.touches[1];
     
     
        touchToWorldA = camera.ScreenToWorldPoint (Vector3 ((touchA.position.x), (touchA.position.y), camera.nearClipPlane));
        touchToWorldB = camera.ScreenToWorldPoint (Vector3 ((touchB.position.x), (touchB.position.y), camera.nearClipPlane));
     
        center = (touchToWorldA + touchToWorldA / 2);
     
    }
     
     
    //Sets rotation
    rotation = Quaternion.Euler(y, x, 0);
     
     
    //Sets zoom
     
    //this was the original
    // position = Vector3(0.0, 0.0, -distance);
    //this is the edit
    position = Vector3(center.x, center.y, -distance);
     
    //Applies rotation and position
    transform.rotation = rotation;
    transform.position = position;
     
     
     
}