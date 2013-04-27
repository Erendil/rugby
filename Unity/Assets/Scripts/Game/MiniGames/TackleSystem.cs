using UnityEngine;
using System.Collections;

public class TackleSystem {
	
	private Unit tackled;
	private Unit tackler;
	private Ball ball;
	
	private float angleOfFOV;
	private float distanceOfTackle;
	
	
	public TackleSystem(Unit tackled, Unit tackler, Ball b, float teta, float d)
	{
        this.tackled = tackled;
        this.tackler = tackler;
        this.angleOfFOV = teta;
        this.distanceOfTackle = d;
        this.ball = b;
	}
			
	public void Tackle()
	{
		if (canTackle())
		{
			if (IsInRange())
			{
				if (IsCrit())
				{
                    // Le plaqueur r�cup�re la balle instan�ement => Cut Sc�ne	
                

				    //	ball.Owner = tackler;
			        //	ball.transform.parent = tackler;                   
				}
				else
				{
                    // Le plaqu� a une dur�e avant de tomber			    => time.timeScale (attention cam�ra !)
                    // Pendant la tomb�e : QTE => Cut sc�ne peut-�tre		=> code reusable
                    // UI : bouton A (pos tweakable)				        => �

                    // QTE :
                    // * Si pas appui sur A : Plaquage - M�l�e		=> voil� quoi ^^ 
                    // * Sinon : Passe




					//TODO : Launch CutScene
				    /*	
                        if (System.range(0,1) > 0.5f)
					    {
						    //ball.Owner = tackler;
						    //ball.transform.parent = tackler;
					    }
                    */
				}
			}
		}
	}
	
	private bool canTackle()
	{
		return tackled == ball.Owner;
	}
	
	private bool IsCrit()
	{
		return Vector3.Angle(tackled.transform.position - tackler.transform.position, tackler.transform.forward) <= angleOfFOV;
	}
	
	private bool IsInRange()
	{
		return (tackled.transform.position - tackler.transform.position).magnitude <= distanceOfTackle;
	}
	
}
