/**
  * @class IntroCameraState
  * @brief Etat de la cam�ra au d�part
  * @author Sylvain Lafon
  * @see MonoBehaviour
  */
public class IntroCameraState : CameraState
{
    public IntroCameraState(StateMachine sm, CameraManager cam) : base(sm, cam) { }
}