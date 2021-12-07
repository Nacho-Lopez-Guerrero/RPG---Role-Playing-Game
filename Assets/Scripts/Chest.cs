using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(BoxCollider))]
[RequireComponent (typeof(AudioSource))]
public class Chest : MonoBehaviour 
{
	public enum State
	{
		Open,
		Close,
		InBetween
	}

	public State state;

	public AudioClip openSound;
	public AudioClip closeSound;
	public GameObject particleEffect;

	public GameObject[] parts;

	public float maxDistance = 10;								//maxDistance playe can open this chest

	public bool inUse = false;

	private Color[] _defaultColors;
	private GameObject _player;
	private Transform _myTransform;
	private bool _used = false;

	public List<Item> loot = new List<Item>();

	public static float defaultLifetime = 120;					//Chest'sLifetime in seconds
	private float _lifeTimer = 0;

	// Use this for initialization
	void Start () 
	{
		_myTransform = transform;
		state = State.Close;
		_defaultColors = new Color[parts.Length];
		particleEffect.active = false; 

		if(parts.Length > 0)
			for(int cnt = 0; cnt < _defaultColors.Length; cnt ++)
				_defaultColors[cnt] = parts[cnt].GetComponent<Renderer>().material.GetColor("_Color");
	}
	
	void Update()
	{
		_lifeTimer += Time.deltaTime;

		if(_lifeTimer > defaultLifetime && state == State.Close)
			DestroyChest();

		if(!inUse)
			return;

		if(_player == null)
			return;

		if(Vector3.Distance(_myTransform.position, _player.transform.position) > maxDistance)
			MyGUI.chest.ForceClose();
			//Messenger.Broadcast("CloseChest");
	}

	public void OnMouseEnter()
	{
		Debug.Log("Enter");
		Highlight(true);
	}

	public void OnMouseExit()
	{
		Debug.Log("Exit");
		Highlight(false);
	}

	public void OnMouseUp()									//On mouse Click
	{
		_player = GameObject.FindGameObjectWithTag("Player");
		Debug.Log("Up");

		GameObject go = GameObject.FindGameObjectWithTag("Player");

		if(go == null)
			return;

		if(Vector3.Distance(transform.position, go.transform.position) > maxDistance && !inUse)
			return;
	
		switch(state)
		{
		case State.Open:
			Debug.Log("Case open");
			state = Chest.State.InBetween;
//			StartCoroutine("Close");
			ForceClose();
			break;
		case State.Close:
			if(MyGUI.chest != null)
			{
				MyGUI.chest.ForceClose();
			}
			Debug.Log("Case close");
			state = Chest.State.InBetween;
			StartCoroutine("Open");
			break;
		}

	}
//		if(state == State.Close)
//			Open();
//		else
//			Close();


	private IEnumerator Open()
	{
		MyGUI.chest = this;

		inUse = true;

		GetComponent<Animation>().Play("Open");
		GetComponent<AudioSource>().PlayOneShot(openSound);
		particleEffect.active = true; 

		Debug.Log("Open");	

		yield return new WaitForSeconds(GetComponent<Animation>()["Open"].length);

		state = State.Open;
		if(!_used)
			PopulateChest(5);
		//		Messenger<int>.Broadcast("PopulateChest",5 ,MessengerMode.DONT_REQUIRE_LISTENER);
		Messenger.Broadcast("DisplayLoot");
	}


	private void PopulateChest(int x)
	{

		for(int cnt = 0; cnt < x; cnt++)
		{
			loot.Add(ItemGenerator.CreateItem());
		}

		_used = true;
	}


	private IEnumerator Close()
	{
		inUse = false;
		_player = null;
//		animation.Play("Close");
//		audio.PlayOneShot(closeSound);
		particleEffect.GetComponent<ParticleSystem>().enableEmission = false; 

		particleEffect.active = false; 

//		float tempTimer = animation["close"].length;

//		if(closeSound.length > tempTimer)
//		tempTimer = closeSound.length;
//		yield return WaitForSeconds(animation["Close"].length);

		yield return 2;

		state = State.Close;

		if(loot.Count == 0)
		{
			loot = null;
			DestroyChest();
		}
		
	}

	private void DestroyChest()
	{
		loot = null;
		Destroy(gameObject);
	}

	public void ForceClose()
	{
		Messenger.Broadcast("CloseChest");

		StopCoroutine("Open");
		StartCoroutine("Close");
	}

	private void Highlight(bool glow)
	{
		if(glow)
		{
			if(parts.Length > 0)
				for(int cnt = 0; cnt < _defaultColors.Length; cnt ++)
					parts[cnt].GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
		}
		else
		{
			if(parts.Length > 0)
				for(int cnt = 0; cnt < _defaultColors.Length; cnt ++)
					parts[cnt].GetComponent<Renderer>().material.SetColor("_Color", _defaultColors[cnt]);
		}


	}


}
