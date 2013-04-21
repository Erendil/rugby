/**
  * @class DodgeState
  * @brief Etat de la cam�ra durant une esquive
  * @author Sylvain Lafon
  * @see CameraState
  */
public class DodgeState : CameraState
{
    public DodgeState(StateMachine sm, CameraManager cam) : base(sm, cam) { }
}