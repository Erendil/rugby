/**
  * @class ScrumState
  * @brief Etat de la cam�ra durant une m�l�e
  * @author Sylvain Lafon
  * @see CameraState
  */
public class ScrumState : CameraState
{
    public ScrumState(StateMachine sm, CameraManager cam) : base(sm, cam) { }
}