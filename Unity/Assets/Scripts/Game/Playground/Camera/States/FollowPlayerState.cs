/**
  * @class FollowPlayerState
  * @brief Etat de la cam�ra lorsqu'elle doit suivre un joueur.
  * @author Sylvain Lafon
  * @see MonoBehaviour
  */
public class FollowPlayerState : CameraState {
    public FollowPlayerState(StateMachine sm, CameraManager cam) : base(sm, cam) { }

    public override void OnEnter()
    {
        cam.setTarget(cam.game.Ball.Owner.transform);
    }

    public override bool OnNewOwner(Unit old, Unit current)
    {
        cam.setTarget(cam.game.Ball.Owner.transform);
        return true;
    }
}
