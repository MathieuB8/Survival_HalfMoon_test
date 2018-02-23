using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	public int damagePerAttack = 10;
	public float delayBetweenattack = 2f;


	private bool enemyInRange = false;
	private float timer = 0f;
	private Animator anim;
	private GameObject enemy;
	AudioSource swordSound;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Enemy")){
			enemy = other.gameObject;
			enemyInRange = true;	
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.CompareTag ("Enemy")){
			enemy = null;
			enemyInRange = false;	
		}
	}

	void Attack(){
		timer = 0f;
		anim.SetTrigger ("Attack");
		if (enemy != null){
			EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth> ();
			if (enemyHealth != null){
				enemyHealth.takeDamage (damagePerAttack);
				swordSound.Play ();
			}
		}
	}

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
		swordSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (Input.GetButtonDown("Fire1") && timer > delayBetweenattack){
			Attack();
		}
	}
}
