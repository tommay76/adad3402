using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// these define the flock's behavior
/// </summary>
public class BoidController : MonoBehaviour
{
	public float minVelocity = 5;
	public float maxVelocity = 20;
	public float randomness = 1;
	public int flockSize = 20;
	public BoidFlocking prefab;
	public Transform target;
	public BirdController bird;
	public float shrinkRay;
	public float height;
	public float borderLength;
	public float visibleDistance;
	public Vector3 flockCenter;
	internal Vector3 flockVelocity = new Vector3(1,1,1);
	public AudioClip[] audios; 
	List<BoidFlocking> boids = new List<BoidFlocking>();

	void Start()
	{

		for (int i = 0; i < flockSize; i++)
		{
			Vector3 pos = new Vector3();
			int randomBorderChooser = Random.Range(0,4);
			switch (randomBorderChooser){
				case 0:
					pos = new Vector3(0,height + ( i*45),Random.Range(0,2*borderLength));
					break;
				case 1:
					pos = new Vector3(2*borderLength,height + ( i*46),Random.Range(0,2*borderLength));
					break;
				case 2:
					pos = new Vector3(Random.Range(0,2*borderLength),height + ( i*46),0);
					break;
				case 3:
					pos = new Vector3(Random.Range(0,borderLength),height + ( i*46),2*borderLength);
					break;
			}
			BoidFlocking boid = Instantiate(prefab, pos, transform.rotation) as BoidFlocking;

			boid.music.clip = audios[i];
			float ting = Mathf.Atan2(pos.x-(target.transform.position.x+borderLength),pos.z- (target.transform.position.z+borderLength))*180/Mathf.PI;
			boid.transform.eulerAngles = new Vector3(0,ting-180 + Random.Range(-45,45),0);
			boid.controller = this;
			if (bird != null){
				boid.bird = bird;
			}
			boids.Add(boid);
			Debug.Log("New boid added");
		}
		foreach (BoidFlocking boid in boids)
		{
			boid.music.loop = true;
			boid.music.Play();
		}
	}

	void Update()
	{
		Vector3 center = Vector3.zero;
		Vector3 velocity = Vector3.zero;
		foreach (BoidFlocking boid in boids)
		{
			constrainBoid(boid);
			center += boid.transform.position;

		}
		center += bird.transform.position;
		if (flockSize != 0){
			flockCenter = center / (flockSize+1);
		}
	}
	void constrainBoid(BoidFlocking boid){
		if (boid.transform.position.x > visibleDistance || boid.transform.position.x < -visibleDistance
		|| boid.transform.position.z > visibleDistance || boid.transform.position.z < -visibleDistance){
			Vector3 scale = boid.transform.localScale;
			scale.x =  Mathf.Lerp(scale.x,0,Time.deltaTime*shrinkRay);
			scale.y =  Mathf.Lerp(scale.y,0,Time.deltaTime*shrinkRay);
			scale.z =  Mathf.Lerp(scale.z,0,Time.deltaTime*shrinkRay);
			boid.transform.localScale =  scale;
			if (boid.transform.position.x > borderLength){ 
				boid.transform.position = new Vector3(-borderLength +20,boid.transform.position.y,boid.transform.position.z);
				boid.position = boid.transform.position;
			}
			else if (boid.transform.position.x < -borderLength){
				boid.transform.position = new Vector3(borderLength-20,boid.transform.position.y,boid.transform.position.z);
				boid.position = boid.transform.position;
			}
			if (boid.transform.position.z > borderLength){ 
				boid.transform.position = new Vector3(boid.transform.position.x,boid.transform.position.y,-borderLength+20);
				boid.position = boid.transform.position;

			}
			else if (boid.transform.position.z < -borderLength){
				boid.transform.position = new Vector3(boid.transform.position.x,boid.transform.position.y,borderLength-20);
				boid.position = boid.transform.position;
			}
		}else {
			Vector3 scale = boid.transform.localScale;
			scale.x =  Mathf.Lerp(scale.x,126.6816f,Time.deltaTime*shrinkRay);
			scale.y =  Mathf.Lerp(scale.y,10,Time.deltaTime*shrinkRay);
			scale.z =  Mathf.Lerp(scale.z,30,Time.deltaTime*shrinkRay);
			boid.transform.localScale =  scale;
		}
	}
}