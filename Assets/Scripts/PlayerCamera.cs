using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class PlayerCamera : MonoBehaviour {

	public GameObject player;
	private void Awake() {
		player = PlayerController._playercontroller.gameObject;
	}
	void Update () {
		transform.position = new Vector3 (player.transform.position.x, 0, 0);
	}
}