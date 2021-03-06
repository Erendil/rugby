using UnityEngine;

/**
 * @class FollowState
 * @brief Etat suivre : fait suivre une unité par une autre
 * @author Sylvain Lafon
 */
public class FollowState : UnitState {

    public FollowState(StateMachine sm, Unit unit) : base(sm, unit) { }

    Transform target;

    public override void OnEnter()
    {
        Order o = unit.Order;
        if (o.type == Order.TYPE.FOLLOW)
        {
            target = unit.Order.targetT;
        }
        if (o.type == Order.TYPE.SEARCH)
        {
            target = unit.game.Ball.transform;
        }
    }

    public override void OnUpdate()
    {
        Vector3 pos;
        if (target == null)
        {
            pos = unit.game.Ball.transform.position;
        }
        else
        {
            pos = target.transform.position;
        }

        unit.nma.stoppingDistance = 2;
        unit.nma.SetDestination(pos);
    }
}
