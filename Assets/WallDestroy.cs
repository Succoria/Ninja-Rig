using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroy : MonoBehaviour
{
	public bool Moving;

	private float vlocx;

	private float xspeed;
	private float yspeed;

	private Vector2 projVeloc;

	// Use this for initialization
	void Start ()
	{
		Moving = false;
	}

	// Update is called once per frame
	void Update ()
	{
		xspeed = gameObject.GetComponent<Rigidbody2D> ().velocity.x;
		yspeed = gameObject.GetComponent<Rigidbody2D> ().velocity.y;

		if (xspeed > 10 || xspeed < -10)
		{
			gameObject.tag = "DangerWall";

		}

		if (yspeed > 10 || yspeed < -10)
		{
			gameObject.tag = "DangerWall";
		}

		if (xspeed < 10 && xspeed > -10 && yspeed < 10 && yspeed > -10)
		{
			gameObject.tag = "brokenwall";
		}
	}

	public void OnTriggerStay2D (Collider2D other)
	{
		if ((Moving == true) && (other.tag == "brokenwall"))
		{
			//other.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
			//other.GetComponent<Rigidbody2D> ().WakeUp ();
			other.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
			//other.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
		}

	}

	public void OnCollisionEnter2D (Collision2D proj)
	{

		if (proj.collider.tag == "destroy")
		{
			gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
			//gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;

			projVeloc = proj.gameObject.GetComponent<MovingProj> ().cVeloc;
			//gameObject.GetComponent<Rigidbody2D> ().WakeUp ();
			Moving = true;
			gameObject.GetComponent<Rigidbody2D> ().AddForce (projVeloc * 200, ForceMode2D.Impulse);
		}

	}
}