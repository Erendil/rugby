using UnityEngine;
using System;
using System.Collections;

public class TackleManager: MonoBehaviour {

    public Unit tackled { get; set; }
    public Unit tackler { get; set; }
    	
    public enum RESULT
    {
        NONE,
        QTE,
        CRITIC,
        PASS,
        NORMAL
    }

    public Action<RESULT> callback;

    private RESULT result = RESULT.NONE;

    public void Tackle()
    {
        result = RESULT.NONE;

        if (tackled == null || tackler == null)
        {
            throw new UnityException("Manque tackled ou tackler");
        }

    	if (IsCrit())
		{            
            // Le plaqueur r�cup�re la balle instan�ement => Cut Sc�ne	
            result = RESULT.CRITIC;

            if (callback != null)
                callback(result);              
		}
		else
		{
            result = RESULT.QTE;
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

    public void Update()
    {
        if (result != RESULT.QTE)
        {
            result = RESULT.NONE;
        }
    }
	
	private bool IsCrit()
	{
        float angle = Vector3.Angle(tackled.transform.position - tackler.transform.position, tackler.transform.forward);
        bool supporte = tackled.getNearAlliesNumber() > 0;

		return angle <= tackler.Team.AngleOfFovTackleCrit && !supporte;
	}
}
