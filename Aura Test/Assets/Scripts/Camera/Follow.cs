using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	public GameObject cameraHandler;
	public float aswdSpeed;
	public float scrollAmount;
	public float cameraDepth;
	public float smoothTime = 0.3f;
	public Vector3 offset;

	private Vector3 velocity = Vector3.zero;
	Vector3 pos;
	Vector3 targetPos;

	bool isCentering;
	bool isKeyPressed;

	// Use this for initialization
	void Start() {
		//if (target) {
		//	pos = target.transform.position;
		//	pos.y = cameraDepth;
		//	targetPos = new Vector3(pos.x, cameraDepth, pos.z);
		//	pos = Vector3.SmoothDamp(transform.position, targetPos + offset, ref velocity, smoothTime);

		//	transform.position = new Vector3(pos.x, pos.y, pos.z);
		//}
		isKeyPressed = false;
		isCentering = false;
		Camera.main.transform.position = new Vector3(offset.x, cameraDepth + offset.y, offset.z);
	}

	private void LateUpdate() {
		if (isCentering) {
			isKeyPressed = Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f;
		}

		float mouseScroll = -Input.GetAxis("Mouse ScrollWheel");
		//Debug.DrawRay(transform.position, transform.forward * 20f, Color.green);
		Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		move = move.normalized * Time.deltaTime * aswdSpeed * Camera.main.transform.position.y;

		move += new Vector3(0f, mouseScroll * scrollAmount * Camera.main.transform.position.y, 0f);

		cameraHandler.transform.Translate(move);
	}

	public void CenterCameraOn(GameObject target) {
		isKeyPressed = false;
		StartCoroutine(Move(target.transform.position));
	}

	public void CenterCameraOn(Vector3 target) {
		isKeyPressed = false;
		StartCoroutine(Move(target));
	}

	IEnumerator Move(Vector3 target) {
		while (isKeyPressed != true && Vector3.Distance(cameraHandler.transform.position, target) > 0.3f) {
			Debug.Log("Camera Follow -> Moving...");
			isCentering = true;
			cameraHandler.transform.position = Vector3.SmoothDamp(cameraHandler.transform.position, target, ref velocity, smoothTime);
			yield return new WaitForEndOfFrame();
		}

		Debug.Log("Camera Follow -> Exiting Move...");
		isCentering = false;
	}

	public void OnTurnMasterChanged() {
		CenterCameraOn(GameManager.Instance.TurnMaster.transform.position);
	}
}
