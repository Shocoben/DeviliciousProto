using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Statue : VillagerTarget 
{

    public static Dictionary<string, List<Statue>> activeStatues = new Dictionary<string,List<Statue>>();
    

    public float distancePraying = 2;

    private int nbrPrayers = 0;
    private float prayingJauge = 0;
    public float prayingSpeed = 2;
    public float prayingSeuil = 1;
    public string name;

    public GUIStyle prayingStyle;
    private float _lastActiveStart = -1000;
    public float duration = 2;
    public bool _active = false;
    public ParticleSystem particle;

    public enum StatueState
    {
        None,
        Loading,
        ThrowSpell
    };

    public StatueState status;

    public override void Awake()
    {
	
 	    base.Awake();
        if (!activeStatues.ContainsKey(name))
        {
            activeStatues.Add(name, new List<Statue>());
        }

        status = StatueState.None;

    }

    

    public void removePrayer()
    {
        nbrPrayers--;
    }

    void FixedUpdate()
    {
		if (PlayerManager.GameStates.gameOver == PlayerManager.cState)
			return;
        Villager cVillager;
        List<Villager> statueVillagers = Villager.villagersByState[Villager.States.statue];

        for (int i = 0; i < statueVillagers.Count; ++i)
        {
            cVillager = statueVillagers[i];
            if (cVillager == null)
                continue;
            if (cVillager.statueTarget.targetID == targetID && Vector3.Distance(transform.position, cVillager.transform.position) < distancePraying)
            {
                cVillager.state = Villager.States.praying;
                nbrPrayers++;
            }
            
        }

        if (prayingJauge > 0 && particle.isPlaying == false )
        {
            particle.Play();
        }
        

        prayingJauge += nbrPrayers * prayingSpeed * Time.deltaTime;
        if (_lastActiveStart + duration < Time.time && _active)
        {
            activeStatues[name].Remove(this);
        }
        
        if (prayingJauge >= prayingSeuil)
        {
            prayingJauge = 0;
            _active = true;
            onActive();
            _lastActiveStart = Time.time;
            activeStatues[name].Add(this);
            return;
        }
    }
	
	
	public virtual void onActive()
	{
		
	}
	
    public void OnGUI()
    {
        Vector2 GUIPoint = GUIUtility.ScreenToGUIPoint(Camera.main.WorldToScreenPoint(transform.position));
        GUI.Label(new Rect(GUIPoint.x, Screen.height - GUIPoint.y, 100, 25), "PrayingJauge " + prayingJauge.ToString("F2"), prayingStyle);
    }

}
