/**
  * @class PlaqueState 
  * @brief Etat d'une unité plaquée
  * @author Sylvain Lafon
  */
class PlaqueState : UnitState
{
    public PlaqueState(StateMachine sm, Unit u) : base(sm, u) { }

    float t;

    public override void OnEnter()
    {
        t = 0;
        unit.GetNMA().Stop();

        if (unit == unit.Game.Ball.Owner)
        {
            unit.Game.Ball.Put();
        }
    }

    public override void OnUpdate()
    {
        t += UnityEngine.Time.deltaTime;
        if (t > unit.Game.settings.timePlaque)
        {
            sm.state_change_me(this, new MainState(sm, unit));
        }
    }
}

