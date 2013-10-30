using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Statue : MonoBehaviour 
{

    public static Dictionary<string, List<Statue>> activeStatues = new Dictionary<string,List<Statue>>();
    

    public float distancePraying = 2;
    public static  uint instanceCount = 0;
    public uint id = 0;
    private int nbrPrayers = 0;
    private float prayingJauge = 0;
    public float prayingSpeed = 2;
    public float prayingSeuil = 1;
    public string name;

    public GUIStyle prayingStyle;
    private float _lastActiveStart = -1000;
    public float duration = 2;
    private bool _active = false;

    void Awake()
    {
        if (!activeStatues.ContainsKey(name))
        {
            activeStatues.Add(name, new List<Statue>());
        }
        instanceCount++;
        id = instanceCount;
    }

    public void removePrayer()
    {
        nbrPrayers--;
    }

    void FixedUpdate()
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
                nbrPrayers++;
            }
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
            _lastActiveStart = Time.time;
            activeStatues[name].Add(this);
            return;
        }
    }

    public void OnGUI()
    {
        Vector2 GUIPoint = GUIUtility.ScreenToGUIPoint(Camera.main.WorldToScreenPoint(transform.position));
        GUI.Label(new Rect(GUIPoint.x, Screen.height - GUIPoint.y, 100, 25), "PrayingJauge " + prayingJauge.ToString("F2"), prayingStyle);
    }

}
