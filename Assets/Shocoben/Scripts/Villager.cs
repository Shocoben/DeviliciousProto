using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Villager : MonoBehaviour 
{
    public static List<Villager> quietVillagers = new List<Villager>();
    public static List<Villager> statueVillagers = new List<Villager>();


    public enum States { still, panic , statue, praying}
	public static int instances = 0;


	private int _id = 0;
    private float stress;
    
    public float speed = 3;

	public LayerMask cityHallLayer;
    public List<CityHall> cityHalls;
    private float _lastPanicTime = -1000;
    public float panicDuration = 1.5f;
    private States _lastState = States.still;

    public LayerMask villagerMask;
    private States _state = States.still;

    public bool cityHallMode = true;
    private Vector3 _panicDirection = Vector3.zero;
    private Statue _statueTarget;

    public Statue statueTarget
    {
        set
        {
            _statueTarget = value;
        }
        get
        {
            return _statueTarget;
        }
    }

    public States state 
    {
        set
        {
            if (value == States.panic && _state == States.praying)
                return;

            if (value == States.still)
            {
                quietVillagers.Add(this);
            }
            else
            {
                quietVillagers.Remove(this);
            }

            if (value == States.statue)
            {
                statueVillagers.Add(this);
            }
            else
            {
                statueVillagers.Remove(this);
            }

            

            if (value != _state)
                _lastState = _state;

            

            if (value == States.panic)
            {
                _lastPanicTime = Time.time;
            }
            else if (value == States.statue && _statueTarget == null)
            {
                GameObject[] statues = GameObject.FindGameObjectsWithTag( "Statue" );
                float lastMin = 1000;
                for ( int i = 0; i < statues.Length; ++i )
                {
                    float distance = Vector3.Distance(statues[i].transform.position, transform.position);
                    if (distance < lastMin)
                    {
                        lastMin = distance;
                        _statueTarget = statues[i].GetComponent<Statue>();
                    }
                }
            }
            _state = value;
        }
        
        get 
        {
            return _state;
        }
   }

    public Vector3 panicDirection
    {
        set
        {
            _panicDirection = value.normalized;
        }
        get
        {
            return _panicDirection;
        }
    }


	public void Awake()
	{
        quietVillagers.Add(this);
		_id = instances;
		instances++;
        /*
		GameObject[] cityHallsObj = GameObject.FindGameObjectsWithTag("CityHall");
        for (int i = 0; i < cityHallsObj.Length; ++i)
        {
            cityHalls.Add( cityHallsObj[i].GetComponent<CityHall>() );
        }*/

	}

    public void onHitted(Vector3 source)
	{
        active = false;
        GameObject.Destroy(this.gameObject);
        
	}

    public float prayingRunSpeed = 0.8f;
    public void FixedUpdate()
    {
        switch (_state)
        {
            case States.panic :
                transform.Translate(_panicDirection * Time.deltaTime * speed);
                if (_lastPanicTime + panicDuration < Time.time)
                {
                    state = States.statue;
                }
            break;
            case States.statue :
                Vector3 statueDir = (_statueTarget.transform.position - transform.position);
                statueDir.y = 0;
                transform.Translate(statueDir.normalized * Time.deltaTime * speed);
            break;
            case States.praying :
                /*
                 * A'.x = A.x * cos(θ) - A.y * sin(θ)
                 * A'.y = A.x * sin(θ) + A.y * cos(θ)
                */

                float rotateSpeed = Time.deltaTime * prayingRunSpeed;
                Vector3 A = transform.position;
                Vector3 O = _statueTarget.transform.position;
                Vector2 AmO = new Vector2(A.x - O.x, A.z - O.z);
                Vector2 rotAmO = new Vector2(AmO.x * Mathf.Cos(rotateSpeed) - AmO.y * Mathf.Sin(rotateSpeed), AmO.x * Mathf.Sin(rotateSpeed) + AmO.y * Mathf.Cos(rotateSpeed));
                Vector2 OR = new Vector2(rotAmO.x + O.x, rotAmO.y + O.z);
                transform.position = new Vector3(OR.x, transform.position.y, OR.y);
                
            break;
            default :

            break;
        }
    }
	
}
