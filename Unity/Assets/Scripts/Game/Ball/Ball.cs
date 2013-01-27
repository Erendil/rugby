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
                Game.OwnerChanged(_owner, value);
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
  
	//Drop
	/**
	 * TODO
	 * Passer un vector3 r�sultant du capteur de pression en param�tre
	 */
    public void Drop()
    {              
        this.transform.parent = null;
        this.rigidbody.useGravity = true;
		this.rigidbody.isKinematic = false;
        this.rigidbody.AddForce(Owner.transform.forward * multiplierDrop.x + Owner.transform.up * multiplierDrop.y + Owner.transform.right * multiplierDrop.z); // 750 1050 0
        Owner = null;
    }

	// Passe
	public void Pass(Vector3 direction, float pressionCapture = 1.0f)
	{
		Debug.Log("On Pass pression : " + pressionCapture + " direction : " + direction);
        Vector3 force = new Vector3(direction.x * multiplierPass.x * pressionCapture, 1 * multiplierPass.y * pressionCapture, direction.z * multiplierPass.z * pressionCapture);
        Debug.Log("--> force : " + force);

        this.transform.parent = null;
        this.rigidbody.isKinematic = false;
		this.rigidbody.useGravity = true;
		this.rigidbody.AddForce(force);
		Owner = null;
	}

	//Poser la balle
    public void Put()
    {
        setPosition(this.transform.position);    
    }

    public void Taken(Unit u)
    {
        this.rigidbody.useGravity = false;        
       // this.rigidbody.velocity = Vector3.zero;
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

        if (t.GetType() == typeof(ScrumField))
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
        lastTackle = Time.deltaTime;
    }

    public void UpdateTackle()
    {
        if (lastTackle >= 0)
        {
            // TODO cte : 2 -> temps pour checker
            if (Time.deltaTime - lastTackle < 2)
            {
                lastTackle = -1;
                int right = 0, left = 0;
                foreach (Unit u in scrumFieldUnits)
                {
                    if (u.Team == Game.right)
                        right++;
                    else
                        left++;
                }

                // TODO cte : 3 --> nb de joueurs de chaque equipe qui doivent etre dans la zone
                if (right >= 3 && left >= 3)
                {
                    Debug.Log("SCRUUUUUUMMM !!!");
                }
            }
        }
    }

}
