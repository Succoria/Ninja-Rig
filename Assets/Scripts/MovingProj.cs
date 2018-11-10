using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingProj : MonoBehaviour
{
	public Vector2 cVeloc;
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
				gameObject.tag = "Grabbable";
				gameObject.layer = 11;
			}
		}

		if (gameObject.tag == "destroy" && gameObject.GetComponent<Rigidbody2D> ().velocity.x == 0)
		{
			Destroy (gameObject);
		}

	}

	public void OnCollisionEnter2D (Collision2D other)
	{
		if (other.collider.tag == "player")
		{
			//Destroy (gameObject);
		}

	}

	public void MovingProjectile ()
	{
		//transform.gameObject.tag = "destroy";
		gameObject.GetComponent<PaintBrushO> ().enabled = true;
		cVeloc = gameObject.GetComponent<Rigidbody2D> ().velocity;

	}

	void OnCollisionExit2D (Collision2D other)
	{
		if (other.collider.tag == "brokenwall" || other.collider.tag == "wall")
		{
			gameObject.tag = "Grabbable";
			gameObject.layer = 11;
			gameObject.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Static;

		}
	}
}