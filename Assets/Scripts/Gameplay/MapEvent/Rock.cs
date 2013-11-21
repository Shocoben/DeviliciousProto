using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

    public string villagerTag = "Villager";
    public string houseTag = "House";

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        if (other.CompareTag(villagerTag))
        {
            other.GetComponent<Villager>().Die();
        }
        else if (other.CompareTag(houseTag))
        {
            other.GetComponent<House>().Die();
        }
    }
}
