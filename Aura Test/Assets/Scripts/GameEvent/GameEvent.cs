using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Game Event")]

public class GameEvent : ScriptableObject {

	private List<GameEventListener> activeListeners = new List<GameEventListener>();

	public void AddListener(GameEventListener listener) {
		activeListeners.Add(listener);
	}

	public void RemoveListener(GameEventListener listener) {
		activeListeners.Remove(listener);
	}

	public void RaiseAll() {
		for (int i = activeListeners.Count - 1; i >= 0; i--) {
			activeListeners[i].OnEventRaised();
		}
	}
}
