using UnityEngine;
using System.Collections;

public class VillagerTarget : MonoBehaviour {

    public static uint instanceCount = 0;
    public uint targetID = 0;

    public virtual void Awake()
    {
        instanceCount++;
        targetID = instanceCount;
    }
}
