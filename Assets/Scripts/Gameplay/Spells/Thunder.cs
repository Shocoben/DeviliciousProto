using UnityEngine;
using System.Collections;

public class Thunder : InstantDestroySpell {
    public string INSTANTDESTROYS = "-----------------------------------";
    public GameObject forbiddenTexture;
    //Thunder
    public GameObject thunderImpactPrefab;
    public GameObject thunderLightningPrefab;

    public Quaternion rotationLightning = Quaternion.identity;

    public override void OnCast(Vector3 inputPos)
    {
        if (Statue.activeStatues["antithunder"].Count > 0)
        {
            Debug.Log("show!");
            InGameNotifications.showNotification("ThunderForbidden");
            return;
        }
        base.OnCast(inputPos);
    }

    public override void OnHit(RaycastHit hit, bool villagerHit)
    {
        Vector3 impactPos = hit.point;
        Quaternion impactRot = Quaternion.FromToRotation(Vector3.up, hit.normal);
        impactPos.y += 0.2f;

        if (!villagerHit)
        {
            GameObject.Instantiate(thunderImpactPrefab, impactPos, impactRot);
        }

        impactPos.x -= 0.30f;
        impactPos.y += 3;
        GameObject.Instantiate(thunderLightningPrefab, impactPos, impactRot);
    }

    public override void onHitStaticObject(RaycastHit hit)
    {
        Vector3 impactPos = hit.point;
        impactPos.y += 0.2f;
        Quaternion impactRot = Quaternion.FromToRotation(Vector3.up, hit.normal);
        GameObject.Instantiate(thunderImpactPrefab, impactPos, impactRot);
        impactPos.x -= 0.60f;
        GameObject.Instantiate(thunderLightningPrefab, impactPos, impactRot);
    }
}
