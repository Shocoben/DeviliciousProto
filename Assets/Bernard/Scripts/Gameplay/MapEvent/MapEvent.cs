using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapEvent : MonoBehaviour {
    public List<string> compatibleSpells;
    public bool canBeActivatedByAnySpell = false;
    
    public void activate(Vector3 source, string spellName)
    {
        if (canBeActivatedByAnySpell || compatibleSpells.Contains(spellName))
        {
            onActivated(source, spellName);
        }
    }

    protected virtual void onActivated(Vector3 source, string spellName)
    {

    }
}
