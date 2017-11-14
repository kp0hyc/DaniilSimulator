using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTransform : MonoBehaviour 
{
	public Rigidbody rb;
	public Transform tr;
	Quaternion Qua;
	Vector3 destination;
	Vector3 Qu;
	Vector3 speed;
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		tr = GetComponent<Transform>();		
	}
	void OnTransformUpdate()
	{
		rb.isKinematic = true;
		Vector3 direction = destination - tr.position;
		float range = direction.magnitude;
		Qu = Qua.eulerAngles;
		float deltaTime = range / speed.magnitude;
		tr.forward = Vector3.Slerp (tr.forward, Qu.normalized, deltaTime);
		tr.position = Vector3.Lerp (tr.position, destination, deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
