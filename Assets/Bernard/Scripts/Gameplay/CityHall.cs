using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CityHall : MonoBehaviour 
{
    public GameObject panicAuraPrefab;
    public LayerMask houseLayer;
    public float auraSpeed = 8;
    public float stressSeuil = 1;
    public float maxAuraRadius = 10;

    private float _stress = 0;
    private List<House> _attachedHouses = new List<House>();

    private GameObject _panicAura = null;
    private float _panicAuraRadius = 1;
    

    public void addStress(float stress)
    {
        _stress += stress;
        if (_stress >= stressSeuil && _panicAura == null)
        {
            spawnAttachedHouses();
            startPanicAura();
        }
    }

    public void spawnAttachedHouses()
    {

        for (int i = 0; i < _attachedHouses.Count; ++i)
        {
            _attachedHouses[i].setStress(1);
        }
    }

    public void startPanicAura()
    {
        if (_panicAura == null)
         _panicAura = GameObject.Instantiate(panicAuraPrefab, transform.position, transform.rotation) as GameObject;
    }

    public GUIStyle stressStyle;
    public void OnGUI()
    {
        Vector2 GUIPoint = GUIUtility.ScreenToGUIPoint(Camera.main.WorldToScreenPoint(transform.position));
        GUI.Label(new Rect(GUIPoint.x, GUIPoint.y, 100, 25), "VillageStress " + _stress.ToString("F2"), stressStyle);
    }

    public void attachHouse(House house)
    {
        _attachedHouses.Add(house);
    }

    public void FixedUpdate()
    {
        if (_panicAura != null && _panicAuraRadius < maxAuraRadius)
        {
            _panicAuraRadius += auraSpeed * Time.deltaTime;
            _panicAura.transform.localScale = new Vector3( _panicAuraRadius, _panicAura.transform.localScale.y, _panicAuraRadius );
            
            //Collider[] housesGO = Physics.OverlapSphere(transform.position, _panicAuraRadius, houseLayer);

            House cHouse;
            for (int i = 0; i < House.unspawnedHouses.Count; ++i)
            {
                cHouse = House.unspawnedHouses[i];
                if (Vector3.Distance(transform.position, cHouse.transform.position) < _panicAuraRadius * 0.5f)
                {
                    cHouse.spawnVillager( );
                }
            }

            Villager cVillager;
            List<Villager> quietVillagers = Villager.villagersByState[Villager.States.quiet];
            for (int j = 0; j < quietVillagers.Count; ++j)
            {
                if (quietVillagers[j] == null)
                    continue;
                cVillager = quietVillagers[j];
                if (Vector3.Distance(transform.position, cVillager.transform.position) < _panicAuraRadius * 0.5f)
                {
                    cVillager.state = Villager.States.statue;
                }
            }
        }
    }
}
