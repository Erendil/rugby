/**
  * @class ScrumState
  * @brief Etat de la cam�ra durant une m�l�e
  * @author Sylvain Lafon
  * @see CameraState
  */
using UnityEngine;

public class ScrumState : CameraState
{
    public ScrumState(StateMachine sm, CameraManager cam) : base(sm, cam) { }
	
	void initCutScene(){
		cam.setTarget(this.cam.game.ScrumBloc.transform);
	}
	
	public override void OnEnter()
    {
		initCutScene();
		//MyDebug.Log("previous owner "+cam.game.Ball.PreviousOwner);

       // cam.game.arbiter.ScrumCinematicMovement();
       // cam.game.arbiter.NowScrum();

    }
}