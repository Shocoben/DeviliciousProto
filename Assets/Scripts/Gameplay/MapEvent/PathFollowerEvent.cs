using UnityEngine;
using System.Collections;

[System.Serializable]
public class ObjectAndPath
{
    public Transform objectToAnimate;
    public Transform[] path;
    public int pathI;
    public float speed;
}

public class PathFollowerEvent : MapEvent 
{

    private bool _actived = false;
    public ObjectAndPath[] objectsToAnimate;

    protected override void onActivated(Vector3 source, string spellName)
    {
        base.onActivated(source, spellName);
        _actived = true;
    }

    public void FixedUpdate()
    {
        if (!_actived)
            return;
        for (int i = 0; i < objectsToAnimate.Length; ++i)
        {
            ObjectAndPath cObj = objectsToAnimate[i];
            if (cObj.pathI >= cObj.path.Length)
                continue;
            Vector3 dir = (cObj.path[cObj.pathI].position - cObj.objectToAnimate.position);
            dir.Normalize();

            cObj.objectToAnimate.Translate(dir * cObj.speed * Time.deltaTime);
            if (Vector3.Distance(cObj.objectToAnimate.position, cObj.path[cObj.pathI].position) < 0.2f)
            {
                cObj.pathI++;
            }
        }
  
    }
}
