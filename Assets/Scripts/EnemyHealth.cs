using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int health = 100;

	public void takeDamage(int amount){
		health -= amount;
		if (health <= 0){
			// animation pouf
			Destroy(gameObject);
		}
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
