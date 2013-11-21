using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct AttachedCityHall
{
    public CityHall cityHall;
    public float coef;
}

public class House : MonoBehaviour 
{
    public static List<House> unspawnedHouses = new List<House>();


    public GameObject villager;
    private List<AttachedCityHall> _attachedCityHalls = new List<AttachedCityHall>();

    private float _stress;
    
    public float maxAttackDistance = 5;
    public float attenuationAttack = 6;
    public float globalAttenuation = 10;

    public float maxCityHallDistance = 8;

    private bool _spawned = false;

    public bool alreadySpawned()
    {
        return _spawned;
    }

    public void Start()
    {
        GameObject[] cityHalls = GameObject.FindGameObjectsWithTag("CityHall");
        for (int i = 0; i < cityHalls.Length; ++i)
        {
            CityHall cityHall = cityHalls[i].GetComponent<CityHall>();
            float distance = (maxCityHallDistance - Vector3.Distance(cityHalls[i].transform.position, transform.position));
            if (distance > 0)
            {
              
                AttachedCityHall newHall = new AttachedCityHall();
                newHall.cityHall = cityHall;
                newHall.coef = distance / globalAttenuation;

                cityHall.attachHouse(this);
                _attachedCityHalls.Add(newHall);
            }
        }
        unspawnedHouses.Add(this);
    }

    public void addStressByDistance(float distance)
    {
        addStress(Mathf.Max(0, (maxAttackDistance - distance) / attenuationAttack));
    }

    public void addStress(float stress)
    {
        _stress += stress;
        for (int i = 0; i < _attachedCityHalls.Count; ++i)
        {
            _attachedCityHalls[i].cityHall.addStress(_attachedCityHalls[i].coef);
        }
        checkStress();
    }

    public void Die()
    {
        for (int i = 0; i < _attachedCityHalls.Count; ++i)
        {
            _attachedCityHalls[i].cityHall.detachHouse(this);
        }
        unspawnedHouses.Remove(this);
        GameObject.Destroy(this.gameObject);
    }

    public void setStress(float stress)
    {
        _stress = stress;
        checkStress();
    }

    public bool debug = false;
    public void checkStress()
    {
        if (_stress >= 1)
            spawnVillager();
    }

    public void spawnVillager()
    {
        if (_spawned)
            return;
        Vector3 villagerPosition = villager.transform.position;
        villagerPosition.x = transform.position.x + 0.2f;
        villagerPosition.z = transform.position.z + 0.2f;
        GameObject spawnedGO = GameObject.Instantiate(villager, villagerPosition, Quaternion.identity) as GameObject;
        Villager spawnedVillager = spawnedGO.gameObject.GetComponent<Villager>();
        spawnedVillager.state = Villager.States.statue;
        _spawned = true;
        unspawnedHouses.Remove(this);
    }

    public GUIStyle stressStyle;
    public void OnGUI()
    {
        /*
        if (!debug)
            return;
        Vector2 GUIPoint = GUIUtility.ScreenToGUIPoint(Camera.main.WorldToScreenPoint(transform.position));
        GUI.Label(new Rect(GUIPoint.x, GUIPoint.y, 100, 25), "HouseStress " + _stress.ToString("F2"), stressStyle);
         * */
    }
}
