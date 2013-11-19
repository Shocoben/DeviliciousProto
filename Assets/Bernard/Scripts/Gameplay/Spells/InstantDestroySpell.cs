using UnityEngine;
using System.Collections;

public class InstantDestroySpell : Spell {
    public string SPELL = "-----------------------------------";
    public float destroyRadius = 0.8f;
    public LayerMask destroyableLayer;

    public override void onCastWorld(RaycastHit hit)
    {
        Debug.Log("destroyRadius" + destroyRadius);
        Collider[] colliders = Physics.OverlapSphere(hit.point, destroyRadius, destroyableLayer.value);
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i] == null)
                continue;
            onHitDestroyable(colliders[i].gameObject, hit);
        }

        if (!hit.transform.CompareTag("Villager"))
        {
            onHitStaticObject(hit);
        }

        impactSociety(hit);
        Spell.ActiveNeighbourhoodsEvents(name, hit.point, activateRadius);
    }

    public virtual void onHitStaticObject(RaycastHit hit)
    {

    }

    public virtual void onHitDestroyable(GameObject destroyableGO, RaycastHit hit)
    {
        if (destroyableGO.CompareTag("Villager"))
        {
            Villager hitted = destroyableGO.GetComponent<Villager>();
            hitted.onHitted(hit.point);
        }
    }
}
