using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour {

	public GameEvent listenedEvent;

	public UnityEvent OnEvent;

	public void OnEventRaised() {
		OnEvent.Invoke();
	}

	private void OnEnable() {
		listenedEvent.AddListener(this);
	}

	private void OnDisable() {
		listenedEvent.RemoveListener(this);
	}
}
