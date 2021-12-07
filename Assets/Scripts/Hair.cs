using UnityEngine;

public class Hair 
{
	public int hairColorIndex;
	public int hairMeshIndex;
	public GameObject hairStyle;

	public Rect position;
	public int offset;

	private int _numberOfHairColors;
	private int _numberOfHairMeshes;

	private Vector2 _hairStyleButtonSize;
	private Vector2 _hairColorButtonSize;
	private Texture[] _hairColorTextures;

	public Hair()
	{
		offset = 5;
		_numberOfHairColors = 4;
		_numberOfHairMeshes = 3;
		hairColorIndex = 0;
		hairMeshIndex = 0;
		_hairColorTextures = new Texture[_numberOfHairColors];

		position = new Rect(140, 50, 100, 50);
		_hairColorButtonSize = new Vector2((position.width - (offset * 2)) / _numberOfHairColors, (position.height - (offset * 2)) / 2);
		_hairStyleButtonSize = new Vector2((position.width - (offset * 2)) / 2, (position.height - (offset * 2)) / 2);
	}

	public void OnGUI()
	{
		GUI.BeginGroup(position);
		GUI.Box(new Rect(0, 0, position.width, position.height), "");
		HairColorButtons();
		NextHairStyle();
		PreviousHairStyle();
		GUI.EndGroup();
	}

	private void HairColorButtons()
	{
		if(_hairColorTextures[0] == null)
			LoadHairColorTexture();

		for(int cnt = 0; cnt < _numberOfHairColors; cnt++)
		{
			if(GUI.Button(new Rect(offset + (_hairColorButtonSize.x * cnt), offset, _hairColorButtonSize.x, _hairColorButtonSize.y), _hairColorTextures[cnt], "box"))
			{
				hairColorIndex = cnt;
				hairStyle.GetComponent<Renderer>().material.mainTexture = _hairColorTextures[hairColorIndex];
			}
		}
	}

	private void PreviousHairStyle()
	{
		if(GUI.Button(new Rect(offset, position.height - _hairStyleButtonSize.y - offset, _hairStyleButtonSize.x, _hairStyleButtonSize.y), "<"))
		{
			hairMeshIndex--;
			if(hairMeshIndex < 0)
				hairMeshIndex = _numberOfHairMeshes - 1;

			LoadHairMesh();
		}
	}

	private void NextHairStyle()
	{
		if(GUI.Button(new Rect(offset + _hairStyleButtonSize.x, position.height - _hairStyleButtonSize.y - offset, _hairStyleButtonSize.x, _hairStyleButtonSize.y), ">"))
		{
			hairMeshIndex++;
			if(hairMeshIndex > _numberOfHairMeshes - 1)
				hairMeshIndex = 0;

			LoadHairMesh();
		}
	}	

	private void LoadHairMesh()
	{

//		GameObject mount = PlayerModelCustomization.characterMesh.GetComponent<PlayerCharacter>().hairMount;

		if(PC.Instance.hairMount.transform.childCount > 0)
			Object.Destroy(PC.Instance.hairMount.transform.GetChild(0).gameObject);
		
		hairStyle = Object.Instantiate( Resources.Load(GameSettings2.HUMAN_MALE_HAIR_MESH_PATH + "Hair_" + hairMeshIndex), 
		                               PC.Instance.hairMount.transform.position, 
		                               PC.Instance.hairMount.transform.rotation) as GameObject;				//Use Object class because class dont descend from monobeahviour

		hairStyle.transform.parent = PC.Instance.hairMount.transform;

		hairStyle.GetComponent<Renderer>().material.mainTexture = _hairColorTextures[hairColorIndex];

		MeshOffset mo = hairStyle.GetComponent<MeshOffset>();
		if(mo == null)
			return;

		hairStyle.transform.localPosition = mo.positionalOffset;
		hairStyle.transform.localRotation = Quaternion.Euler(mo.rotationalOffset);
		hairStyle.transform.localScale = mo.scaleOffset;

	}

	private void LoadHairColorTexture()
	{
		for(int cnt = 0; cnt < _hairColorTextures.Length; cnt++)
		{
			_hairColorTextures[cnt] = Resources.Load(GameSettings2.HUMAN_MALE_HAIR_COLOR_PATH + ((HairColorNames)cnt).ToString()) as Texture;
		}
	}

	public void LoadInitialHair()
	{
		if(_hairColorTextures[0] == null)
			LoadHairColorTexture();

		LoadHairMesh();
	}
}


public enum HairColorNames
{
	Black,
	Brown,
	Blonde,
	Orange
}