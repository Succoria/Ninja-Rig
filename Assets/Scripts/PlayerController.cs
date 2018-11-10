using System.Collections;
using System.Collections.Generic;
//using HUD;
using Rewired;
using UnityEngine;

//namespace Playerctlr
//{

public class PlayerController : MonoBehaviour
{

	public static PlayerController _playercontroller;

	Rigidbody2D rb;
	public float RayD = 0.25f;
	public float moveMultiplier = 1f;
	public float jumpHeight = 4f;
	public bool grounded = true;
	public LayerMask layer;
	Transform FlipScale;
	Animator anim;
	public int jumpCount = 0;
	bool shoot = false;
	public bool drawRays;
	public GameObject peashot;
	public GameObject sp;
	public float rof;
	bool canShoot = true;
	public GameObject cam;
	public float health;
	public bool Airhit;
	public bool BAirT;
	//UIHUD _ui;
	int _hp = 2;
	Player _input;
	public int playerID;

	private Vector2 vloc2;

	private float vlocx;

	public float pveloc;

	private bool aMove;

	RaycastHit2D hit;

	private void Awake ()
	{
		_playercontroller = this;
		health = 100;
		aMove = true;

		//_ui = UIHUD.ui;
	}
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		FlipScale = GetComponent<Transform> ();
		shoot = false;
		canShoot = true;
		//Instantiate (cam, sp.transform.position, transform.rotation);
		_input = ReInput.players.GetPlayer (playerID);
		BAirT = false;
	}

	void FixedUpdate ()
	{

		// Visualising raycast
		//if (drawRays == true)
		//{
		//	Debug.DrawRay (transform.position, Vector2.down * RayD, Color.green, 1);
		//}
		//Raycast for ground
		Physics2D.queriesStartInColliders = false;
		hit = Physics2D.Raycast (transform.position, Vector2.down, RayD);

		//RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, RayD);
		Debug.DrawRay (transform.position, Vector2.down * RayD, Color.green, 1);
		//Debug.Log (hit);
		if (hit.collider != null && hit.collider.tag == "ground")
		{
			Debug.Log (hit);
			grounded = true;
			anim.SetBool ("grounded", grounded);
			anim.SetBool ("falling", false);
			if (rb.velocity.y <= 0.001f)
			{
				anim.SetBool ("Jump", false);
				jumpCount = 0;
			}
		}
		else if (hit.collider == null)
		{
			grounded = false;
			anim.SetBool ("falling", true);
			anim.SetBool ("grounded", grounded);
		}
	}
	void Update ()
	{

		if (BAirT == false)
		{
			float hInput = _input.GetAxis ("Horizontal");
			//if (hInput != 0) Debug.Log (playerID + ": " + hInput);
			bool jInput = _input.GetButtonDown ("Leap");
			//if (jInput) Debug.Log ("Jump: " + jInput);
			shoot = _input.GetButton ("Shoot");
			Move (hInput);
			Leap (jInput);
			Shoot (shoot);
			if (health == 0 || health < 0)
			{
				Destroy (gameObject);
				Application.LoadLevel (Application.loadedLevel);

			}
		}

	}
	void Leap (bool jInput)
	{
		if (jInput && jumpCount < 10)
		{
			//Debug.Log ("isJumping");
			rb.velocity = new Vector2 (0, jumpHeight);
			anim.SetBool ("Jump", true);
			jumpCount++;
		}
		if (rb.velocity.y < 0 && grounded == false)
		{
			anim.SetBool ("falling", true);
		}
	}
	void Move (float hInput)
	{
		if (hInput != 0)
		{
			//Debug.Log (hInput);

			if (hInput > 0)
			{
				FlipScale.localScale = new Vector2 (1, 1);
				rb.velocity = new Vector2 (hInput * moveMultiplier * Time.deltaTime, rb.velocity.y);
				//Debug.Log (playerID + " vel: " + rb.velocity);
				anim.SetFloat ("Speed", hInput);
			}
			else if (hInput < 0)
			{
				FlipScale.localScale = new Vector2 (-1, 1);
				rb.velocity = new Vector2 (hInput * moveMultiplier * Time.deltaTime, rb.velocity.y);
				//Debug.Log (playerID + " vel: " + rb.velocity);
				anim.SetFloat ("Speed", hInput);
			}
		}
		else
		{
			rb.velocity = new Vector2 (0, rb.velocity.y);
		}

	}
	void OnCollisionEnter2D (Collision2D other)
	{
		if ((other.transform.tag == "wall") && (BAirT == true))
		{
			//gameObject.GetComponent<Rigidbody2D> ().velocity = vloc2;

			if (pveloc < 0)
			{
				pveloc = pveloc * -1;
				health = health - (10 + pveloc);
				BAirT = false;

			}
			else
			{
				health = health - (10 + pveloc);
				BAirT = false;
			}

		}
		if (other.transform.tag == "Enemy")
		{

			if (_hp == 0)
			{
				//_ui.LooseLife ();
				Destroy (gameObject);
				//Debug.Log ("Hit");
			}
			else
			{
				_hp = _hp - 1;
				//_ui.LooseLife ();
			}
		}

		if (other.transform.tag == "destroy")
		{
			health = health - 10;
			pveloc = other.transform.GetComponent<Rigidbody2D> ().velocity.x;
			BAirT = true;
		}
	}
	void Shoot (bool shoot)
	{
		anim.SetBool ("Fire1", shoot);
		if (shoot)
		{
			//Debug.Log ("shoot");
			StartCoroutine (RateOfFire ());
		}

	}
	public void Airveloc ()
	{
		vlocx = pveloc;
		Debug.Log (vlocx);

	}
	public void DamageTaken ()
	{
		health = health - 10;
	}

	IEnumerator RateOfFire ()
	{
		while (shoot)
		{
			if (canShoot)
			{

				GameObject projectile = Instantiate (peashot, sp.transform.position, transform.rotation);
				if (transform.localScale.x < 0)
				{
					projectile.transform.Rotate (Vector2.up * 180);
				}
				canShoot = false;
				yield return new WaitForSeconds (rof);
				canShoot = true;
			}
			else
			{
				yield return null;
			}
		}

	}

	void OnCollisionExit2D (Collision2D other)
	{
		if (other.transform.tag == "destroy")
		{
			pveloc = 0;
			BAirT = false;

		}

	}

}