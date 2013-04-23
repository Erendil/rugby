/**
  * @class TransfoState
  * @brief Etat de la cam�ra durant une transformation
  * @author Sylvain Lafon
  * @see CameraState
  */
public class TransfoState : CameraState
{
    public TransfoState(StateMachine sm, CameraManager cam) : base(sm, cam) { }
}