using UnityEngine;
using System.Collections;

public abstract class Spell : MonoBehaviour 
{
    public LayerMask stressLayer;
    public LayerMask panicLayer;
    public LayerMask impactLayers;
    public LayerMask eventLayers;

    private static int _stressLayer;
    private static int _panicLayer;
    private static int _eventLayers;

    public float stressRadius = 8;
    public float panicDistance = 3;
    public float missAttenuation = 1.5f;

    public float activateRadius = 0.8f;
    public string name = "Thunder";

    public virtual void OnCast(Vector3 inputPos)
    {
        Ray camRay = Camera.main.ScreenPointToRay(inputPos);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100, impactLayers.value))
        {
            onCastWorld(hit);
        }
    }

    public virtual void onCastWorld(RaycastHit hit)
    {
        
    }

    public virtual void impactSociety(RaycastHit hit)
    {
        Spell.MakeStress(hit.point, stressRadius, (hit.transform.tag == "Villager") ? 1 : missAttenuation);
        Spell.PanicVillagers(hit.point, panicDistance);
    }



    public virtual void Setup()
    {
        _stressLayer = stressLayer.value;
        _panicLayer = panicLayer.value;
        _eventLayers = eventLayers.value;
    }

    public static void ActiveNeighbourhoodsEvents(string spellName, Vector3 source, float radius)
    {
        Collider[] events = Physics.OverlapSphere(source, radius, _eventLayers);
        for (int i = 0; i < events.Length; ++i)
        {
            if (events[i] == null)
                continue;
            MapEvent cEvent = events[i].GetComponent<MapEvent>();
            cEvent.activate(source, spellName);
        }
    }

    public static void MakeStress(Vector3 source, float radius, float attenuation = 1)
    {
        Collider[] houses = Physics.OverlapSphere(source, radius, _stressLayer);

        for (int i = 0; i < houses.Length; ++i)
        {
            House house = houses[i].gameObject.GetComponent<House>();
            house.addStressByDistance(Vector3.Distance(source, house.transform.position) / attenuation);
        }
    }

    public static void PanicVillagers(Vector3 source, float radius)
    {
        Collider[] villagers = Physics.OverlapSphere( source, radius, _panicLayer);
        Villager cVillager = null;
        Vector3 cPanicDirection = new Vector3();
        //Debug.Log("panic villager " + villagers.Length +"source" + source + "radius" + radius);
        
        for (int i = 0; i < villagers.Length; ++i)
        {
            cVillager = villagers[i].GetComponent<Villager>();
            cPanicDirection = villagers[i].transform.position - source;
            cVillager.panicDirection = new Vector3(cPanicDirection.x, 0, cPanicDirection.z);
            cVillager.state = Villager.States.panic;
        }
        
    }


}
