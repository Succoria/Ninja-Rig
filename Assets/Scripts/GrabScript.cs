using System.Collections;
using Rewired;
using UnityEngine;

public class GrabScript : MonoBehaviour
{

	public bool grabbed;
	RaycastHit2D hit;
	public float distance = 2f;
	public Transform holdpoint;
	public float throwforce;
	public LayerMask notgrabbed;

	Player _input;
	public int _playerID;

	// Use this for initialization
	void Start ()
	{
		_input = ReInput.players.GetPlayer (_playerID);
	}

	// Update is called once per frame
	void Update ()
	{
		if (_input.GetButtonDown ("Throw"))
		{
			if (!grabbed)
			{
				Physics2D.queriesStartInColliders = false;

				hit = Physics2D.Raycast (transform.position, Vector2.right * transform.localScale.x, distance);

				if (hit.collider != null && hit.collider.tag == "Grabbable")
				{
					grabbed = true;

				}

				//grab
			}
			else if (!Physics2D.OverlapPoint (holdpoint.position, notgrabbed))
			{
				grabbed = false;

				if (hit.collider.gameObject.GetComponent<Rigidbody2D> () != null)
				{

					hit.collider.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (transform.localScale.x, 0) * throwforce;
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

		Gizmos.DrawLine (transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
	}
}