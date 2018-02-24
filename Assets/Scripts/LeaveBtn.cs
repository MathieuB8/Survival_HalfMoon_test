using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveBtn : ButtonBehaviour {

	protected override void ClickedBtn () {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
		
	}
}
