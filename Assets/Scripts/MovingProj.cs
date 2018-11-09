using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingProj : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void MovingProjectile ()
	{
		transform.gameObject.tag = "destroy";
		gameObject.GetComponent<PaintBrushO> ().fActive = true;

	}
}