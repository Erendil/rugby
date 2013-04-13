/**
 * @class CameraState
 * @brief Etat de la cam�ra : patron de l'�tat pour la cam�ra
 * @author Sylvain Lafon
 */
public abstract class CameraState : State
{
    protected CameraManager cam;
    public CameraState(StateMachine sm, CameraManager cam)
        : base(sm)
    {
        this.cam = cam;
    }
}
