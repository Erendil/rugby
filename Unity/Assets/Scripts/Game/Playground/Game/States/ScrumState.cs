/**
  * @class ScrumState
  * @brief Etat de la cam�ra durant une m�l�e
  * @author Sylvain Lafon
  * @see GameState
  */
using UnityEngine;

public class ScrumState : GameState
{
    public ScrumState(StateMachine sm, CameraManager cam, Game game) : base(sm, cam, game) { }
	
	void initCutScene(){
		cam.setTarget(null);
	}
	
	public override void OnEnter()
    {
		//initCutScene();
		MyDebug.Log("previous owner "+cam.game.Ball.PreviousOwner);
    }
}