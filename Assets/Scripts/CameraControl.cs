using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		Vector3 translation = Vector3.zero;

		translation += new Vector3 (-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

		camera.transform.position += 100 * translation * Time.deltaTime;
	}
}
