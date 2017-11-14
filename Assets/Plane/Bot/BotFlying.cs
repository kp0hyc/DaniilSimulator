using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotFlying : MonoBehaviour
{
	public Moving mov;
	GameObject player;   
	Vector3 direction;
	float angle;
	bool takeoff = false;
	float fieldOfViewAngle = 160f;	
	GameObject[] enemyarray = new GameObject[10];  
	int iterator = 0;
	//private NavMeshAgent nav;

	bool playerInSight;                     
                     
	Vector3 personalLastSighting;            
	float distance;                            
	float storeddistance;

	int numberofEnemiesSeen;
	int maxenemypossible;

	void Start()
	{
		//Debug.Log (gameObject.name);

		//nav = GetComponent<NavMeshAgent>();
	}
	void FixedUpdate()
	{
		player = GameObject.FindGameObjectWithTag ("Player"); 
		if (player) 
		{			
			Debug.Log (player.name);
			direction = player.transform.position - transform.position;
			angle = Vector3.Angle (direction, player.transform.forward);
			mov.pitch = 0;
			if (direction.magnitude < 100 && !takeoff) 
			{
				gameObject.GetComponent<takeoff> ().enabled = true;
				takeoff = true;
				gameObject.GetComponent<BotFlying> ().enabled = false;
			}
			if (angle < 360 && takeoff) 
			{
				while (mov.eulerAngles.z < 220)
					mov.bank = 1;
				mov.pitch = -1;
				
			}

			/*if (angle < 30f) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position + transform.up, 
					     direction.normalized, out hit, col.radius)) {
					Debug.DrawRay (transform.position, direction, Color.red);
					Debug.Log (hit.collider.gameObject.name);
					if (hit.collider.gameObject.tag == "player") {
						playerInSight = true;
						Debug.Log ("player is seen");
						numberofEnemiesSeen++;
					}
				}
			}	*/
		}
	}
}



	
			
	/*float CalculatePathLength (Vector3 targetPosition)
	{
		NavMeshPath path = new NavMeshPath();
		if(nav.enabled)
			nav.CalculatePath(targetPosition, path);
		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
		allWayPoints[0] = transform.position;
		allWayPoints[allWayPoints.Length - 1] = targetPosition;
		for(int i = 0; i < path.corners.Length; i++)
		{
			allWayPoints[i + 1] = path.corners[i];
		}
		float pathLength = 0;
		for(int i = 0; i < allWayPoints.Length - 1; i++)
		{
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		}
		return pathLength;
	}*/

