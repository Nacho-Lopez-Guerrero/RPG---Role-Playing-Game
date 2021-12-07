using UnityEngine;
using System.Collections;

public enum CharacterMeshMaterial
{
	Torso,
	Face,
	Head,
	Feet,
	Legs,
	Hands,
	COUNT
}

public class ChangingRoom : MonoBehaviour 
{
	private int _charModelIndex = 0;

	private int _weaponIndex = 0;
	private int _hairMeshIndex = 0;

	private int _torsoMaterialIndex = 0;
	private int _faceMaterialIndex = 0;
	private int _feetMaterialIndex = 0;
	private int _handsMaterialIndex = 0;
	private int _legseMaterialIndex = 0;
	
//	private PlayerCharacter _pc;
	private CharacterAsset _ca;
	private string _charModelName = "Samurai";
	private GameObject _characterMesh;

	// Use this for initialization
	void Start () 
	{

		_ca = GameObject.Find("Character Asset Manager").GetComponent<CharacterAsset>();

		InstantiateCharacterModel();
		RefreshCharacterMeshMaterials();
		InstantiateWeaponModel();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void InstantiateWeaponModel()
	{
		if(_weaponIndex > _ca.weaponMesh.Length - 1)
			_weaponIndex = 0;

		if(transform.childCount > 0)
			for(int cnt = 0; cnt < PC.Instance.weaponMount.transform.childCount; cnt ++)
				Destroy(PC.Instance.weaponMount.transform.GetChild(cnt).gameObject);

		GameObject w = Instantiate(_ca.weaponMesh[_weaponIndex], PC.Instance.weaponMount.transform.position, Quaternion.identity) as GameObject;
		w.transform.parent = PC.Instance.weaponMount.transform;
		w.transform.rotation = new Quaternion(0, 0, 0, 0);


	}

	private void ChangeWeaponMesh()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 70, 120, 30), _weaponIndex.ToString()))
		{
			_weaponIndex++;
			InstantiateWeaponModel();
		}
	}

	private void InstantiateCharacterModel()
	{
		switch(_charModelIndex)
		{
			case 1:
				_charModelName = "Knight";
				break;
			default:
				_charModelIndex = 0;
				_charModelName = "Samurai";
				break;
		}

		Quaternion oldRot;

		if(_characterMesh == null)
			oldRot = transform.rotation;
		else
			oldRot = _characterMesh.transform.rotation;

		if(transform.childCount > 0)
			for(int cnt = 0; cnt < transform.childCount; cnt ++)
				Destroy(transform.GetChild(cnt).gameObject);

		_characterMesh = Instantiate(_ca.characterMesh[_charModelIndex], transform.position, Quaternion.identity) as GameObject;
		_characterMesh.transform.parent = transform;

//		_model.transform.rotation = transform.rotation;
		_characterMesh.transform.rotation = oldRot;

		_characterMesh.GetComponent<Animation>()["idle"].wrapMode = WrapMode.Loop;
		_characterMesh.GetComponent<Animation>().Play("idle");

//		_pc = _characterMesh.GetComponent<PlayerCharacter>();

		RefreshCharacterMeshMaterials();
		InstantiateWeaponModel();
//		InstantiateHairModel();
		Resources.UnloadUnusedAssets();

	}

	void OnGUI()
	{
		ChangeCharacterMesh();
		ChangeFaceMaterialGUI();
		ChangeTorsoMaterialGUI();

		/*********************************************************************/
		/***	CODE READY FOR CHANGE FEET, LEGS, HANDS AND HAIR MATERIALS ***/
		/*********************************************************************
		ChangeHandMaterialGUI();
		ChangeLegsMaterialGUI();
		ChangeFeetMaterialGUI();
		ChangeHairMeshGUI();

		*******/
		ChangeWeaponMesh();
		RotateCharacterModel();
	}

	/*********************************************************************/
	/***	CODE READY FOR CHANGE FEET, LEGS, HANDS AND HAIR MATERIALS ***/
	/*********************************************************************

	private void InstantiateHairMesh()
	{
		if(_hairMeshIndex > ca.hairMesh.Length - 1)
			_hairMeshIndex = 0;
		
		if(transform.childCount > 0)
			for(int cnt = 0; cnt < pc.hairMount.transform.childCount; cnt ++)
				Destroy(pc.hairMount.transform.GetChild(cnt).gameObject);
		
		GameObject w = Instantiate(ca.hairMesh[_hairMeshIndex], pc.hairMount.transform.position, Quaternion.identity) as GameObject;
		w.transform.parent = pc.hairMount.transform;
		w.transform.rotation = new Quaternion(0, 0, 0, 0);

		MeshOffset mo = w.GetComponent<MeshOffset>();

		if(mo == null)
			return;

		w.transform.localPosition = mo.posOffset;

	}

	private void ChangeHairMeshGUI()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 105, 120, 30), _hairMeshIndex.ToString()))
		{
			_hairMeshIndex++;
			InstantiateHairMesh();
		}
	}
	********/

	private void ChangeTorsoMaterialGUI()
	{
		if(GUI.Button(new Rect(Screen.width * 0.5f - 95, Screen.height - 140, 30, 30), "<"))
		{
			_torsoMaterialIndex--;
			ChangeMeshMaterial(CharacterMeshMaterial.Torso);
		}		
		GUI.Box(new Rect(Screen.width / 2 - 60, Screen.height - 140, 120, 30), _torsoMaterialIndex.ToString());
		if(GUI.Button(new Rect(Screen.width * 0.5f + 65, Screen.height - 140, 30, 30), ">"))
		{
			_torsoMaterialIndex++;
			ChangeMeshMaterial(CharacterMeshMaterial.Torso);
		}	
	}

	private void ChangeFaceMaterialGUI()
	{
		if(GUI.Button(new Rect(Screen.width * 0.5f - 95, Screen.height - 175, 30, 30), "<"))
		{
			_faceMaterialIndex--;
			ChangeMeshMaterial(CharacterMeshMaterial.Face);
		}		
		GUI.Box(new Rect(Screen.width / 2 - 60, Screen.height - 175, 120, 30), _faceMaterialIndex.ToString());
		if(GUI.Button(new Rect(Screen.width * 0.5f + 65, Screen.height - 175, 30, 30), ">"))
		{
			_faceMaterialIndex++;
			ChangeMeshMaterial(CharacterMeshMaterial.Face);
		}	
	}

	/*********************************************************************/
	/***	CODE READY FOR CHANGE FEET, LEGS, HANDS AND HAIR MATERIALS ***/
	/*********************************************************************
	private void ChangeFeetMaterialGUI()
	{
		if(GUI.Button(new Rect(Screen.width * 0.5f - 95, Screen.height - 245, 30, 30), "<"))
		{
			_feetMaterialIndex--;
			ChangeMeshMaterial(CharacterMeshMaterial.Feet);
		}		
		GUI.Box(new Rect(Screen.width / 2 - 60, Screen.height - 245, 120, 30), _feetMaterialIndex.ToString());
		if(GUI.Button(new Rect(Screen.width * 0.5f + 65, Screen.height - 245, 30, 30), ">"))
		{
			_feetMaterialIndex++;
			ChangeMeshMaterial(CharacterMeshMaterial.Feet);
		}	
	}

	private void ChangeLegsMaterialGUI()
	{
		if(GUI.Button(new Rect(Screen.width * 0.5f - 95, Screen.height - 210, 30, 30), "<"))
		{
			_legseMaterialIndex--;
			ChangeMeshMaterial(CharacterMeshMaterial.Legs);
		}		
		GUI.Box(new Rect(Screen.width / 2 - 60, Screen.height - 210, 120, 30), _legseMaterialIndex.ToString());
		if(GUI.Button(new Rect(Screen.width * 0.5f + 65, Screen.height - 210, 30, 30), ">"))
		{
			_legseMaterialIndex++;
			ChangeMeshMaterial(CharacterMeshMaterial.Legs);
		}	
	}


	private void ChangeHandMaterialGUI()
	{
		if(GUI.Button(new Rect(Screen.width * 0.5f - 95, Screen.height - 105, 30, 30), "<"))
		{
			_handsMaterialIndex--;
			ChangeMeshMaterial(CharacterMeshMaterial.Hands);
		}		
		GUI.Box(new Rect(Screen.width / 2 - 60, Screen.height - 105, 120, 30), _handsMaterialIndex.ToString());
		if(GUI.Button(new Rect(Screen.width * 0.5f + 65, Screen.height - 105, 30, 30), ">"))
		{
			_handsMaterialIndex++;
			ChangeMeshMaterial(CharacterMeshMaterial.Hands);
		}	
	}
*****/

	private void RefreshCharacterMeshMaterials()
	{
		for(int cnt = 0; cnt < (int)CharacterMeshMaterial.COUNT; cnt++)
		{
			ChangeMeshMaterial((CharacterMeshMaterial)cnt);
		}
	}

	private void ChangeMeshMaterial(CharacterMeshMaterial cmm)
	{
		Material[] mats = PC.Instance.characterMaterialMesh.GetComponent<Renderer>().materials;

		for(int cnt = 0; cnt < PC.Instance.characterMaterialMesh.GetComponent<Renderer>().materials.Length; cnt++)
			mats[cnt] = PC.Instance.characterMaterialMesh.GetComponent<Renderer>().materials[cnt];		

		switch(cmm)
		{
		case CharacterMeshMaterial.Torso:
			if(_torsoMaterialIndex > _ca.torsoMaterial.Length - 1)
				_torsoMaterialIndex = 0;
			else if(_torsoMaterialIndex < 0)
				_torsoMaterialIndex = _ca.torsoMaterial.Length - 1;

			mats[(int)cmm] = _ca.torsoMaterial[_torsoMaterialIndex];
			break;

		case CharacterMeshMaterial.Face:
			if(_faceMaterialIndex > _ca.faceMaterial.Length - 1)
				_faceMaterialIndex = 0;
			else if(_faceMaterialIndex < 0)
				_faceMaterialIndex = _ca.faceMaterial.Length - 1;
			
			mats[(int)cmm] = _ca.faceMaterial[_faceMaterialIndex];
			break;

			/*********************************************************************/
			/***	CODE READY FOR CHANGE FEET, LEGS, HANDS AND HAIR MATERIALS ***/
			/*********************************************************************
		case CharacterMeshMaterial.Feet:
			if(_feetMaterialIndex > ca.feetMaterial.Length - 1)
				_feetMaterialIndex = 0;
			else if(_feetMaterialIndex < 0)
				_feetMaterialIndex = ca.feetMaterial.Length - 1;
			
			mats[(int)cmm] = ca.feetMaterial[_feetMaterialIndex];
			break;
		case CharacterMeshMaterial.Legs:
			if(_legseMaterialIndex > ca.legMaterial.Length - 1)
				_legseMaterialIndex = 0;
			else if(_legseMaterialIndex < 0)
				_legseMaterialIndex = ca.legMaterial.Length - 1;
			
			mats[(int)cmm] = ca.legMaterial[_legseMaterialIndex];
			break;
		case CharacterMeshMaterial.Hands:
			if(_handsMaterialIndex > ca.handsMaterial.Length - 1)
				_handsMaterialIndex = 0;
			else if(_handsMaterialIndex < 0)
				_handsMaterialIndex = ca.handsMaterial.Length - 1;
			
			mats[(int)cmm] = ca.handsMaterial[_handsMaterialIndex];
			break;
			*/
		}


//		DestroyImmediate(pc.characterMaterialMesh.renderer.materials[(int)cmm]);

		PC.Instance.characterMaterialMesh.GetComponent<Renderer>().materials = mats;


//		Resources.UnloadUnusedAssets();
	}

	private void RotateCharacterModel()
	{
		if(GUI.RepeatButton(new Rect(Screen.width * 0.5f - 95, Screen.height - 35, 30, 30), "<"))
			_characterMesh.transform.Rotate(Vector3.up * Time.deltaTime * 100);
		
		if(GUI.RepeatButton(new Rect(Screen.width * 0.5f + 65, Screen.height - 35, 30, 30), ">"))
			_characterMesh.transform.Rotate(Vector3.down * Time.deltaTime * 100);
	}

	private void ChangeCharacterMesh()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 35, 120, 30), _charModelName))
		{
			_charModelIndex++;
			InstantiateCharacterModel();
		}
	}
}
