using UnityEngine;
using System.Collections;

public class Thunder : InstantDestroySpell {
    public string INSTANTDESTROYS = "-----------------------------------";

    //Thunder
    public GameObject thunderImpactPrefab;

    public override void OnCast(Vector3 inputPos)
    {
        if (Statue.activeStatues["antithunder"].Count > 0)
            return;
        base.OnCast(inputPos);


    }

    public override void onHitStaticObject(RaycastHit hit)
    {
        Vector3 impactPos = hit.point;
        impactPos.y += 0.2f;
        Quaternion impactRot = Quaternion.FromToRotation(Vector3.up, hit.normal);
        GameObject.Instantiate(thunderImpactPrefab, impactPos, impactRot);
    }
}
