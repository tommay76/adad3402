using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidFlocking : MonoBehaviour
{
	internal BoidController controller;
	public float drag = 5;
	public float velocity = 5;
	public Vector3 position;
	public Vector3 position2;
	public Vector3 offset;
	public Vector3 rotation;
	public Vector3 forward;
	public float steerMultiple;
	public BirdController bird;
	public MeshRenderer mesh;
	public AudioSource music;
	IEnumerator Start()
	{
		position = transform.position;
		position2 = transform.position;
		rotation = transform.eulerAngles;
		forward = transform.TransformDirection(Vector3.forward);

		while (true)
		{
			if (controller)
			{
				
				// position = steer();
				
				// transform.position = position;
				offset = addDrag();

				position=offset*drag;
				steer();
				
				
				position+=position2;
				
				position.x = (position.x  % (2*controller.borderLength))-controller.borderLength;
				position.z = (position.z  % (2*controller.borderLength))-controller.borderLength;
				transform.position = convertToRealDirection(position);
				
				
				// position = convertToRealDirection(transform.position);
				// transform.eulerAngles = new Vector3(0,rotation.y ,0);

				transform.eulerAngles = new Vector3(0,rotation.y - (bird.direction*Mathf.Rad2Deg),0);

				// enforce minimum and maximum speeds for the boids

			}
			float waitTime = 0.001f;
			yield return new WaitForSeconds(waitTime);
		}
	}
	Vector3 steer()
	{

		if (bird != null){
			
			float newAngle = Mathf.Atan2((position2.z - controller.flockCenter.z),(position2.x  -  controller.flockCenter.z))*Mathf.Rad2Deg;
			newAngle*=steerMultiple;
			rotation.y += newAngle;
			transform.eulerAngles = rotation;
			forward = transform.TransformDirection(Vector3.forward);
			rotation.y -= newAngle;
			transform.eulerAngles = rotation;
			position2+=forward*velocity;
			rotation.y += newAngle;

			return position2;

		}
		return position2;
	}
	Vector3 convertToRealDirection(Vector3 position){
		Vector3 realPos = new Vector3();
		realPos.x = position.x * Mathf.Cos(bird.direction) - position.z * Mathf.Sin(bird.direction);
		realPos.y = transform.position.y;
		realPos.z = position.x * Mathf.Sin(bird.direction) + position.z * Mathf.Cos(bird.direction);
		return realPos;
	}
	Vector3 convertToFakeDirection(Vector3 position){
		Vector3 realPos = new Vector3();
		realPos.x = position.x * Mathf.Cos(-bird.direction) - position.z * Mathf.Sin(-bird.direction);
		realPos.y = transform.position.y;
		realPos.z = position.x * Mathf.Sin(-bird.direction) + position.z * Mathf.Cos(-bird.direction);
		return realPos;
	}
	Vector3 addDrag(){
		Vector3 draggi = new Vector3(-bird.location.x,0, bird.location.y);

		return draggi;
	}
}