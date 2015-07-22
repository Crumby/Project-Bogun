using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour
	
{
	public GameObject objectHide;
	static private Transform trSelect = null; 
	private bool selected = false;
	private Collision collision = null;

	void Start()
	{
		objectHide.renderer.enabled = false;
	}
	
	void Update () 
	{
		transform.Rotate(Vector3.up * Time.deltaTime * 10, Space.World);
		if (selected && transform != trSelect) 
		{
			selected = false;
			objectHide.renderer.enabled = false;
		}
		if (Input.GetKeyDown ("escape")) 
		{
			selected = false;
			objectHide.renderer.enabled = false;
		}
	}




	void OnMouseDown()
	{
		if (Input.GetMouseButtonDown (0))
		{	objectHide.renderer.enabled = true;
			selected = true;
			trSelect = transform;
		}

		/*if (selected == true && collision.gameObject.tag == "Terrain") 
		{
			objectHide.renderer.enabled = false;
			selected = false;
			Debug.Log ("Hit !!");

		}*/
	}

	void OnMouseEnter() 
	{
		objectHide.renderer.enabled = true;
	}

	void OnMouseOver() 
	{
		objectHide.renderer.enabled = true;
	}
	
	void OnMouseExit() 
	{
		if (selected == true) 
				objectHide.renderer.enabled = true;
		else 
				objectHide.renderer.enabled = false;
	}

	/*void OnCollisionEnter(Collision collision) 
	{
		if (selected == true && Input.GetMouseButtonDown (0)) 
		{
			if (collision.gameObject.tag == "Terrain") 
			{
				objectHide.renderer.enabled = false;
				selected = false;
				Debug.Log ("Hit !!");
			}
		}
	}*/
	/*
	void OnControllerColliderHit(hit:ControllerColliderHit)
	{
		if(hit.gameObject.tag == "terrain")
		{
			objectHide.renderer.enabled = false;
			selected = false;
		}
	}*/


}


