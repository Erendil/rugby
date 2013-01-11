using UnityEngine;
using System.Collections;

/**
 * @class UnitState
 * @brief Etat d'unit� : patron de l'�tat pour une unit�
 * @author Sylvain Lafon
 */
public abstract class UnitState : State {

    protected Unit unit;
    public UnitState(StateMachine sm, Unit unit) : base(sm)
    {
        this.unit = unit;
    }

}
