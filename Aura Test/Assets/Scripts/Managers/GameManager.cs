using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager> {

	public GameEvent OnTurnMasterChanged;
	public GameObject TurnMaster { get; set; }

	public bool Moving { get; set; }
	public bool Casting { get; set; }

	public void SetTurnMaster(GameObject character) {
		Log("TurnMaster Changed!");
		TurnMaster = character;
		OnTurnMasterChanged.RaiseAll();
	}

	public void Log(string str) {
		Debug.Log("GameManager-> " + str);
	}
}
