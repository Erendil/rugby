using UnityEngine;
using System.Collections.Generic;

/**
  * @class GroundBallState
  * @brief Etat de la cam�ra lorsque la balle est par terre
  * @author Sylvain Lafon
  * @see MonoBehaviour
  */
public class GroundBallState : CameraState {

    public GroundBallState(StateMachine sm, CameraManager cam) : base(sm, cam) { }


}
