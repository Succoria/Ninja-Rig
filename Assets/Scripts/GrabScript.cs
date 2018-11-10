using System.Collections;
using Rewired;
using UnityEngine;

public class GrabScript : MonoBehaviour
{

	public bool grabbed;
	RaycastHit2D hit;
	public float distance;
	public Transform holdpoint;
	public float throwforce;
	public LayerMask notgrabbed;

	public GameObject Ograbbed;
	private float posP;

	Player _input;
	public int _playerID;

	private Vector2 posPV;

	// Use this for initialization
	void Start ()
	{
		_input = ReInput.players.GetPlayer (_playerID);
	}

	// Update is called once per frame
	void Update ()

	{

		posP = transform.position.y + 2;
		posPV.x = transform.position.x;
		posPV.y = transform.position.y + 1;

		if (_input.GetButtonDown ("Throw"))
		{
			if (!grabbed)
			{
				Physics2D.queriesStartInColliders = false;

				hit = Physics2D.Raycast (posPV, Vector2.right * transform.localScale.x, distance);
				Debug.DrawRay (posPV, Vector2.right * transform.localScale.x, Color.green, distance);
				if (hit.collider != null && hit.collider.tag == "Grabbable")
				{
					grabbed = true;
					Ograbbed = hit.collider.gameObject;
					Debug.Log (Ograbbed);
					Ograbbed.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Static;
					//Ograbbed.layer = 13;

				}

				//grab
			}
			else if (!Physics2D.OverlapPoint (holdpoint.position, notgrabbed))
			{
				grabbed = false;

				if (hit.collider.gameObject.GetComponent<Rigidbody2D> () != null)
				{
					Ograbbed.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
					Ograbbed.layer = 9;
					hit.collider.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (transform.localScale.x, 0) * throwforce;

					Ograbbed = hit.collider.gameObject;
					Ograbbed.tag = "destroy";
					Ograbbed.GetComponent<MovingProj> ().MovingProjectile ();
					//Ograbbed = null;
				}

			}

		}

		if (Input.GetKeyDown (KeyCode.B))
		{

		}

		if (grabbed)
			hit.collider.gameObject.transform.position = holdpoint.position;

	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.green;

		Gizmos.DrawLine (posPV, posPV + Vector2.right * transform.localScale.x * distance);
	}

	public void Pickupitem ()
	{

	}
}