using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireR : MonoBehaviour 
{
	public Moving mov;
	public GameObject[] rocket = new GameObject[6];
	float Timer = 1;
	int iterator = 0;
	float  rangeOfExplosion = 10;
	float fieldOfViewAngle = 160f;	
	float fieldOfCatchAngle = 60f;
	bool playerInSight = false;                     
	Transform target;
	GameObject enemy;
	// скорость ракеты
	float speed;
	// скорость поворота ракеты
	float turnSpeed = 80;

	Transform thisTransform;
	Transform Ltarget;
	public Camera cam;
	void Start () 
	{
		enemy = GameObject.FindGameObjectWithTag ("enemy");
	}

	IEnumerator TestCoroutine(int _iterator, Transform _target, Rigidbody _enemy, Transform _thisTransform,float _speed)
	{
		while(rocket[_iterator])
		{
			yield return null;
			if (rocket [_iterator] == null || _target == null) 
			{
				Destroy (rocket[_iterator],0);
				yield break;	
			}
			Vector3 movement = _thisTransform.forward * _speed * Time.deltaTime;

			Vector3 direction = _target.position - _thisTransform.position;

			float maxAngle = turnSpeed * Time.deltaTime;

			float angle = Vector3.Angle (_thisTransform.forward, direction);

			if (angle <= maxAngle)  
			{
				_thisTransform.LookAt(_target);
				
			} else 
			{
				//сферическая интерполяция направления с использованием максимального угла поворота
				Vector3 tmpDir = Vector3.Slerp(_thisTransform.position + movement * 2, _target.position, maxAngle / angle);
				_thisTransform.LookAt(tmpDir);
			}
			float distanceToTarget = direction.magnitude;

			_thisTransform.Translate (movement); 

			if (distanceToTarget < rangeOfExplosion)
			{
				Destroy (_enemy.gameObject, 0);
				Destroy (rocket[_iterator],0);
				yield break;
			}
		}
	}

	void FixedUpdate () 
	{
		speed = 100 + mov.speed;
		float fireR = Input.GetAxis ("Fire1");
		if(enemy)
		{
			Vector3 direction = enemy.transform.position - transform.position;
			float angle = Vector3.Angle (transform.forward, direction);
			if (angle < fieldOfCatchAngle * 0.5f) 
			{
				playerInSight = true;
				target = enemy.transform;
				Ltarget = target;
			}
			if (angle > fieldOfViewAngle * 0.5f)
				playerInSight = false;
			else 
			{
				RaycastHit hit;
				if (Physics.Raycast (transform.position + transform.up, 
				direction.normalized, out hit, 10000)) 
				{
					Debug.DrawRay (transform.position, direction, Color.red);
					if (hit.collider.transform.parent.gameObject.tag != "enemy") //gameObject.tag != "bot") 
					{
						playerInSight = false;
						Debug.Log (hit.collider.transform.parent.gameObject.tag);
					}
				}
			}
			if (!playerInSight && target) 
			{
				Debug.Log ("enemy is lost");
				target = null;
			}
			if (fireR != 0 && iterator < 6 && Timer >= 1 && target)
			{
				thisTransform = rocket [iterator].transform;
				Destroy (rocket [iterator], 100);
				rocket [iterator].transform.parent = null;
				GameObject fire = rocket [iterator].transform.GetChild (0).gameObject;
					//Debug.Log (fire.name);
				fire.SetActive (true);
				StartCoroutine (TestCoroutine (iterator, Ltarget, Ltarget.GetComponent<Rigidbody>(), thisTransform, speed));
				iterator++;
				Timer = 0;
			}
			Timer += Time.deltaTime;
		}
	}
}
