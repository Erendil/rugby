/**
  * @class DodgeState
  * @brief Etat de la cam�ra durant une esquive
  * @author Sylvain Lafon
  * @see CameraState
  */
public class DodgeState : CameraState
{
    public DodgeState(StateMachine sm, CameraManager cam, Unit unit) : base(sm, cam) { this.unit = unit; }

    Unit unit;
}