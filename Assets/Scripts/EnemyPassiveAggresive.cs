using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPassiveAggresive : MonoBehaviour {
	private bool enemyInRange = false;
	public int damagePerAttack = 5;
	public float delayBetweenattack = 1f;
	private GameObject enemy;

	private  bool aggressiveState = false;
	private Animator anim;
	private Rigidbody2D rg2d;

	public void EnableAggro(bool state){
		aggressiveState = state;
	}


	void Move(float x, float y){
		anim.SetBool ("isRunning", true);
		rg2d.MovePosition (rg2d.position + new Vector2 (x, y /** Time.fixedDeltaTime*/));
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")){
			enemy = other.gameObject;
			enemyInRange = true;	
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			enemyInRange = false;	
		}
	}

	void Attack(){
		
	}
	void Start () {
		anim = GetComponent<Animator> ();
		rg2d = GetComponent<Rigidbody2D> ();
	}

	void LateUpdate () {
		if (aggressiveState){
			Attack ();
		}
	}
}
