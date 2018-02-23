using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : HealthSystem {
	public GameObject healthBar;
	public Image bgImage;
	public Image testImage;
	//public Text lifeText;

	private GameObject player;
	private EnemyPassiveAggresive EnemyAggressiveComp;
	private bool isDieing = false;
	private Slider healthBarSlider;

	private GameObject worldCanvas;
	private GameObject healthBarSpawned;
	private Image bgHealthBar;
	private Image testHealthBar;
	//private Text hpText;

	private string unitType;

	void RepositionHealthBar (){
		//hpText.transform.position = gameObject.transform.position - new Vector3 (-1f, -.7f, 0f);
		if (unitType == "knight"){
			bgHealthBar.transform.position = gameObject.transform.position - new Vector3 (-1f, -.7f, 0f);
			testHealthBar.transform.position = gameObject.transform.position - new Vector3 (-1f, -.7f, 0f);	
		}
		else{
			bgHealthBar.transform.position = gameObject.transform.position - new Vector3 (0f, -.7f, 0f);
			testHealthBar.transform.position = gameObject.transform.position - new Vector3 (0f, -.7f, 0f);	
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player"))
			player = other.gameObject;
	}

	public override IEnumerator PlayAndDestroy(AudioSource audio, GameObject gameO){
		if (isDieing == false){ // otherwise players can chain hit him and triggers it multiple times before it dies
			isDieing = true;
			bgHealthBar.enabled = false;
			testHealthBar.enabled = false;
			EnemyAggressiveComp.UpdateIsDieing ();
			audio.Play();
			yield return new WaitForSeconds(audio.clip.length);
			if (player != null)
				player.GetComponent<PlayerAttack> ().UpdateNumberEnemiesKilled ();
			gameObject.SetActive(false); // can't destroy it right now because of the call UpdateNumberEnemiesKilled
		}	
	}

	public new void takeDamage(int amount){
		base.takeDamage (amount);
		adjustHealthBar();
		EnemyAggressiveComp.EnableAggro (true);
	}

	void InitHealthBar(){
		worldCanvas = GameObject.Find ("WorldCanvas");
		bgHealthBar = Instantiate(bgImage);
		testHealthBar = Instantiate(testImage);
		bgHealthBar.transform.SetParent(worldCanvas.transform);
		testHealthBar.transform.SetParent(worldCanvas.transform);
		//hpText = Instantiate (lifeText);
		//hpText.transform.SetParent (worldCanvas.transform);
		RepositionHealthBar ();
	}
	void adjustHealthBar(){
		testHealthBar.fillAmount = (float) health / (float)maxHealth;
	}
	new void Start () {
		base.Start ();
		EnemyAggressiveComp = GetComponent<EnemyPassiveAggresive> ();
		unitType = EnemyAggressiveComp.unitType;
		InitHealthBar();

	}

	void OnDisable(){
		Destroy(testHealthBar.gameObject);
		Destroy(bgHealthBar.gameObject);
		Destroy(gameObject);
	}

	void Update(){
		RepositionHealthBar ();
	}
}
