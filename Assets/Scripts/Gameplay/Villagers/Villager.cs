using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Villager : PoolableObject 
{
    public static bool initalizedVillagerDicitonary = false;
    public enum States { quiet, panic, statue, praying, movingToTrap, trapped };
    public static Dictionary<States, List<Villager>> villagersByState = new Dictionary<States, List<Villager>>();
    public static List<Villager> villagerListTest = new List<Villager>();

	public static int instances = 0;

	private int _id = 0;
    private float stress;
    
    public float speed = 3;

	public LayerMask cityHallLayer;
    public List<CityHall> cityHalls;
    private float _lastPanicTime = -1000;
    public float panicDuration = 1.5f;
    private States _lastState = States.quiet;

    public LayerMask villagerMask;
    public States _state = States.quiet;

    public bool cityHallMode = true;
    private Vector3 _panicDirection = Vector3.zero;
    private Statue _statueTarget;
    private Trap _trapTarget;

    public void Awake()
    {
        if (!initalizedVillagerDicitonary)
        {
            foreach (var value in Enum.GetValues(typeof(States)))
            {
                States cState = (States)value;
                villagersByState.Add(cState, new List<Villager>());
            }
            initalizedVillagerDicitonary = true;
        }

        villagersByState[States.quiet].Add(this);

        villagerListTest.Add(this);
        _id = instances;
        instances++;
    }

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

    public Trap trapTarget
    {
        set
        {
            _trapTarget = value;
        }
        get
        {
            return _trapTarget;
        }
    }



    public States state 
    {
        set
        {
            if (value == States.panic && _state == States.praying)
                return;

            if (value != _state)
            {
                villagersByState[_state].Remove(this);
                villagersByState[value].Add(this);
                _lastState = _state;
                onChangeState(value, _state);
            }

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

    private States _stateBeforeTrap = States.quiet;
    public void stopBeingTrapped()
    {
        _trapTarget = null;
        state = _stateBeforeTrap;
        Debug.Log(_stateBeforeTrap);
    }

    public void onChangeState(States newState, States oldState)
    {
        if (newState != States.movingToTrap && newState != States.trapped)
        {
            _stateBeforeTrap = newState;
        }

        if (oldState == States.movingToTrap)
        {
            if (_trapTarget != null)
            {
                _trapTarget.removeMovingToMeVillager(this);
                if (newState != States.trapped)
                    _trapTarget = null;
            }
        }
        else if (oldState == States.trapped)
        {
            if (_trapTarget != null)
            {
                _trapTarget.removeTrappedVillager(this);
                _trapTarget = null;
            }
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




    public void onHitted(Vector3 source)
	{
        Die();
	}

    public override void Die()
    {
        if (_statueTarget && _state == States.praying)
            _statueTarget.removePrayer();
        base.Die();

    }

    
    public float prayingRunSpeed = 0.8f;
    public void FixedUpdate()
    {
        switch (_state)
        {
            case States.panic :
                
                transform.Translate(_panicDirection * Time.deltaTime * speed, Space.World);
                transform.rotation = Quaternion.LookRotation(_panicDirection);
                if (_lastPanicTime + panicDuration < Time.time)
                {
                    state = States.statue;
                }
            break;
            case States.statue :
                moveTo(_statueTarget.transform.position);
            break;
            case States.praying :
                runAround(_statueTarget.transform.position);
            break;
            case States.movingToTrap :
                if (_trapTarget == null)
                    return;
                moveTo(_trapTarget.transform.position);
            break;
            case States.trapped :
                if (_trapTarget == null)
                    return;
                runAround(_trapTarget.transform.position);
            break;
            default :

            break;
        }
    }

    public void runAround(Vector3 O)
    {
        /*
          * A'.x = A.x * cos(θ) - A.y * sin(θ)
          * A'.y = A.x * sin(θ) + A.y * cos(θ)
         */
        float rotateSpeed = Time.deltaTime * prayingRunSpeed;
        Vector3 A = transform.position;
        Vector2 AmO = new Vector2(A.x - O.x, A.z - O.z);
        Vector2 rotAmO = new Vector2(AmO.x * Mathf.Cos(rotateSpeed) - AmO.y * Mathf.Sin(rotateSpeed), AmO.x * Mathf.Sin(rotateSpeed) + AmO.y * Mathf.Cos(rotateSpeed));
        Vector2 OR = new Vector2(rotAmO.x + O.x, rotAmO.y + O.z);
        transform.position = new Vector3(OR.x, transform.position.y, OR.y);
    }

    public void moveTo(Vector3 objectif)
    {
        Vector3 objectifDir = (objectif - transform.position);
        objectifDir.y = 0;
        transform.Translate(objectifDir.normalized * Time.deltaTime * speed, Space.World);
        transform.rotation = Quaternion.LookRotation(objectifDir);
    }
	
}
