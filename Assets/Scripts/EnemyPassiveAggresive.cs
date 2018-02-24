using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPassiveAggresive : MonoBehaviour {
	private bool enemyInRange = false;
	public float speed = 2f;
	public int damagePerAttack = 1;
	public float delayBetweenattack = 2f;
	public string unitType = "";
	public Text talkingMobText;
	public LayerMask blockingLayer;

	private bool isDieing = false;
	private GameObject enemy;
	private bool aggressiveState = false;
	private Animator anim;
	private Rigidbody2D rg2d;
	private BoxCollider2D boxCollider;
	private CapsuleCollider2D capCollider;
	private float timer = 0f;
	bool facingleft = false;
	private AudioSource[] sounds;
	private Text msgMobText;
	private GameObject worldCanvas;

	private bool warningSent = false;

	public void EnableAggro(bool state){
		if (state == true && aggressiveState == false) {
			if (sounds.Length >= 2)
				sounds[1].Play ();
			else
				sounds[0].Play ();
		}
		aggressiveState = state;
	}

	public void AttackPlayerMode(){
		enemy = GameObject.FindGameObjectWithTag ("Player");
	}
	public void UpdateIsDieing(){
		isDieing = true;
	}

	void AttemptToMove(float xDir, float yDir){
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);
		if (unitType == "knight" && !warningSent){
			msgMobText.text = "I will avenge them, you monster !";
			warningSent = true;
		}
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

	void Move(float x, float y){
		anim.SetBool ("isMoving", true);
		msgMobText.transform.position = gameObject.transform.position - new Vector3 (-1f, -1.5f, 0f);
		//Debug.Log ("Moving to destination!");
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
		anim.SetBool ("isMoving", false);
		timer += Time.deltaTime;
		if(timer >= delayBetweenattack && !isDieing){
			anim.SetTrigger ("Attack");
			enemy.GetComponent<PlayerHealth> ().takeDamage (damagePerAttack);
			timer = 0;
		}
	}

	void AttemptAttack(){
		Vector2 targetpos = new Vector2 (enemy.transform.position.x, enemy.transform.position.y);
		Vector2 mypos = new Vector2 (transform.position.x, transform.position.y);
		if (unitType == "knight")
			mypos = mypos - new Vector2 (1, 2);
		Vector2 pos = new Vector2(targetpos.x, targetpos.y) - mypos;
		Vector2 target = new Vector2(0, 0);
		if (pos.x > 0.2)
			target.x = 1;
		else if (pos.x < -0.2)
			target.x = -1;
		if(pos.y > 0)
			target.y = 1;
		else if (pos.y < 0)
			target.y = -1;
		if (target.x > 0 && !facingleft){
			facingleft = !facingleft;
			gameObject.transform.Rotate (0, 180, 0);
		}
		if (target.x < 0 && facingleft){
			facingleft = !facingleft;
			gameObject.transform.Rotate (0, 180, 0);
		}
		if (enemyInRange)
			Attack();
		else{
			AttemptToMove (target.x * speed * Time.deltaTime, target.y * speed * Time.deltaTime);
		}
	}

	void Awake () { // Important to awake otherwise the monster generator script will crash when from it, you try to call one method of enemypassiveaggresive script
		timer = delayBetweenattack / 2;
		anim = GetComponent<Animator> ();
		rg2d = GetComponent<Rigidbody2D> ();
		boxCollider = GetComponent<BoxCollider2D> ();
		capCollider = GetComponent<CapsuleCollider2D> ();
		sounds = GetComponents<AudioSource> ();
		worldCanvas = GameObject.Find ("WorldCanvas");

		msgMobText = Instantiate (talkingMobText);
		msgMobText.transform.SetParent (worldCanvas.transform);
	}

	void LateUpdate () {
		if (aggressiveState && enemy){
			AttemptAttack ();
		}
		else if (!enemy)
			anim.SetBool ("isMoving", false);
	}

	void OnDisable(){
		if (msgMobText)
			Destroy (msgMobText.gameObject);
	}
}
