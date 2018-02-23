using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersGenerator : MonoBehaviour {
	public GameObject boss;

	private Transform monstersHolder; 

	public void SummonBoss(){
		GameObject instance = (GameObject) Instantiate (boss, new Vector3 (50f, 40f, 0f), new Quaternion(0,180,0,0));
		//monstersHolder = new GameObject ("MonsteresHolder").transform;
		//instance.transform.SetParent(monstersHolder);
		//if (instance != null)
			//if (instance.GetComponent<EnemyPassiveAggresive>() != null)
		instance.GetComponent<EnemyPassiveAggresive> ().AttackPlayerMode();
		instance.GetComponent<EnemyPassiveAggresive> ().EnableAggro (true);
		//else
		//	Debug.Log ("WUT???");
	}
}
