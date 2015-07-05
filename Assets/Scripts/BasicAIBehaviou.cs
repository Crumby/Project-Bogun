using UnityEngine;
using System.Collections;

public class BasicAIBehaviou : MonoBehaviour {
	public float GroundDistance = 0.1f;

	private float firstRayDistance;
	private Ray rayRangeFromScreen;
	private RaycastHit hit;

	void Start () {
		firstRayDistance = -1;
	}
	// Update is called once per frame
	void Update () {
		// to keep unit above ground
		rayRangeFromScreen = new Ray(this.gameObject.transform.position+new Vector3(0,0,20),-Vector3.up);
				
		//if ray hits terrain
		if (Physics.Raycast(rayRangeFromScreen, out hit, 1000))
		{
				// tak pokud se nejak zmeni delka paprsku od prvniho vystreleneho paprsku, pak se kamera oddali, ci priblizi
				if (firstRayDistance < 0)
				{
					firstRayDistance = hit.distance;
				}
			else if (firstRayDistance > hit.distance + GroundDistance)
				{
					Vector3 ff = transform.position;
					ff.y += 50F*Time.deltaTime;
					transform.position = ff;
				}
			else if (firstRayDistance < hit.distance - GroundDistance)
				{
					Vector3 ff = transform.position;
					ff.y -= 50F*Time.deltaTime;
					transform.position = ff;
				}
	}
	}
}

