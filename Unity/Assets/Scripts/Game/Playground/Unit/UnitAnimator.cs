using UnityEngine;
using System.Collections.Generic;

/**
  * @class UnitAnimator
  * @brief Description.
  * @author Sylvain Lafon
  * @see MonoBehaviour
  */
[RequireComponent(typeof(Unit))]
public class UnitAnimator : MonoBehaviour {

    private Unit unit;
    public Animator animator;

    public void Start()
    {
        unit = this.GetComponent<Unit>();
        if (unit == null)
        {
            throw new UnityException("I need a unit");
        }
    }

    public void Update()
    {
        if (animator)
        {
            animator.SetFloat("speed", unit.nma.velocity.magnitude);
        }
    }
}
