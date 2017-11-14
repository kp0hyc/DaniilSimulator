using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeoff : MonoBehaviour {
	public Moving mov;

	void Start () 
	{
	}
	
	void Update () 
	{
		/*RaycastHit hit;
		if (Physics.Raycast (transform.position + transform.up, transform.forward, out hit, 2500)) 
		{
			Debug.DrawRay (transform.position, transform.forward, Color.green);
			while(hit.collider != null) 
			{
				Debug.Log (hit.transform.gameObject.name);
				rb.AddRelativeTorque(Vector3.up * torqueYaw);

			}
		}*/
		mov.maxThrust = 80000;
			mov.accelerate = 1;
		mov.pitch = -1;
		/*if (mov.height >= 200) 
		{
			gameObject.GetComponent<BotFlying> ().enabled = true;
			mov.accelerate = 0;
			gameObject.GetComponent<takeoff> ().enabled = false;
		}*/
	}
}
