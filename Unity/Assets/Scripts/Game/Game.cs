using UnityEngine;
using System.Collections;

/**
 * @class Game
 * @brief Classe principale du jeu
 * @author Sylvain Lafon
 */
[AddComponentMenu("Scripts/Game/Game")]
public class Game : MonoBehaviour {

    public GameSettings settings;

    public GameObject limiteTerrainNordEst;
    public GameObject limiteTerrainSudOuest;

    public Team right;
    public Team left;
       
    public Gamer p1, p2;

    public Ball Ball;

    private Team Owner;
    
	void Start ()
    {
        right.Game = this;
        left.Game = this;
        right.right = true;
        left.right = false;
        right.CreateUnits();
        left.CreateUnits();

        p1 = right.gameObject.AddComponent<Gamer>();
        p1.game = this;
        p1.team = right;
        p1.controlled = right[0];
        p1.inputs = settings.inputs;

        p2 = left.gameObject.AddComponent<Gamer>();
        p2.game = this;
        p2.team = left;
        p2.controlled = left[0];
        p2.inputs = settings.inputs2;

        this.Owner = p1.controlled.Team;
        Ball.Game = this;
        Ball.transform.parent = p1.controlled.BallPlaceHolder.transform;
        Ball.transform.localPosition = Vector3.zero;
        Ball.Owner = p1.controlled;        
       
        Camera.mainCamera.transform.rotation = Quaternion.Euler(new Vector3(28.57f, 0f, 0f));                
	}

    void Update()
    {
        positionneCamera();       
	}

    void positionneCamera()
    {
        // TODO : Changer de place, rendre customizable.
        // Synopsis : Positionne la cam�ra derri�re le joueur s�lectionn� par le joueur courant.
        Vector3 ecart = new Vector3(1.32f, 16.91f, -9.73f);
        Vector3 test = new Vector3(
            ecart.x * Camera.mainCamera.transform.forward.x,
            ecart.y * Camera.mainCamera.transform.forward.y,
            -ecart.z * Camera.mainCamera.transform.forward.z
        );

        if(Ball.Owner)
            Camera.mainCamera.transform.position = Ball.Owner.transform.position - test;
        else
            Camera.mainCamera.transform.position = Ball.transform.position - test;
    }

    public void OwnerChanged(Unit before, Unit after)
    {
        if (after != null)
        {
            if (after.Team != Owner)
            {
                Owner = after.Team;
                Camera.mainCamera.GetComponent<rotateMe>().rotate(new Vector3(0, 1, 0), 180);
            }

            // PATCH
            // p1.controlled = after;
            if (after.Team == right) p1.controlled = after;
            else p2.controlled = after;
        }
        
        this.left.OwnerChanged();
        this.right.OwnerChanged();       
    }
}
