using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour {

	public float movementRange;

	public Vector3 target;
	public LayerMask mask;
	public GameObject customCursor;

	private Vector3[] corners;
	private int posCount;

	float dist;
	Vector3 circlePosition;
	GameObject turnMaster;
	NavMeshAgent nav;

	// Update is called once per frame
	public void Update() {
		// Tallennetaan Navmeshin distance
		dist = nav.remainingDistance;

		// Jos ollaan määränpäässä niin laitetaan liikkuminen falseksi
		if (dist != Mathf.Infinity && nav.pathStatus == NavMeshPathStatus.PathComplete && nav.remainingDistance == 0) {
			GameManager.Instance.Moving = false;
		}


		//if (caster.CheckHit(mask) && !EventSystem.current.IsPointerOverGameObject()) {
		//	EnablePointer();
		//	if (!GameManager.Instance.Moving && !GameManager.Instance.Moving) {
		//		CalculatePath(caster.GetHit());
		//	}
		//} else {
		//	DisablePointer();
		//}

		//if (!moving) {
		//Päivitetään navmeshin pathi joka # välein
		//elapsed += Time.deltaTime;
		//if (elapsed > 0.02f) {
		//    elapsed -= 0.02f;
		//    NavMesh.CalculatePath(raypoint.position, target, NavMesh.AllAreas, path);
		//}
		//}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask) && !GameManager.Instance.Moving && !GameManager.Instance.Moving && !EventSystem.current.IsPointerOverGameObject()) {
			CalculatePath(hit);
		}
	}

	public void CalculatePath(RaycastHit hit) {

		// Hiiren worldspace positioni
		Vector3 mousePosition = hit.point;

		// Hiiren etäisyys ympyrän keskipisteestä
		float mouseDistanceFromCircle = Vector3.Distance(circlePosition, mousePosition);

		// Clämpätään se pelaajan liikkumis etäisyydellä
		mouseDistanceFromCircle = Mathf.Clamp(mouseDistanceFromCircle, 0f, movementRange);

		// Luodaan vektori ympyrän keskipisteestä hiireen
		Vector3 dir = mousePosition - circlePosition;

		dir = Vector3.ClampMagnitude(dir, movementRange);

		// Lasketaan 3D hiiren sijainti
		Vector3 pointerPosition = circlePosition + (dir.normalized * mouseDistanceFromCircle);

		// Laitetaan 3D hiiren laskettuun sijaintiin
		customCursor.transform.position = pointerPosition;

		// Piiretään ray debuggausta varten
		Debug.DrawRay(circlePosition, dir, Color.green);

		// Lasketaan 3D hiiren suunta pelaajan hahmosta
		//Vector3 dirFromPlayer = pointerPosition - transform.position;

		// Tarkistetaan onko seinä 3D hiiren ja pelaajan välissä.
		//RaycastHit hitinfo;
		//if (Physics.Raycast(new Ray(transform.position, dirFromPlayer), out hitinfo, Mathf.Infinity)) {
		//	Debug.DrawRay(transform.position, dirFromPlayer, Color.yellow);
		//	NavMeshHit hitp;

		//// Jos löytyy seinä, niin laitetaan 3D hiiren lähimpään navmesh areaan
		//if (hitinfo.collider.CompareTag("Wall")) {
		//	if (NavMesh.SamplePosition(hitinfo.point, out hitp, 5f, NavMesh.AllAreas)) {
		//		customCursor.transform.position = hitp.position;
		//	}
		//}

		// Ei löytynyt seinää, joten liikutaan normaalisti 3D hiiren sijaintiin
		if (hit.transform.CompareTag("Ground")) {
			if (Input.GetMouseButtonDown(0)) {
				Move(pointerPosition);
			}
		}

		//}
	}

	public void Move(Vector3 destination) {
		nav.SetDestination(destination);
		GameManager.Instance.Moving = true;
	}

	//public int GetPathLength(Vector3[] corners) {
	//	float lng = 0.0f;

	//	if ((path.status != NavMeshPathStatus.PathInvalid)) {
	//		for (int i = 1; i < corners.Length; ++i) {
	//			lng += Vector3.Distance(corners[i - 1], corners[i]);
	//		}
	//	}

	//	return (Mathf.RoundToInt(lng) / 2) + 1;
	//}

	public void OnTurnMasterChanged() {
		turnMaster = GameManager.Instance.TurnMaster;
		nav = turnMaster.GetComponent<NavMeshAgent>();
		movementRange = turnMaster.GetComponent<Stats>().GetMovementRange();
		circlePosition = turnMaster.transform.position;
	}
}
