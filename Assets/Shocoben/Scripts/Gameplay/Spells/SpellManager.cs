using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellManager : MonoBehaviour {

    public static SpellManager instance;
    public GameObject[] spellsGOs;
    private Dictionary<string, Spell> _instancesByName = new Dictionary<string, Spell>();
    private Spell _currentSpell = null;

    public string currentSpellName;

    public Spell getSpell(string name)
    {
        if (_instancesByName.ContainsKey(name))
            return _instancesByName[name];
        return null;
    }

    public void setCSpell(string name)
    {
        if ((_currentSpell == null || name != _currentSpell.name) && _instancesByName.ContainsKey(name))
            _currentSpell = _instancesByName[name];
    }

    public void castCSpell(Vector3 inputPos)
    {
        _currentSpell.OnCast(inputPos);
    }

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
        instance = this;
        spawnSells();
        setCSpell(currentSpellName);
    }

    private void spawnSells()
    {
        for (uint i = 0; i < spellsGOs.Length; ++i)
        {
   
            Spell cSpell = spellsGOs[i].GetComponent<Spell>();
            string name = cSpell.name;
            if (!_instancesByName.ContainsKey(name))
            {
                _instancesByName.Add(name, cSpell);
                cSpell.Setup();
            }
        }

    }
}
