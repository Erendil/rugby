using UnityEngine;
using System.Collections;
using XInputDotNetPure;

/*
 *@author Maxens Dubois 
 */
[AddComponentMenu("Scripts/Game/Scrum Controller")]
public class scrumController : myMonoBehaviour {

	public Camera cam;
	
	private Game 	_game;
	private Ball 	_ball;
	private Gamer 	_p1;
	private Gamer 	_p2;
	private Team	_t1;
	private Team	_t2;
	
	private int  playerScore;
	private bool playerSpecial;
	private int cpuScore;
	private bool inScrum;
	private	int currentFrameWait;
	private int frameToGo;
	
	private bool btnScrumNormalReleased = true, btnScrumSpecialReleased = true;
	
	/** tweak session **/
	public int scoreTarget 		= 1000;
	public int playerUp	   		= 15;
	public int specialLuck 		= 20;
	public int playerSpecialUp 	= 80;
	public int frameStart		= 30;
	
	public float rightGap = -27f;
	public float leftGap = 10f;
	
	private float zGap = 0f;
	private float offset = 0f;
	
	public float IAoffset = -0.05f;
	public float playerOffset = 0.5f;
		
	/*
 	 *@author Maxens Dubois 
 	 */
	void Start()
    {		
		_game 	= gameObject.GetComponent<Game>();
		_ball 	= _game.Ball;
		_p1 	= _game.p1;
		_p2	 	= _game.p2;
		_t1		= _game.left;
		_t2		= _game.right;
        Init();
	}
	
	/*
 	 *@author Sylvain Lafon
 	 */
    void Init()
    {
        playerScore		 = 1;
        cpuScore 		 = 1;
		currentFrameWait = 0;
        inScrum 		 = false;
        playerSpecial    = false;
		offset           = 0f;
		frameToGo 		 = frameStart;
    }
	
	
    /*
 	 *@author Maxens Dubois 
 	 */
	void Update () {
				
		if(!inScrum){
			if (_ball.getGoScrum())
			{
				inScrum = true;
				_game.state = Game.State.SCRUM;
				_ball.setGoScrum(false);
				
				
				_p1.stopMove();
				_game.disableIA = true;
				_game.cameraManager.OnScrum(true);
		    }
		}
		
		if(inScrum){
			
			currentFrameWait ++;
			playersInline(offset);	
			if(currentFrameWait > frameStart)
			{
                if (Input.GetKeyDown(_game.p1.Inputs.scrumNormal.keyboard) || _game.p1.XboxController.GetButtonDown(_game.p1.Inputs.scrumNormal.xbox))
                {
                    playerUpScore(playerUp);
                    if (Random.Range(1, specialLuck) == 1)
                    {
                        playerSpecial = true;
                    }
                }

                if (Input.GetKeyDown(_game.p1.Inputs.scrumExtra.keyboard) || _game.p1.XboxController.GetButtonDown(_game.p1.Inputs.scrumExtra.xbox))
                {
                    if (playerSpecial)
                    {
                        playerUpScore(playerSpecialUp);
                        playerSpecial = false;
                    }
                }
				
				cpuScore += Random.Range(0,4);
				offset += IAoffset;
				if(cpuScore > scoreTarget){
					//cpu win
					Debug.Log("cpu win");
					endScrum();
				}
			}else{
				frameToGo --;
			}
		}
	}
	
	public bool isInScrum() {
		return inScrum;
	}
	
	/*
 	 *@author Maxens Dubois 
 	 */
	void changeZpos(float rightGap, float leftGap){
		
		
		Vector3 pos = _t2.transform.position;
		pos.z = _ball.transform.position.z + rightGap;
		_t2.transform.position = pos;
		
		pos = _t1.transform.position;
		pos.z = _ball.transform.position.z + leftGap;
		_t1.transform.position = pos;
	}
	
	/*
 	 *@author Maxens Dubois 
 	 */
	void playerUpScore(int up){
		playerScore += up;
		if(playerScore > scoreTarget){
			//player Win !
			Debug.Log("player win");
			//Vector3 ballPos = new Vector3(0,0,_t1.transform.position.z+10);
			endScrum();
		}
		offset += playerOffset;
		//_t1.transform.Translate(new Vector3(0f,0f,1f));
		//_t2.transform.Translate(new Vector3(0f,0f,1f));
		//_ball.transform.Translate(new Vector3(0f,0f,0.5f));
	}
	
	/*
 	 *@author Maxens Dubois 
 	 */
	void endScrum(){
		inScrum = false;
		_game.state = Game.State.PLAYING;
		
		_p1.enableMove();
		_game.disableIA = false;
		_game.cameraManager.OnScrum(false);
		Init();
	}
	
	/*
 	 *@author Maxens Dubois 
 	 */
	void playersInline(float offset){
			
		//Debug.Log("ligne !");
		
		Unit cap1 = _t1[0];
		Unit cap2 = _t2[0];
				
		for(int i = 0; i < _t1.nbUnits; i++){
			Unit u1 = _t1[i];
			Unit u2 = _t2[i];
			
			if(cap1 != u1){
                Vector3 tPos = cap1.transform.position;

                int dif = u1.Team.GetLineNumber(u1, cap1);
                float x = 3 * dif;

                u1.GetNMA().stoppingDistance = 0;
                u1.GetNMA().SetDestination(new Vector3(tPos.x + x, 0, tPos.z+offset));
			}
			
			if(cap2 != u2){
                Vector3 tPos = cap2.transform.position;

                int dif = u2.Team.GetLineNumber(u2, cap2);
                float x = 3 * dif;

                u2.GetNMA().stoppingDistance = 0;
                u2.GetNMA().SetDestination(new Vector3(tPos.x + x, 0, tPos.z+offset));
			}
		}       
	}
		
	
	/*
	 * Needed for GUI
	 * maxens dubois
	 */
	public int GetPlayerScore(){
		return playerScore;
	}
	
	public int GetCpuScore(){
		return cpuScore;
	}
	
	public bool HasPlayerSpecial(){
		return playerSpecial;
	}
	
	public int GetFrameToGo(){
		return frameToGo;
	}
}
