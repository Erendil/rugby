/**
  * @class PlayingState
  * @brief Etat de la cam�ra durant le jeu
  * @author Sylvain Lafon
  * @see CameraState
  */
public class PlayingState : CameraState
{
    public PlayingState(StateMachine sm, CameraManager cam) : base(sm, cam) { }

    public override void OnEnter()
    {
        decide();
    }

    public override void OnLeave()
    {
        cam.setTarget(null);
    }

    private bool decide()
    {
        if (cam.game.Ball.Owner == null)
        {
            sm.state_change_son(this, new GroundBallState(sm, cam));
        }
        else
        {
            sm.state_change_son(this, new FollowPlayerState(sm, cam));
        }

        return true;
    }
}
