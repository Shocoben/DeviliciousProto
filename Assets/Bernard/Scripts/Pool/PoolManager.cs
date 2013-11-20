using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PoolableCategory
{
    public string name;
    public GameObject prefab;
    public int nbrInstance;
}

public class PoolManager : MonoBehaviour {
    public static PoolManager instance;
    
    public List<PoolableCategory> poolableCategories;
    private Dictionary<string, List<PoolableObject>> pool = new Dictionary<string, List<PoolableObject>>();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
        instance = this;
        loadPoolableObjects();
    }

    public void loadPoolableObjects()
    {
        for (int i = 0; i < poolableCategories.Count; ++i)
        {
            string categoryName = poolableCategories[i].name;
            if (!pool.ContainsKey(categoryName))
            {
                pool.Add(categoryName, new List<PoolableObject>());
            }

            GameObject prefab = poolableCategories[i].prefab;
            for (uint j = 0; j < poolableCategories[i].nbrInstance; ++j)
            {
                GameObject iGO = GameObject.Instantiate(prefab) as GameObject;
                PoolableObject pO = iGO.GetComponent<PoolableObject>();
          
                if ( pO == null )
                {
                    Debug.LogError( "Prefab " + categoryName + " has no PoolableObject component" );
                    break;
                }
                pO.gameObject.SetActive(false);
                pO.poolCategoryName = categoryName;
                pool[categoryName].Add(pO);
            }
        }
    }

    public bool isEnoughObjectInThePool(string categoryName)
    {
        if (pool[categoryName].Count > 0)
            return true;
        Debug.LogError("The pool for the category " + categoryName + " is empty");
        return false;
    }

    public PoolableObject getPoolableObject(string categoryName, bool setAlive = true)
    {
        if (pool.ContainsKey(categoryName) && isEnoughObjectInThePool(categoryName))
        {
            PoolableObject obj = pool[categoryName][0];
            if (setAlive)
                obj.Alive();
            return obj;
        }
        return null;
    }

    public void getPoolableObject<T>(string categoryName,  out T obj ,bool setAlive = true) where T : PoolableObject
    {
        PoolableObject pObj = getPoolableObject(categoryName, setAlive);
        if (pObj != null)
            obj = pObj.GetComponent<T>();
        else
            obj = null;
    }

    public void addToPool(string categoryName, PoolableObject obj)
    {
        if (pool.ContainsKey(categoryName))
            pool[categoryName].Add(obj);
        else
            Debug.LogError("addToPool : There is no categoryName " + categoryName);

    }

    public void removeFromPool(string categoryName, PoolableObject obj)
    {
        if (pool.ContainsKey(categoryName) && categoryName.Length > 0)
            pool[categoryName].Remove(obj);
        else
            Debug.LogError("removeFromPool : categoryName " + categoryName + " is not in the pool");
    }

}
