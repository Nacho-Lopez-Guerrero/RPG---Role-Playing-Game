using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public GameObject playerCharacter;
	public GameObject gameSttings;
//	public Camera mainCamera;

//	public float zOffset;
//	public float yOffset;
//	public float xRotOffset;

	private GameObject _pc;
	private PlayerCharacter _pcScript;

	public Vector3 _playerSpawnPointPos;		//This is the place in 3D Space where i want my player to spawn

	// Use this for initialization
	void Start () 
	{
//		LoadCharacter();


//		GameObject go = GameObject.Find(GameSettings.PLAYER_SPAWN_POINT);

//		_playerSpawnPointPos = new Vector3(520, 6, 140);		//Spawn 3D Space position

//		if(go == null)
//		{
//			Debug.LogWarning("Cannot find player SpawPoint!");

//			go = new GameObject(GameSettings.PLAYER_SPAWN_POINT);		//Creates an empty object with the name 'Player Spawn Point'
//			go.transform.position = _playerSpawnPointPos;
//		}

//		_pc = Instantiate(playerCharacter, go.transform.position, Quaternion.identity) as GameObject;
//		_pc.name = "pc";

//		_pcScript = _pc.GetComponent<PlayerCharacter>();

//		zOffset = -12.0f;
//		yOffset = 12.0f;
//		xRotOffset = 5.0f;

//		mainCamera.transform.position = new Vector3(_pc.transform.position.x, _pc.transform.position.y + yOffset, _pc.transform.position.z + zOffset);
//		mainCamera.transform.Rotate(xRotOffset, 0, 0);

	}
/*
	public void LoadCharacter()
	{
		GameObject gs = GameObject.Find("__GameSettings");
		if(gs == null)
		{
			GameObject gs1 = Instantiate(gameSttings, Vector3.zero, Quaternion.identity) as GameObject;
			gs1.name = "__GameSettings";
		}

		GameSettings gsCript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
		
		//Loading the character data
		gsCript.LoadCharacterData();
	}
*/

}
