using UnityEngine;
using System.Collections;

public class SpellButton : MonoBehaviour {

    public string spellName;

    void OnClick()
    {
        SpellManager.instance.setCSpell(spellName);
    }
}
