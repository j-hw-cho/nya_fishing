using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour {
	private Vector3 touchPosition;

	// Use this for initialization
	void Start () {
		touchPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {	// Mouse Button Clicked
			touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D[] hits = Physics2D.RaycastAll (touchPosition, Vector2.zero);
			bool isHole = false;
			GameObject touchedHole = null;
			if (hits.Length > 0) {
				foreach (RaycastHit2D hit in hits) {
					if (hit != null && hit.collider != null) {
						if (hit.collider.gameObject.tag == "hole_active") {
							touchedHole = hit.collider.gameObject.transform.parent.gameObject;
							isHole = true;
							break;
						}
					}
				}

				if (isHole && touchedHole != null) {
					touchedHole.GetComponent<HoleController> ().Touched ();
				}
			}
		}
	}
}
