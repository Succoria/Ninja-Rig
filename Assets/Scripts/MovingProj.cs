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
		if (gameObject.layer == 9)
		{
			if (gameObject.GetComponent<Rigidbody2D> ().velocity.x == 0)
			{
				Destroy (gameObject);
			}
		}
	}

	public void MovingProjectile ()
	{
		//transform.gameObject.tag = "destroy";
		gameObject.GetComponent<PaintBrushO> ().enabled = true;

	}
}