using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {
	public int damagePerAttack = 10;
	public float delayBetweenattack = 2f;


	private int number_enemies_killed = 0;
	private bool enemyInRange = false;
	private float timer = 0f;
	private Animator anim;
	private GameObject enemy;
	AudioSource swordSound;
	private Text playerMessage;


	IEnumerator ShowMessage(string message, float delay){
		playerMessage.text = message;
		yield return new WaitForSeconds (delay);
		playerMessage.text = "";
	}
	private void MessagesTriggered(){
		if (number_enemies_killed == 1)
			StartCoroutine (ShowMessage ("I am a monster...", 3)); 
	}

	public void UpdateNumberEnemiesKilled(){
		number_enemies_killed+=1;
		if (number_enemies_killed == 1)
			MessagesTriggered();
	}

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
		playerMessage = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (Input.GetButtonDown("Fire1") && timer > delayBetweenattack){
			Attack();
		}
	}
}
