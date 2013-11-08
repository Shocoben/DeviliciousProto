using UnityEngine;
using System.Collections;

public class PoolableObject : MonoBehaviour {
    public string poolCategoryName;

    public virtual void Die()
    {
        PoolManager.instance.addToPool(poolCategoryName, this);
        gameObject.SetActive(false);
    }

    public virtual void Alive()
    {
        PoolManager.instance.removeFromPool(poolCategoryName, this);
        gameObject.SetActive(true);
    }
    
}
