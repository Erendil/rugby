using UnityEngine;
using System.Collections;

/**
  * @interface Debugable
  * @brief Interface � faire impl�menter par les Components qui veulent �tre affich�s dans DebugWindow
  * @see DebugWindow
  * @author Sylvain Lafon  
  */
public interface Debugable
{
    /// Ce qui sera affich� dans la DebugWindow.
    void ForDebugWindow();

    /*
     *  Vous pourrez ainsi afficher des informations et faire
     *  des entr�es utilisateurs pour d�bugger sans vous prendre la t�te !
     */

    /*
     * Exemple :
     * 
     * public void ForDebugWindow() {
     *     #if UNITY_EDITOR
     *          EditorGUILayout.LabelField("Plop, je m'appelle : " + this.name);
     *     #endif
     * }
     */
}