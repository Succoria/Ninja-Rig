using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnplayer : MonoBehaviour {

	public GameObject player1;
	public GameObject sp;
	void Start () {
		Instantiate (player1, sp.transform.position, transform.rotation);

	}
}