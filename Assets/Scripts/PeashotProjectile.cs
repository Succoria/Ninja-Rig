using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeashotProjectile : MonoBehaviour {

	public float pSpeed;
	Animator anim;
	Rigidbody2D rb;
	public float counter;
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.AddForce (transform.right * pSpeed, ForceMode2D.Impulse);
		anim = GetComponent<Animator> ();
		StartCoroutine (Counter());
	}
	void OnTriggerEnter2D (Collider2D other) {
		anim.SetBool ("HitAnim", true);
		rb.velocity = Vector2.zero;
		StartCoroutine (WaitToDestroy());
	}
	IEnumerator WaitToDestroy () {
		bool finishedAnim = false;
		while (finishedAnim == false) {
			AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo (0);
			if (info.IsName ("DestroyPea")) {
				finishedAnim = true;
				Destroy (gameObject);
			}
			yield return null;
		}
	}
IEnumerator Counter(){
while (counter > 0)
{
	counter -= Time.deltaTime;
	yield return null;
}
Destroy (gameObject);
}
}