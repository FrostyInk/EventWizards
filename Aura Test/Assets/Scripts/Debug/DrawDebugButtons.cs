using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawDebugButtons : MonoBehaviour {

	public GameEvent OnTurnChanged;

	const float m_buttonWidth = 100f;
	const float m_buttonHeight = 50f;
	const float m_offset = 10f;

	Rect rect = new Rect(Screen.width - (m_buttonWidth + m_offset), Screen.height - (m_buttonHeight + m_offset), m_buttonWidth, m_buttonHeight);
	Rect rect2 = new Rect(Screen.width - (m_buttonWidth + m_offset + m_buttonWidth), Screen.height - (m_buttonHeight + m_offset), m_buttonWidth, m_buttonHeight);

	public void Update() {
		if (Input.GetKeyDown(KeyCode.E)) {
			OnTurnChanged.RaiseAll();
		}
	}

	private void OnGUI() {
		if (GUI.Button(rect, "End Turn")) {
			Debug.Log("OnTurnChanged event");
			OnTurnChanged.RaiseAll();
		}

		if (GUI.Button(rect2, "Test Button")) {
			Debug.LogWarning("Unimplemented!");
		}
	}
}
