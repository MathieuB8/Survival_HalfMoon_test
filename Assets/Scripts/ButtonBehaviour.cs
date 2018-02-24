using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour {

	private Button btn;
	// Use this for initialization
	protected virtual void ClickedBtn(){}

	void Awake () {
		btn = GetComponent<Button> ();
		btn.onClick.AddListener (ClickedBtn);
	}
}
