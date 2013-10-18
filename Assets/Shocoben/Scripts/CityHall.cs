using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CityHall : MonoBehaviour 
{
    public static bool cityHallStressmode = true;
    private float _stress = 0;

    private List<House> _attachedHouses = new List<House>();

    /*
    public void addStressByDistance(float distance)
    {
        distance = Mathf.Max(0, (maxDistance - distance) / attenuation);
        stress += distance;
    }*/

    public void addStress(float stress)
    {
        _stress += stress;
        if (_stress >= 1)
            spawnAttachedHouses();
    }

    public void spawnAttachedHouses()
    {
        for (int i = 0; i < _attachedHouses.Count; ++i)
        {
            _attachedHouses[i].setStress(1);
        }
    }

    public GUIStyle stressStyle;
    public void OnGUI()
    {
        Vector2 GUIPoint = GUIUtility.ScreenToGUIPoint(Camera.main.WorldToScreenPoint(transform.position));
        GUI.Label(new Rect(GUIPoint.x, GUIPoint.y, 100, 25), "VillageStress " + _stress, stressStyle);
    }

    public void attachHouse(House house)
    {
        _attachedHouses.Add(house);
    }
}
