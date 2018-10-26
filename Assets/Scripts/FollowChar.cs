using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowChar : MonoBehaviour {

    public Transform PlayerX;
    private float xpos;
    private float ypos;
    public float zOffset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(PlayerX.transform.position.x, PlayerX.transform.position.y, zOffset);
  
	}
}
