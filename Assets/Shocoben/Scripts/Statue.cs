using UnityEngine;
using System.Collections;

public class Statue : MonoBehaviour 
{
    public float distancePraying = 2;
    public static  uint instanceCount = 0;
    public uint id = 0;

    void Awake()
    {
        instanceCount++;
        id = instanceCount;
    }
	// Update is called once per frame
	void Update () 
    {
        Villager cVillager;
        for (int i = 0; i < Villager.statueVillagers.Count; ++i)
        {
            cVillager = Villager.statueVillagers[i];
            if (cVillager == null)
                continue;
            if (cVillager.statueTarget.id == id && Vector3.Distance(transform.position, cVillager.transform.position) < distancePraying)
            {
                cVillager.state = Villager.States.praying;
            }
        }
	}

}
