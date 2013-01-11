using UnityEngine;
using System.Collections;

/**
 * @class Triggered
 * @brief Repr�sente un objet d�clencheur
 * @author Sylvain Lafon
 */
public abstract class Triggered : MonoBehaviour
{
    public virtual void Start()
    {
        if (this.collider == null)
        {
            throw new UnityException("Pas de collider pour cet objet");
        }

        if (this.collider.isTrigger)
        {
            throw new UnityException("Cet objet n'est pourtant pas un trigger !");
        }

        if (this.rigidbody == null)
        {
            // IsKinematic = true && UseGravity = false permet de laisser l'objet 'libre'
            throw new UnityException("Cet objet a besoin d'un rigidbody..");
        }
    }
}

/**
 * @class TriggeringTriggered
 * @brief Repr�sente un objet d�clencheur qui g�re des d�clenchements
 * @author Sylvain Lafon
 */
public abstract class TriggeringTriggered : Triggered, Triggering
{

    public virtual void Entered(Trigger t)
    {

    }

    public virtual void Inside(Trigger t)
    {

    }

    public virtual void Left(Trigger t)
    {

    }

    public virtual void Entered(Triggered o, Trigger t)
    {
        if (o == this) Entered(t);
    }

    public virtual void Inside(Triggered o, Trigger t)
    {
        if (o == this) Inside(t);
    }

    public virtual void Left(Triggered o, Trigger t)
    {
        if (o == this) Left(t);
    }
}
