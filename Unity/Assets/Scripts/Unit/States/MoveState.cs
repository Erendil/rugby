using UnityEngine;
using System.Collections;

/**
 * @class MoveState
 * @brief Etat d�placement : d�place une unit� � une position donn�e
 * @author Sylvain Lafon
 */
public class MoveState : UnitState {

    const float epsilon = 0.1f;
       
    public MoveState(StateMachine sm, Unit unit) : base(sm, unit) { }

    public override void OnEnter()
    {
        unit.GetNMA().stoppingDistance = 0f;
        unit.GetNMA().SetDestination(unit.GetOrder().point);
    }

    public override void OnUpdate()
    {
        if (unit.GetNMA().remainingDistance < epsilon)
        {
            unit.GetNMA().Stop();
            unit.ChangeOrder(Order.OrderNothing());
        }
    }
}
