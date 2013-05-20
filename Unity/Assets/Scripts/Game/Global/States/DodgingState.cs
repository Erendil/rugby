/**
  * @class DodgeState
  * @brief Etat de la cam�ra durant une esquive
  * @author Sylvain Lafon
  * @see GameState
  */
public class DodgingState : GameState
{
    public DodgingState(StateMachine _sm, CameraManager _cam, Game _game) : base(_sm, _cam, _game) { /*this.unit = unit;*/ }

    // Unit unit;
}