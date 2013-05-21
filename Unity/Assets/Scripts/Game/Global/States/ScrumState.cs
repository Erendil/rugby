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
		cam.setTarget(this.cam.game.refs.gameObjects.ScrumBloc.transform);
	}
	
	public override void OnEnter()
    {
		initCutScene();
		//

       // cam.game.arbiter.ScrumCinematicMovement();
       // cam.game.arbiter.NowScrum();

    }
}