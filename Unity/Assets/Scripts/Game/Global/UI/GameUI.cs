using UnityEngine;
using System;
using System.Collections;

public class GameUI{
	
	private Game game;
	
	public GameUI(Game _game)
	{
		game = _game;
	}

	public void DrawUI(float blueProgress, float redProgress)
	{
        GameUISettings settings = game.settings.UI.GameUI;

        settings.blueSuper.percent = blueProgress;
		settings.redSuper.percent = redProgress;
		
        settings.blueScore.number = game.southTeam.nbPoints;
        settings.redScore.number = game.northTeam.nbPoints;

		int reverseTime = (int)(game.settings.Global.Game.period_time - game.Referee.IngameTime);
		
        settings.timeNumber.number = reverseTime;

        ShowOutsideScreenUnit();
    }

    public void ShowOutsideScreenUnit()
    {
        //try
        //{
        //    Unit[] units = new Unit[2];
        //    units[0] = game.southTeam.Player.Controlled;
        //    units[1] = game.northTeam.Player.Controlled;            
        //    
        //    foreach (Unit u in units)
        //    {
        //        ShowOutsideScreenUnit(u);
        //    }            
        //}
        //catch (NullReferenceException e)
        //{
        //    Debug.Log(e.Message);
        //}
        //catch (Exception e)
        //{
        //    Debug.LogError(e.Message);
        //}
    }

    private Vector2 GetOutsideIndicationPosition(Vector3 position, Vector2 offset)
    {
        float w = Screen.width;
        float h = Screen.height;
               
        Camera cam = game.refs.managers.camera.gameCamera.camera;

        Vector3 screenPoint = cam.WorldToScreenPoint(position);
        screenPoint.y = h - screenPoint.y;
        
        bool inside = true;

        if (screenPoint.x > w)
        {
            inside = false;
            screenPoint.x = w - offset.x;
        }
        else if (screenPoint.x < 0)
        {
            inside = false;
            screenPoint.x = 0;
        }

        if (screenPoint.y > h)
        {
            inside = false;
            screenPoint.y = h - offset.y;
        }
        else if (screenPoint.y < 0)
        {
            inside = false;
            screenPoint.y = 0;
        }

        if (screenPoint.z < 0)
        {
            inside = false;
            screenPoint.y = h - offset.y;
            screenPoint.x = w - screenPoint.x - offset.x;
        }

        screenPoint.z = 0;

        if (inside)
        {
            return Vector2.zero;
        }

        return screenPoint;
    }

    private void ShowOutsideScreenUnit(Unit u) {
       
        Vector2 pos = this.GetOutsideIndicationPosition(u.transform.position, Vector2.one*20);
        if (pos != Vector2.zero)
        {
            GUI.Box(new Rect(pos.x, pos.y, 20, 20), u.name);
            //Debug.Log(u + " est hors vision !\n" + test);
        }
    }

}
