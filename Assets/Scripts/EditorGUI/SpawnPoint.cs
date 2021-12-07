using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour 
{
	public bool available = true;			//check this to see if we can spawn a new mob or not


	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube(transform.position, new Vector3(2, 2, 2));
//		Gizmos.DrawIcon(transform.position, "");
	}
}
