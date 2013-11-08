using UnityEngine;
using System.Collections;

public class InstianteSpell : Spell {
    public string SPELL = "-----------------------------------";

    public string toInstantiateSpellCategory = "Traps";

    public override void onCastWorld(RaycastHit hit)
    {
       InstantiedObject cObj;
       PoolManager.instance.getPoolableObject<InstantiedObject>(toInstantiateSpellCategory, out cObj);

       if (cObj != null)
       {
           cObj.setInPosition(hit);
       }
    }
}
