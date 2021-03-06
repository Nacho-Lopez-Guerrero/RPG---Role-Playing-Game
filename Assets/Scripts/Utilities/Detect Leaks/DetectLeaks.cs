using UnityEngine;
using System.Collections;

public class DetectLeaks : MonoBehaviour {
	
	void OnGUI () 
	{
		GUILayout.Label("All " + FindObjectsOfTypeAll(typeof(UnityEngine.Object)).Length);
		GUILayout.Label("Textures " + FindObjectsOfTypeAll(typeof(Texture)).Length);
		GUILayout.Label("AudioClips " + FindObjectsOfTypeAll(typeof(AudioClip)).Length);
		GUILayout.Label("Meshes " + FindObjectsOfTypeAll(typeof(Mesh)).Length);
		GUILayout.Label("Materials " + FindObjectsOfTypeAll(typeof(Material)).Length);
		GUILayout.Label("GameObjects " + FindObjectsOfTypeAll(typeof(GameObject)).Length);
		GUILayout.Label("Components " + FindObjectsOfTypeAll(typeof(Component)).Length);
	}
}