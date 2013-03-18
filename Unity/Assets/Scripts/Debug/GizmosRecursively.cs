using UnityEngine;
using System.Collections.Generic;

/**
  * @class GizmosRecursively
  * @brief Description.
  * @author Sylvain Lafon
  * @see MonoBehaviour
  */
[AddComponentMenu("Scripts/Debug/Gizmos (recursively)")]
public class GizmosRecursively : MonoBehaviour {
	public Color [] colorPattern = {
		Color.white,
		Color.red,
		Color.blue,
		Color.green	
	};
	
	public float size;
	
	public void OnDrawGizmos() {		
		DrawRecursively(this.transform, 0);
	}
	
	private void DrawRecursively(Transform t, int level) {
		
		DrawGizmos(t, level);
		
		int childCount = t.childCount;
		for(int i = 0; i < childCount; i++) {
			DrawRecursively(t.GetChild(i), level+1);	
		}
	}
	
	private void DrawGizmos(Transform t, int level) {
		Color c = colorPattern[level % colorPattern.Length];
		Gizmos.color = c;
		Gizmos.DrawCube(t.transform.position, Vector3.one * size);
	}
}
