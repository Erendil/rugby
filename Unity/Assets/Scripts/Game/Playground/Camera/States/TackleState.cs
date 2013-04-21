/**
  * @class TackleState
  * @brief Etat de la cam�ra durant un plaquage
  * @author Sylvain Lafon
  * @see CameraState
  */
public class TackleState : CameraState
{
    public TackleState(StateMachine sm, CameraManager cam) : base(sm, cam) { }
}