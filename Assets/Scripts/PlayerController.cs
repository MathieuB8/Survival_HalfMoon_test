using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 1;
	public LayerMask blockingLayer;

	bool facingleft = true;
	Animator anim;
	Transform tf;
	Rigidbody2D rg2d;
	BoxCollider2D boxCollider;
	CapsuleCollider2D capCollider;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
		tf = GetComponent<Transform> ();
		rg2d = GetComponent<Rigidbody2D> ();
		boxCollider = GetComponent<BoxCollider2D> ();
		capCollider = GetComponent<CapsuleCollider2D> ();
	}

	void Move(float x, float y){
		anim.SetBool ("isRunning", true);
		rg2d.MovePosition (rg2d.position + new Vector2 (x, y /** Time.fixedDeltaTime*/));
	}

	void AttemptToMove(float xDir, float yDir){
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);
		RaycastHit2D hit;
		boxCollider.enabled = false;
		capCollider.enabled = false;
		hit = Physics2D.Linecast (start, end, blockingLayer);
		boxCollider.enabled = true;
		capCollider.enabled = true;
		if (hit.transform == null){ // no gameobject found
			Move (xDir, yDir);
		}
	}

	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		/* Move*/
		if (h != 0 || v != 0) {
			if (h > 0 && facingleft){
				facingleft = !facingleft;
				tf.Rotate (0, 180, 0);
			}
			if (h < 0 && !facingleft){
				facingleft = !facingleft;
				tf.Rotate(0,180,0);
			}
			AttemptToMove (h * speed * Time.deltaTime, v * speed * Time.deltaTime);
		}
		else
			anim.SetBool ("isRunning", false);

		/* */
	}
}
