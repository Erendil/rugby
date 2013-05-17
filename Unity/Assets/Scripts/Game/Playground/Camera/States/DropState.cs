using UnityEngine;
/**
  * @class DropState
  * @brief Etat de la cam�ra durant un drop
  * @author Sylvain Lafon
  * @see CameraState
  */
public class DropState : CameraState
{
    public DropState(StateMachine sm, CameraManager cam) : base(sm, cam) { }
	
	public override void OnEnter ()
	{
		Debug.Log("Drop In");
	}
}