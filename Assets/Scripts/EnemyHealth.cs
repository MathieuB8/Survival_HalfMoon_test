using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int maxHealth = 100;

	private int health;
	private Transform tf;
	private AudioSource audioS;
	private GameObject player;
	private EnemyPassiveAggresive EnemyAggressiveComp;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player"))
			player = other.gameObject;
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player"))
			player = null;
	}

	IEnumerator PlayAndDestroy(AudioSource audio, GameObject gameO){
		audio.Play();
		yield return new WaitForSeconds(audio.clip.length);
		player.GetComponent<PlayerAttack> ().UpdateNumberEnemiesKilled ();
		gameObject.SetActive(false); // can't destroy it right now because of the call UpdateNumberEnemiesKilled
	}

	public void takeDamage(int amount){
		health -= amount;
		if (health <= 0){
			StartCoroutine(PlayAndDestroy(audioS, gameObject));
		}
		EnemyAggressiveComp.EnableAggro (true);
	}

	void Start () {
		health = maxHealth;
		audioS = gameObject.GetComponent<AudioSource> ();
		tf = GetComponent<Transform> ();
		EnemyAggressiveComp = GetComponent<EnemyPassiveAggresive> ();
	}

	void OnDisable(){
		Destroy(gameObject);
	}
}
