using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobGenerator : MonoBehaviour {

	public enum State 
	{
		Idle,
		Initialize,
		Setup,
		SpawnMobs
	}


	public GameObject[] mobPrefabs;				//An array to hold all of the prefabs of mobs we want to spawn
	public GameObject[] spawnPoints;			//This array will hold a reference to all the spawnpoints in the scene

	public State state;							//This is our local variable that holds our current state


	void Awake()
	{
		state = MobGenerator.State.Initialize;
	}


	// Use this for initialization
	IEnumerator Start () 
	{
		while(true)
		{
			switch(state)
			{
			case State.Initialize:
				Initialize();
				break;
			case State.Setup:
				Setup();
				break;
			case State.SpawnMobs:
				SpawnMob();
				break;

			}

			yield return 0;					//IEnumerator
		}
	}


	// Update is called once per frame
	void Update () {
	
	}


	//Make sure everything is initialized before going to the next step
	private void Initialize()
	{
		Debug.Log("****We are in Initialize() function!!****");

		if(!checkForMobPrefabs() || !checkForSpawnPoints())
			return;

		state = MobGenerator.State.Setup;
	}


	private void Setup()
	{
		Debug.Log("****We are in Setup() function!!****");

		state = MobGenerator.State.SpawnMobs;

	}


	private void SpawnMob()
	{
		Debug.Log("****Spawn Mob function ****");

		GameObject[] gos = AvailableSpawnPoints();

		for(int cnt = 0; cnt < gos.Length; cnt ++)
		{
			GameObject go = Instantiate(mobPrefabs[Random.Range(0,mobPrefabs.Length)], 
			                            gos[cnt].transform.position, 
			                            Quaternion.identity) as GameObject;
			go.transform.parent = gos[cnt].transform;
			
		}

		state = MobGenerator.State.Idle;
	}


	//Check if we have at least one mob prefab to spawn
	private bool checkForMobPrefabs()
	{
		if(mobPrefabs.Length > 0)
			return true;
		else 
			return false;
	}


	//check to see if we have at leaast one spawnpoint
	private bool checkForSpawnPoints()
	{
		if(spawnPoints.Length > 0)
		   	return true;
		else 
			return false;
	}


	//generate a list of available spawnpoints that do not have any mobs childed to it
	private GameObject[] AvailableSpawnPoints()
	{
		List<GameObject> gos = new List<GameObject>();

		for(int cnt = 0; cnt < spawnPoints.Length; cnt ++)
		{
			if(spawnPoints[cnt].transform.childCount == 0)
			{
				Debug.Log("**** SpawnPOint Available ****");
				gos.Add(spawnPoints[cnt]);
			}
		}

		return gos.ToArray();
	}
}
