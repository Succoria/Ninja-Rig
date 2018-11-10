using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroy : MonoBehaviour
{
	public bool Moving;

	private float vlocx;

	private Vector2 projVeloc;

	// Use this for initialization
	void Start ()
	{
		Moving = false;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void OnTriggerStay2D (Collider2D other)
	{
		if ((Moving == true) && (other.tag == "brokenwall"))
		{
			//other.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
			other.GetComponent<Rigidbody2D> ().WakeUp ();
		}

	}

	public void OnCollisionEnter2D (Collision2D proj)
	{

		if (proj.collider.tag == "destroy")
		{
			projVeloc = proj.gameObject.GetComponent<MovingProj> ().cVeloc;
			//gameObject.GetComponent<Rigidbody2D> ().WakeUp ();
			Moving = true;
			gameObject.GetComponent<Rigidbody2D> ().AddForce (projVeloc);
		}
	}
}