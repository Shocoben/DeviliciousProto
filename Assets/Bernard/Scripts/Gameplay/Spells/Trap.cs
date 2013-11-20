using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Trap : InstantiedObject {

    public Villager.States[] trappableVillagerStates;

    public float distanceTrapMove = 3;

    public float distanceTrap = 0.8f;
    private List<Villager> movingToMeVillagers = new List<Villager>();
    private List<Villager> trappedVillagers = new List<Villager>();
    public Color color;
    public GameObject meshTrap;
    public float duration = 10;
    public float lastTimeAlive = -1000;
	// Update is called once per frame

    void Awake()
    {
        meshTrap.gameObject.renderer.material.color = color;
        meshTrap.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    
    void FixedUpdate() 
    {
	    Villager cVillager;

        for (int j = 0; j < trappableVillagerStates.Length; ++j)
        {
            List<Villager> canBeTrappedVillagers = Villager.villagersByState[trappableVillagerStates[j]];

            for (int i = 0; i < canBeTrappedVillagers.Count; ++i)
            {
                cVillager = canBeTrappedVillagers[i];
                if (cVillager == null)
                    continue;
                if (cVillager.trapTarget == null && Vector3.Distance(transform.position, cVillager.transform.position) < distanceTrapMove)
                {
                    cVillager.trapTarget = this;
                    cVillager.state = Villager.States.movingToTrap;
                    movingToMeVillagers.Add(cVillager);
                }
            }
        }

        for (int j = 0; j < movingToMeVillagers.Count; ++j)
        {
            cVillager = movingToMeVillagers[j];
            if (cVillager == null)
            {
                continue;
            }
            if (Vector3.Distance(transform.position, cVillager.transform.position) < distanceTrap)
            {
                cVillager.state = Villager.States.trapped;
                trappedVillagers.Add(cVillager);
            }
        }

        if (lastTimeAlive + duration < Time.time)
        {
            Die();
        }
        
	}

    public void removeMovingToMeVillager(Villager villager)
    {
        movingToMeVillagers.Remove(villager);
    }

    public void removeTrappedVillager(Villager villager)
    {
        trappedVillagers.Remove(villager);
    }

    public override void Die()
    {
        for (int i = 0; i < movingToMeVillagers.Count; ++i)
        {
            movingToMeVillagers[i].stopBeingTrapped();
        }
        for (int j = 0; j < trappedVillagers.Count; ++j)
        {
            trappedVillagers[j].stopBeingTrapped();
        }

        movingToMeVillagers.Clear();
        trappedVillagers.Clear();
        base.Die();
    }

    public override void Alive()
    {
        base.Alive();
        lastTimeAlive = Time.time;
    }
}
