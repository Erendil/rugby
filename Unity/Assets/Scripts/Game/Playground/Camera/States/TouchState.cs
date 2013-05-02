/**
  * @class TouchState
  * @brief Etat de la cam�ra durant une touche
  * @author Sylvain Lafon
  * @see CameraState
  */
public class TouchState : CameraState
{
    public TouchState(StateMachine sm, CameraManager cam) : base(sm, cam) { }
}