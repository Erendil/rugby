using UnityEngine;
using System.Collections;

/**
 * @class Ball
 * @brief Composant faisant de l'objet, une balle de rugby utilisable.
 * @author Sylvain Lafon
 */
[AddComponentMenu("Scripts/Game/Ball"), RequireComponent(typeof(Rigidbody))]
public class Ball : TriggeringTriggered {
    private Unit _owner;
    public Unit Owner
    {
        get
        {
            return _owner;
        }
        set
        {
            _owner = value;
            if(_owner != null)
                TeamOwner = _owner.Team;
        }
    }

    public Team TeamOwner;

    public void Update()
    {
        if (Owner != null)
        {
            this.transform.position = Owner.BallPlaceHolder.transform.position;
            this.transform.localRotation = Quaternion.identity;
        }       
    }
  
    public void ShootTarget(Unit u)
    {
        Unit shooter = Owner;

        Vector3 pos = u.transform.position;
        Owner = null;

        this.transform.parent = null;

        this.rigidbody.useGravity = true;
        this.rigidbody.AddForce(shooter.transform.forward * 50 + shooter.transform.up * 70);
    }

    public void Taken(Unit u)
    {
        this.rigidbody.useGravity = false;
        this.rigidbody.velocity = Vector3.zero;
        this.transform.parent = u.BallPlaceHolder.transform;
        this.transform.localPosition = Vector3.zero;

        if (TeamOwner != u.Team)
        {
            Camera.mainCamera.GetComponent<rotateMe>().rotate(new Vector3(0, 1, 0), 180);
        }

        Owner = u;

        // TODO : patch
        Owner.Team.Game.p1.controlled = u;
    }

    public void setPosition(Vector3 v)
    {
        this.Owner = null;        
        this.transform.parent = null;
        this.transform.position = v;
        this.rigidbody.useGravity = true;
        this.rigidbody.velocity = Vector3.zero;       
        this.transform.rotation = Quaternion.identity;     
    }

    public override void Entered(Triggered o, Trigger t)
    {
        Unit u = o.GetComponent<Unit>();
        if (u != null)
        {
            if (this.Owner == null)
            {
                this.Taken(u);
            }
        }
    }
}
