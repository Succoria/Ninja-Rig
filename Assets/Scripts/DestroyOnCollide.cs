using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour
{
	public GameObject Cobject;
	public float Bveloc;

	// Use this for initialization
	void Start ()
	{
		//gameObject.GetComponent<
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "destroy")
		{
			Bveloc = other.GetComponent<Rigidbody2D> ().velocity.x;
			gameObject.GetComponent<PlayerController> ().DamageTaken ();
			gameObject.GetComponent<PlayerController> ().BAirT = true;
			gameObject.GetComponent<PlayerController> ().Airveloc ();
			gameObject.GetComponent<PlayerController> ().pveloc = Bveloc;

		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "destroy")
		{
			gameObject.GetComponent<PlayerController> ().Airhit = true;

		}
	}
}