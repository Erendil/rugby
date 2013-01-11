using UnityEngine;
using System.Collections;

/**
 * @interface Triggering
 * @brief Interface de la classe qui g�rera le d�clenchement
 * @author Sylvain Lafon
 */
public interface Triggering
{
    void Entered(Triggered o, Trigger t);
    void Inside(Triggered o, Trigger t);
    void Left(Triggered o, Trigger t);
}



