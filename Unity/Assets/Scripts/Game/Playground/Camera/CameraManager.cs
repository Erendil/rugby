using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Camera/CameraManager")]
public class CameraManager : myMonoBehaviour {
	
	public Game game {get; set;}
	public TouchCamera touchCamera;
	public GameCamera gameCamera;
	public ScrumCamera scrumCamera;
	public TransfoCamera transfoCamera;

	// Use this for initialization
	void Start () {
		gameCamera.cameraManager = this;
		scrumCamera.cameraManager = this;
	}
	
	public void OnOwnerChanged()
    {	
		gameCamera.OnOwnerChanged();
	}
	
	public void OnScrum(bool active) {
		
		scrumCamera.gameObject.SetActive(active);
		if(active)
			scrumCamera.Activate();
			
	}
}
