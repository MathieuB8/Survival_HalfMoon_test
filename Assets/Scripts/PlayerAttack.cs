using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {
	public int damagePerAttack = 10;
	public float delayBetweenattack = 2f;


	private int number_enemies_killed = 0;
	private float timer = 0f;
	private Animator anim;
	private GameObject enemy;
	AudioSource swordSound;
	private Text playerMessage;
	private GameObject MonstersGen;

	IEnumerator ShowMessage(string message, float delay){
		playerMessage.text = message;
		yield return new WaitForSeconds (delay);
		playerMessage.text = "";
	}

	IEnumerator ShowMessages(List<string> messages, float delay){
		for (int i = 0; i < messages.Count; i++){
			yield return ShowMessage (messages [i], delay);
		}
	}

	private void MessagesTriggered(){
		List<string> strList = new List<string>();
		if (number_enemies_killed == 1){
			strList.Add("I am a monster...");
			strList.Add("Why did I kill it...");
			StartCoroutine (ShowMessages(strList, 3f)); 
		} else if (number_enemies_killed == 2) {
			strList.Clear ();
			strList.Add("I did it again...");
			strList.Add ("I'm addicted to it..");
			StartCoroutine (ShowMessages (strList, 3f));
			MonstersGen.GetComponent<MonstersGenerator> ().SummonBoss ();
		}
	}

	public void UpdateNumberEnemiesKilled(){
		number_enemies_killed+=1;
		MessagesTriggered();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Enemy"))
			enemy = other.gameObject;
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.CompareTag ("Enemy"))
			enemy = null;
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

	void Start () {
		anim = GetComponentInChildren<Animator> ();
		swordSound = GetComponents<AudioSource> ()[0];
		playerMessage = GetComponentInChildren<Text> ();
		MonstersGen = GameObject.Find ("MonsterGenerator");
	}

	void Update () {
		timer += Time.deltaTime;

		if (Input.GetButtonDown("Fire1") && timer > delayBetweenattack){
			Attack();
		}
	}
}
