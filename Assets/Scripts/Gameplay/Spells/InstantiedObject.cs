using UnityEngine;
using System.Collections;

public class InstantiedObject : PoolableObject {

    public float positionDistance = 2;
    public bool rotate = true;

    public void setInPosition(RaycastHit hit)
    {
        Vector3 position = hit.point + hit.normal * positionDistance;
        transform.position = position;
    }
}
