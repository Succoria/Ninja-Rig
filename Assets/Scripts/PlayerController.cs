using System.Collections;
using System.Collections.Generic;
//using HUD;
using UnityEngine;

namespace Player
{

	public class PlayerController : MonoBehaviour
	{

		public static PlayerController _playercontroller;

		private GameObject player;

		Rigidbody2D rb;
		public float RayD = 0.25f;
		public float moveMultiplier = 1f;
		public float jumpHeight = 4f;
		bool grounded = true;
		public LayerMask layer;
		Transform FlipScale;
		Animator anim;
		int jumpCount = 0;
		bool shoot = false;
		public bool drawRays;
		public GameObject peashot;
		public GameObject sp;
		public float rof;
		bool canShoot = true;
		public GameObject cam;
		public GameObject health;
		//UIHUD _ui;
		int _hp = 2;

		private void Awake ()
		{
			_playercontroller = this;
			//_ui = UIHUD.ui;
		}
		void Start ()
		{
			rb = GetComponent<Rigidbody2D> ();
			anim = GetComponent<Animator> ();
			FlipScale = GetComponent<Transform> ();
			shoot = false;
			canShoot = true;
			Instantiate (cam, sp.transform.position, transform.rotation);

		}

		void FixedUpdate ()
		{

			// Visualising raycast
			if (drawRays == true)
			{
				Debug.DrawRay (transform.position, Vector2.down * RayD, Color.green, 1);
			}
			//Raycast for ground
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, RayD, layer);
			if (hit.collider != null)
			{
				grounded = true;
				anim.SetBool ("grounded", grounded);
				anim.SetBool ("falling", false);
				if (rb.velocity.y <= 0.001f)
				{
					anim.SetBool ("Jump", false);
					jumpCount = 0;
				}
			}
			else
			{
				grounded = false;
				anim.SetBool ("falling", true);
				anim.SetBool ("grounded", grounded);
			}
		}
		void Update ()
		{
			float hInput = Input.GetAxis ("Horizontal") * moveMultiplier;
			bool jInput = Input.GetButtonDown ("Jump");
			shoot = Input.GetButton ("Fire1");
			Move (hInput);
			Jump (jInput);
			Shoot (shoot);

		}
		void Jump (bool jInput)
		{
			if (jInput && jumpCount < 2)
			{
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
				rb.velocity = new Vector2 (hInput, rb.velocity.y);
				anim.SetFloat ("Speed", hInput);
				if (hInput > 0)
				{
					FlipScale.localScale = new Vector2 (1, 1);

				}
				else if (hInput < 0)
				{
					FlipScale.localScale = new Vector2 (-1, 1);
				}
			}
		}
		void OnCollisionEnter2D (Collision2D other)
		{
			if (other.transform.tag == "Enemy")
			{

				if (_hp == 0)
				{
					//_ui.LooseLife ();
					Destroy (gameObject);
					Debug.Log ("Hit");
				}
				else
				{
					_hp = _hp - 1;
					//_ui.LooseLife ();
				}
			}
		}
		void Shoot (bool shoot)
		{

			anim.SetBool ("shoot", shoot);
			if (shoot)
			{
				StartCoroutine (RateOfFire ());
			}

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

	}
}