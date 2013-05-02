/**
  * @class SprintState
  * @brief Etat de la cam�ra durant un sprint
  * @author Sylvain Lafon
  * @see CameraState
  */
public class SprintState : CameraState
{
    public SprintState(StateMachine sm, CameraManager cam, Unit u) : base(sm, cam) { this.unit = u; }

    Unit unit;

    // Check if the unit stops sprinting..
    public override bool OnSprint(Unit u, bool sprinting)
    {
        if (!sprinting && u == this.unit)
        {
            sm.state_kill_me(this);
            return true;
        }

        return false;
    }
}