using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class InGameNotifications : MonoBehaviour {

    private static Dictionary<string, InGameNotifications> notificationsByName = new Dictionary<string, InGameNotifications>();

    public static void showNotification(string name)
    {

        if (notificationsByName.ContainsKey(name))
        {
			Debug.Log(name);
            notificationsByName[name].show();
        }
        else
        {
            Debug.LogError("Notification " + name + " doesn't exist");
        }
    }
	
	public static void clear()
	{
		notificationsByName.Clear();
	}
	
    public float duration = 2;
    private float startTime = -1000;
    private UIWidget _texture;
    public string name;

	// Use this for initialization
	void Start () {
        _texture = GetComponent<UIWidget>();
        if (_texture == null)
            Debug.LogError("There is no UITexture on object " + transform.name);

        if ( !notificationsByName.ContainsKey(name) )
            notificationsByName.Add(name, this);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (duration > 0 && startTime + duration < Time.time)
        {
            _texture.enabled = false;
        }
	}

    public void show()
    {
        _texture.enabled = true;
        startTime = Time.time;
    }
}
