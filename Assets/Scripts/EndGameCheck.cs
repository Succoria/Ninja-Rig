using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCheck : MonoBehaviour
{

	public int currentPlayers;
	public GameObject canvas;

	// Use this for initialization
	void Start ()
	{
		currentPlayers = 4;
	}

	// Update is called once per frame
	void Update ()
	{
		if (currentPlayers == 1)
		{
			Application.LoadLevel ("End Game Screen");
		}
	}

	public void PlayerDeath ()
	{
		currentPlayers = currentPlayers - 1;
	}
}