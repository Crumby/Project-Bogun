using UnityEngine;
using System.Collections;


public class WoodcuttersHutBehaviour : MonoBehaviour {

    public GameObject prefab;
	
	void Start () {
         GameObject woodcutter = (GameObject)Instantiate(prefab, transform.position + (transform.forward * (-17) ), transform.rotation);
	}

	void Update () {
	
	}
}
