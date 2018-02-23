using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
	
	public int maxHealth = 100;
	protected int health;
	protected AudioSource audioS;

	public virtual IEnumerator PlayAndDestroy(AudioSource audio, GameObject gameO){
		audio.Play();
		yield return new WaitForSeconds(audio.clip.length);
		gameObject.SetActive(false); // can't destroy it right now because of the call UpdateNumberEnemiesKilled
	}

	public virtual void takeDamage(int amount){
		health -= amount;
		if (health <= 0){
			StartCoroutine(PlayAndDestroy(audioS, gameObject));
		}
	}

	void Awake(){
		health = maxHealth;
	}

	protected void Start () {
		audioS = gameObject.GetComponent<AudioSource> ();
	}

}
