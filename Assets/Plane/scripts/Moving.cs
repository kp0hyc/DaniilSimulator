using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moving : MonoBehaviour
{
	public Rigidbody rb;
	public Transform tr;
	public float thrust = 10800;
    public float speed = 0;
    public float torqueBank = 0;
    public float torquePitch = 0;
	public float torqueYaw = 0;
	public float height = 0;
	public Vector3 eulerAngles;
	float Y = 0;
	float S = 38;
	public float maxThrust = 108000;
	public float accelerate = 0;
	public float pitch = 0;
	public float bank = 0;
	public float yaw = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
		Vector3 eulerAngles = transform.rotation.eulerAngles;

		Vector3 locVel = transform.InverseTransformDirection(rb.velocity);
		speed = locVel.z * 3.6f;
		height = rb.position.y;

		if (accelerate != 0) 
			thrust += accelerate * maxThrust/20 * Time.deltaTime;
		if (thrust < 0)				thrust = 0;
		if (thrust > maxThrust)		thrust = maxThrust;

		//Debug.Log (eulerAngles.z);

		rb.AddRelativeTorque(Vector3.forward * torqueBank * bank);
		rb.AddRelativeTorque(Vector3.right * torquePitch * pitch); 
		rb.AddRelativeTorque(Vector3.up * torqueYaw * yaw);

		if(eulerAngles.z < 180)
			Y = (1.162f - 0.00008f * height) * speed * speed * S/30 * (1 - pitch/2) * (180 - eulerAngles.z)/180; //
		if(eulerAngles.z > 180)
			Y = (1.162f - 0.00008f * height) * speed * speed * S/30 * (1 - pitch/2) * (eulerAngles.z - 180)/180; //
		if(Y > 0)	
			rb.AddRelativeForce(Vector3.up * Y);
        if((1.162f - 0.00006f * height) > 0)	
			rb.AddRelativeForce(Vector3.forward * thrust * (1.162f - 0.00006f * height));
   }


}
