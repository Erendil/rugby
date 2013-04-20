using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

[AddComponentMenu("Scripts/Camera/CameraManager")]
public class CameraManager : myMonoBehaviour, Debugable {

    public Game game;
	public TouchCamera touchCamera;
	public GameCamera gameCamera;
	public ScrumCamera scrumCamera;
	public TransfoCamera transfoCamera;
	
	
	private Transform 	target;
	private Vector3 	velocity = Vector3.zero;
	private float[]		angleVelocity = new float[3];
	private float		angleVelocityX;
	private float		angleVelocityY;
	private float		angleVelocityZ;
	private float		actualDelay;
	private Quaternion	targetRotation;
	
	public  float		rotationDelay;
	private float 		rotationCurrentDelay;
	
	public  float 		smoothTime 	= 0.3f;
	public  Vector3 	smoothAngle = new Vector3(0.3f, 0.3f, 0.3f);
	public 	float		delay;
	public 	float		magnitudeGap;
	public  float 		rotationMagnitudeGap;
	public 	float		zoom;
	
	public 	Vector3		MaxfollowOffset;
	public 	Vector3		MinfollowOffset;
	

    public StateMachine sm;

	// Use this for initialization
	void Start () {

        sm.SetFirstState(new MainCameraState(sm, this));
		resetActualDelay();
		resetRotationDelay();
		
		/*
       
        /*
		gameCamera.cameraManager = this;
		scrumCamera.cameraManager = this;
		*/
		
	}
	
	void FixedUpdate(){

        if (target != null && Camera.mainCamera != null)
        {
			Vector3 targetPosition = target.TransformPoint(MaxfollowOffset);
			Vector3 offset = Camera.mainCamera.transform.position+(MinfollowOffset)*zoom;
			Vector3 result = Vector3.SmoothDamp(offset, targetPosition, ref velocity, smoothTime);
			Vector3 delta  = result- Camera.mainCamera.transform.position;

			
			//rotation
			targetRotation = Quaternion.LookRotation(target.position - Camera.mainCamera.transform.position, Vector3.up);
			
			Vector3 euler =  Camera.mainCamera.transform.rotation.eulerAngles;
			Vector3 tarEuler = targetRotation.eulerAngles;
			
			Vector3 angle = new Vector3(
				Mathf.SmoothDampAngle(euler.x, tarEuler.x, ref angleVelocity[0], smoothAngle.x),
				Mathf.SmoothDampAngle(euler.y, tarEuler.y, ref angleVelocity[1], smoothAngle.y),
				Mathf.SmoothDampAngle(euler.z, tarEuler.z, ref angleVelocity[2], smoothAngle.z)
			);
			
			
			
			if(angle.magnitude > rotationMagnitudeGap){
				if(rotationCurrentDelay >= rotationDelay){
					Camera.mainCamera.transform.rotation = Quaternion.Euler(angle.x, angle.y, angle.z);
					//pas besoin, c'est déjà fait !
        			//Camera.mainCamera.transform.LookAt(target);
				}
				else{
					rotationCurrentDelay += Time.deltaTime;
				}
			}else{
				resetRotationDelay();
			}
			
			if( delta.magnitude > magnitudeGap){
				if(actualDelay >= delay){
					Camera.mainCamera.transform.position = result;
					
				}else{
					actualDelay += Time.deltaTime;
				}
			}else{
				resetActualDelay();
			}		
		}
	}
	
	public void setTarget(Transform _t){
		target = _t;
		//resetActualDelay();
	}
	
	void resetActualDelay(){
		actualDelay = 0f;
	}
	
	void resetRotationDelay(){
		rotationCurrentDelay = 0f;
	}
	
	public void OnOwnerChanged(Unit old, Unit current)
    {	
		//gameCamera.OnOwnerChanged();
        sm.event_NewOwner(old, current);
	}
	
	public void OnScrum(bool active) {
		
		scrumCamera.gameObject.SetActive(active);
		if(active) {
			scrumCamera.Activate();
		}
		else {
			gameCamera.ResetRotation();			
			if(game.Ball.Owner.Team == game.left) {
				game.cameraManager.gameCamera.transform.RotateAround(new Vector3(0, 1, 0), Mathf.Deg2Rad * 180);	
			}
		}
			
	}

    public void OnPass(Unit from, Unit to)
    {
        sm.event_Pass(from, to);
    }

    public void ballOnGround(bool onGround)
    {
        sm.event_BallOnGround(onGround);
    }   

    public void ForDebugWindow()
    {
#if UNITY_EDITOR
        EditorGUILayout.LabelField("Current target", target == null ? "null" : target.name);
#endif
    }
}
