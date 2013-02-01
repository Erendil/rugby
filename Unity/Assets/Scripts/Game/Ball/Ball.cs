using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * @class Ball
 * @brief Composant faisant de l'objet, une balle de rugby utilisable.
 * @author Sylvain Lafon
 * @author Guilleminot Florian
 */
[AddComponentMenu("Scripts/Game/Ball"), RequireComponent(typeof(Rigidbody))]
public class Ball : TriggeringTriggered {
    public Game Game;
	public Vector3 multiplierDrop = new Vector3(50.0f, 70.0f, 0.0f);
	public Vector3 multiplierPass = new Vector3(20.0f, 70.0f, 20.0f);
	public float passSpeed = 40.0f;
	
	/*
	 * @ahtor Maxens Dubois 
	 */
	private bool goScrum;
	
    private Unit _owner;
    public Unit Owner
    {
        get
        {
            return _owner;
        }
        set
        {
            if (_owner != value)
            {
                PreviousOwner = _owner;
                _owner = value;
                Game.OnOwnerChanged(_owner, value);
            }         
        }
    }

    private Unit _previousOwner;
    public Unit PreviousOwner
    {
        get
        {
            return _previousOwner;
        }
        private set
        {
            _previousOwner = value;
        }
    }
   
	
	void Start(){
		goScrum = false;
	}
	
    public void Update()
    {
        if (Owner != null)
        {
            if (this.transform.position != Owner.BallPlaceHolderRight.transform.position &&
                this.transform.position != Owner.BallPlaceHolderLeft.transform.position)
            {
                this.transform.position = Owner.BallPlaceHolderRight.transform.position;
            }
                       
            this.transform.localRotation = Quaternion.identity;
        }

        UpdateTackle();
    }
  
    public void Drop()
    {              
        this.transform.parent = null;
        this.rigidbody.useGravity = true;
		this.rigidbody.isKinematic = false;
        this.rigidbody.AddForce(Owner.transform.forward * multiplierDrop.x + Owner.transform.up * multiplierDrop.y + Owner.transform.right * multiplierDrop.z);
        Owner = null;
    }

	public void Pass(Unit to)
	{
        Unit from = this.Owner;

		float distance = (to.transform.position - from.transform.position).magnitude;
		float timeEstimed = distance / passSpeed;
		Vector3 positionEstimed = to.transform.position + to.transform.forward * to.GetNMA().speed * timeEstimed;
		Vector3 direction = (positionEstimed - from.transform.position);
		direction.y += distance / 2;

        // FIX. (TODO)
        to.Order = /* Order.OrderMove(positionEstimed, Order.TYPE_DEPLACEMENT.SPRINT); */
                   Order.OrderFollowBall();

		Debug.Log("position de to : " + to.transform.position + " position estim� apr�s la passe : " + positionEstimed);

        this.transform.parent = null;
        this.rigidbody.isKinematic = false;
		this.rigidbody.useGravity = true;
        this.rigidbody.AddForce(direction * passSpeed);
		Owner = null;

	}

	//Poser la balle
    public void Put()
    {
        setPosition(this.transform.position);    
    }

    public void Taken(Unit u)
    {	
		Debug.Log("i take the ball " + u.name);

		this.rigidbody.useGravity = false;
		this.rigidbody.isKinematic = true;
		this.transform.parent = u.BallPlaceHolderRight.transform;
		this.transform.localPosition = Vector3.zero;

		if (Owner != u)
		{
			Owner = u;
		}
	}

    public void setPosition(Vector3 v)
    {
        if (v.y == 0)
        {
            v.y = 0.5f;
        }

        this.transform.parent = null;
        this.transform.position = v;
        this.rigidbody.useGravity = true;
        this.rigidbody.isKinematic = false;
        this.rigidbody.velocity = Vector3.zero;
        this.transform.rotation = Quaternion.identity;
        this.Owner = null;
    }

    List<Unit> scrumFieldUnits = new List<Unit>();
    public override void Entered(Triggered o, Trigger t)
    {       
        if (t.GetType() == typeof(NearBall))
        {
            Unit u = o.GetComponent<Unit>();
            if (u != null)
            {
                u.sm.event_NearBall();
            }
        }
        else if (t.GetType() == typeof(ScrumField))
        {
            Unit u = o.GetComponent<Unit>();
            if (u != null)
            {
                if(!scrumFieldUnits.Contains(u))
                    scrumFieldUnits.Add(u);
            }
        }
    }

    public override void Left(Triggered o, Trigger t)
    {
        if (t.GetType() == typeof(ScrumField))
        {
            Unit u = o.GetComponent<Unit>();
            if (u != null)
            {
                if (scrumFieldUnits.Contains(u))
                    scrumFieldUnits.Remove(u);
            }
        }
    }

    public float lastTackle = -1;
    public void EventTackle(Unit tackler, Unit tackled)
    {
        if(lastTackle == -1)
            lastTackle = Time.time;
    }
	
	public bool getGoScrum(){
		return goScrum;
	}
	
	public void setGoScrum(bool state){
		goScrum = state;
	}
	
    public void UpdateTackle()
    {       
        if (lastTackle != -1)
        {
            // TODO cte : 2 -> temps pour checker
            if (Time.time - lastTackle > 2)
            {
                lastTackle = -1;
                int right = 0, left = 0;
                for (int i = 0; i < scrumFieldUnits.Count; i++)
                {
                    if (scrumFieldUnits[i].Team == Game.right)
                        right++;
                    else
                        left++;
                }

                // TODO cte : 3 --> nb de joueurs de chaque equipe qui doivent etre dans la zone
                if (right >= 3 && left >= 3)
                {
                    goScrum = true;
					//Debug.Log("Scruuum");
                }else{
					//goScrum = false;
				}
            }
        }
    }

}
