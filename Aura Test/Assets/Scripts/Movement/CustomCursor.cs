using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour {
	public void SetLocation(Vector3 location) {
		transform.position = location;
	}
}
