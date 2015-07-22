using UnityEngine;
using System.Collections;

public class PointToPointMovement : MonoBehaviour {

	public bool go = false;
	public GameObject unit;
	public Vector3 destination;
	public float speed = 2;

	// Update is called once per frame
	void Update () {
		if (go) {
			unit.transform.rotation = Quaternion.LookRotation (unit.transform.position - destination, Vector3.up);
			unit.transform.position = Vector3.MoveTowards (unit.transform.position, destination, speed * Time.deltaTime);
		}
	}
}
