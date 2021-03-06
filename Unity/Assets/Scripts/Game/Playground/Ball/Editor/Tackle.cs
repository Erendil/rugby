using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(Team))]
public class Tackle : Editor
{
	Team team;
	public float coneSize = 2f;
	public Color col;
	
	void OnEnable()
	{
		team = (Team)target;
	}
	
	void OnSceneGUI ()
	{
		if (team && team.nbUnits != 0)
		{
			Handles.color = team.ConeTackle;
			foreach (Unit unit in team)
			{
				Handles.DrawSolidArc(unit.transform.position, Vector3.up, unit.transform.forward, -team.AngleOfFovTackleCrit, team.unitTackleRange / 22f);
				Handles.DrawSolidArc(unit.transform.position, Vector3.up, unit.transform.forward, team.AngleOfFovTackleCrit, team.unitTackleRange / 22f);
			}

			Handles.color = team.DiscTackle;
			foreach (Unit unit in team)
			{
				Handles.DrawSolidDisc(unit.transform.position, Vector3.up, team.unitTackleRange / 22f);
			}

		}
	}

	public override void OnInspectorGUI()
	{
        EditorGUIUtility.LookLikeInspector();
        DrawDefaultInspector();
		
        // In Team vars ^^
        // team.ConeTackle = EditorGUILayout.ColorField("[Handles] Cone tackle color", team.ConeTackle);
        // team.DiscTackle = EditorGUILayout.ColorField("[Handles] Disc tackle color", team.DiscTackle);
	}
}
