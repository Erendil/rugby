/**
 * @class GameState
 * @brief Etat de la cam�ra : patron de l'�tat pour la cam�ra
 * @author Sylvain Lafon
 */
public abstract class GameState : State
{
    protected CameraManager 	cam;
	protected Game			 	game;
	
    public GameState(StateMachine _sm, CameraManager _cam, Game _game)
        : base(_sm)
    {
        this.cam  = _cam;
		this.game = _game;
    }
}
