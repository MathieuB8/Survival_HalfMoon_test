using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	//public Transform player;
	public GameObject player;

	private Transform tf_player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		tf_player = player.GetComponent<Transform> ();
		offset = transform.position - tf_player.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = tf_player.position + offset;
	}
}
