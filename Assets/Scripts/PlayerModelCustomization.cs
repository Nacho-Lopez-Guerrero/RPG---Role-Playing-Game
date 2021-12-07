using UnityEngine;
using System.Collections;

public class PlayerModelCustomization : MonoBehaviour 
{
	public float rotationSpeed = 100;

	public bool _usingMaleModel = true;
	private int _index = 0;

	public static GameObject characterMesh;

	private Material _headMaterial;

	private bool _rotateMe = false;
	private bool _rotateClockwise = true;

	public static Vector2 _scale = new Vector2(1.8f, 1.8f);
	public static int skinColor = 1;
	public static int headIndex = 0;

	private static bool _update = false;

	private Hair _hair = new Hair();
	private bool _resetHair = true;

	// Use this for initialization
	void Start () 
	{
		if(GameSettings2.maleModels.Length < 1)
			Debug.LogWarning("We have no male models");
//		if(GameSettings2.femaleModels.Length < 1)
//			Debug.LogWarning("We have no female models");

		InstantiateCharacterModel();
	}

	void Update()
	{
		if(_rotateMe)
		{
			if(_rotateClockwise)
				transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
			else
				transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);

			_rotateMe = false;
		}

		if(characterMesh == null)
			return;

		characterMesh.transform.localScale = new Vector3(_scale.x, _scale.y, _scale.x);

		if(_update)
			UpdateHead();
	}

	public void OnEnable()
	{
		Messenger.AddListener("ToggleGender", OnToggleGender);
		Messenger<bool>.AddListener("RotatePlayerClockWise", OnRotateClockwise);
	}

	public void OnDisable()
	{
		Messenger.RemoveListener("ToggleGender", OnToggleGender);
		Messenger<bool>.AddListener("RotatePlayerClockWise", OnRotateClockwise);
	}

	private void InstantiateCharacterModel()
	{
		if(transform.childCount > 0)
			for(int cnt = 0; cnt < transform.childCount; cnt ++)
				Destroy(transform.GetChild(cnt).gameObject);
		
		if(_usingMaleModel)
			characterMesh = Instantiate(Resources.Load(GameSettings2.MALE_MODEL_PATH + GameSettings2.maleModels[_index]), transform.position, transform.rotation) as GameObject;
		else
			characterMesh = Instantiate(Resources.Load(GameSettings2.FEMALE_MODEL_PATH + GameSettings2.femaleModels[_index]), transform.position, transform.rotation) as GameObject;

		Destroy(characterMesh.GetComponent<PlayerInput>());

		MeshOffset mo = characterMesh.GetComponent<MeshOffset>();
	
		characterMesh.transform.parent = transform;

		if(mo != null)
		{
			mo.transform.position = new Vector3(
				mo.transform.position.x + mo.positionalOffset.x,
				mo.transform.position.y + mo.positionalOffset.y,
				mo.transform.position.z + mo.positionalOffset.z);
		}
		

		if(characterMesh.GetComponent<Animation>() != null)
		{
			characterMesh.GetComponent<Animation>()["idle"].wrapMode = WrapMode.Loop;
			characterMesh.GetComponent<Animation>().Play("idle");
		}

	}

	private void OnRotateClockwise(bool clockwise)
	{
		_rotateMe = true;
		_rotateClockwise = clockwise;
		Debug.Log("OnRotateClockwise");
	}

	public void OnToggleGender()
	{
		_usingMaleModel = !_usingMaleModel;
		_index = 0;

		InstantiateCharacterModel();
	}

	public static void ChangePlayerSkinColor(int color)
	{
		//Store the color the player has selected
		skinColor = color;
		_update = true	;
		//Change to proper head and hands for the color
	}

	public static void ChangeHeadIndex(int index)
	{
		headIndex = index;
		Debug.Log("Head set to index: " + headIndex);
		_update = true;
	}

	public void UpdateHead()
	{
		_headMaterial = PC.Instance.characterMaterialMesh.GetComponent<Renderer>().materials[1];
		_headMaterial.mainTexture = Resources.Load(GameSettings2.HEAD_TEXTURE_PATH + "head_" + headIndex + "_" + skinColor + ".human") as Texture;
	}

	public void OnGUI()
	{
		if(_resetHair)
		{
			_hair.LoadInitialHair();
			_resetHair = false;
		}

		_hair.OnGUI();

		if(GUI.Button(new Rect(Screen.width - 55, Screen.height - 30, 50, 25), "Next"))
		{
			SaveCustomizations();
			Application.LoadLevel(GameSettings2.levelNames[3]);
		}
	}

	private void LoadSetting()
	{
		Debug.Log("Saved Value: " + GameSettings2.LoadHairColor());
	}

	private void SaveCustomizations()
	{
		GameSettings2.SaveHair(_hair.hairMeshIndex, _hair.hairColorIndex);
		GameSettings2.SaveCharacterScale(_scale.x, _scale.y);
		GameSettings2.SaveCharacterModelIndex(_index);
		GameSettings2.SaveSkinColor(skinColor);
		GameSettings2.SaveHeadlIndex(headIndex);

		GameSettings2.SaveGameVersion();

		Debug.Log(GameSettings2.LoadCharacterScale()[0] + " : " + GameSettings2.LoadCharacterScale()[1]);
	}

}
