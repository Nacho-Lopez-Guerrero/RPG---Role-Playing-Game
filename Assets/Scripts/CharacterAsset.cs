using UnityEngine;
using System.Collections;

public class CharacterAsset : MonoBehaviour 
{
	public GameObject[] characterMesh;	
	public GameObject[] weaponMesh;
	public GameObject[] hairMesh;
	public Material[] torsoMaterial;
	public Material[] legMaterial;
	public Material[] feetMaterial;
	public Material[] handsMaterial;
	public Material[] faceMaterial;

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
