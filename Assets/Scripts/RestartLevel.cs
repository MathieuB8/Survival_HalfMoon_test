using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {
	Button restartBtn;

	void LoadOnClick(){
		Debug.Log("heyy!");
		SceneManager.LoadScene (0);
	}

	void Awake () {
		restartBtn = GetComponent<Button> ();
		restartBtn.onClick.AddListener(LoadOnClick);
	}
}
