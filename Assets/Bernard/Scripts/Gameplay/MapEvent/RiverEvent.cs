using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RiverEvent : MapEvent
{

    public LayerMask destroyableLayer;
    public float destroyRadius = 5;

    public List<GameObject> villagersInWater = new List<GameObject>();

    protected override void onActivated(Vector3 source, string spellName)
    {

        Debug.Log("on activated");

        switch (spellName)
        {

            case "Thunder":
                Debug.Log("I am Thunder");
                
                Collider[] colliders = Physics.OverlapSphere(source, destroyRadius, destroyableLayer.value);

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //sphere.collider.enabled = false;
                //sphere.transform.position = source;
                //sphere.transform.localScale = new Vector3(destroyRadius * 2, destroyRadius * 2, destroyRadius * 2);


                for (int i = 0; i < colliders.Length; ++i)
                {
                    if (colliders[i] == null)
                        continue;
                    Debug.Log("name "+colliders[i].gameObject.name);
                    
                    onHitDestroyable(colliders[i].gameObject, source);
                }
                
                break;
            default:

                Debug.Log("I dont care");
                break;
        }

    }

    public void onHitDestroyable(GameObject destroyableGO, Vector3 hitPoint)
    {
        if (destroyableGO.CompareTag("Villager"))
        {
            Villager hitted = destroyableGO.GetComponent<Villager>();
            hitted.onHitted(hitPoint);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        villagersInWater.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        for (int i = 0; i <= villagersInWater.Count; i++)
        {
            /*
            if (villagersInWater[i] == other )
            {
                villagersInWater.RemoveAt(i);

            }
            */
        }
        
        
    }
}
