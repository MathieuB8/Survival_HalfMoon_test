using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : HealthSystem {

	private Slider healthSlider;
	private Text deathMsg;
	private GameObject deathUI;
	private AudioSource[] sounds;
	private GameObject damageReceived;
	private int damaged = 0;
	private Color flashColour = new Color (1f, 0f, 0f, 0.1f);
	private Image damageImage;

	public override IEnumerator PlayAndDestroy(AudioSource audio, GameObject gameO){
		sounds[1].Play();
		yield return new WaitForSeconds(audio.clip.length);
		gameObject.SetActive(false); // can't destroy it right now because of the call UpdateNumberEnemiesKilled
	}


	public override void takeDamage (int amount) {
		base.takeDamage (amount);
		if (damaged == 0)
			damaged = 1;
		if (healthSlider != null)
			healthSlider.value = healthSlider.value - amount;
	}

	new void Start () {
		healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
		healthSlider.maxValue = base.maxHealth;
		healthSlider.value = base.health;
		deathMsg = GameObject.Find ("DeathMessage").GetComponent<Text> ();
		deathUI = GameObject.Find ("DeathUI");
		deathUI.SetActive (false);
		sounds = GetComponents<AudioSource> ();
		damageReceived = GameObject.Find ("DamageReceived");
		damageImage = damageReceived.GetComponent<Image> ();
		base.Start ();
	}

	void OnDisable(){
		if (deathUI != null){
			deathUI.SetActive (true);
			deathMsg.text = "You died...";
		}
		Destroy(gameObject);
	}

	void Update(){
		if (damaged == 1)
			damageImage.color = flashColour;
		else
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, 5 * Time.deltaTime);
		damaged = 0;
	}
}
