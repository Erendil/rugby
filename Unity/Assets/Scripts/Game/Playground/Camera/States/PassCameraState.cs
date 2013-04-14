using UnityEngine;
using System.Collections.Generic;

/**
  * @class PassCameraState
  * @brief Etat de la cam�ra lorsque l'on fait une passe
  * @author Sylvain Lafon
  * @see MonoBehaviour
  */
public class PassCameraState : CameraState {
    public PassCameraState(StateMachine sm, CameraManager cam) : base(sm, cam) { }
}
