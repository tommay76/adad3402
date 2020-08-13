using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public GameObject hands;
    public GameObject leftHand;
    public GameObject rightHand;
    public float tilt;
    public Vector2 location;
    public float speed;
    public float direction;
    public float swivelSpeed;
    public float distBetweenWings;
    public float minDistancebetweenHands;
    public float yRotation;
    public float turningScale;
    public Camera cameraa;

    void Start()
    {
        cameraa.enabled = false;
        turningScale = 0.02f;
        hands = null;
        rightHand = null;
        leftHand = null;
        tilt = 0;
        swivelSpeed = 3;
        speed = 0.01f;
        distBetweenWings = 100;
        minDistancebetweenHands = 7;
        transform.eulerAngles = new Vector3(0,45,0);
        yRotation = 225;
        location = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (hands == null){
            tilt = rotateTowardsZero();
        }else{
            
            leftHand = hands.transform.GetChild(0).gameObject;
            rightHand = hands.transform.GetChild(1).gameObject;
            tilt = getAngleBetweenHands();
        }
        fly();
        //Debug.Log("bird refreshing!"+ location.x);

    }
    private float getAngleBetweenHands(){
        if (leftHand == null || rightHand == null){
            return 0f;
        }
        float lx = leftHand.transform.position.x;
        float ly = leftHand.transform.position.y;
        float rx = rightHand.transform.position.x;
        float ry = rightHand.transform.position.y;
        float angle = Mathf.Atan2((ry - ly),(rx - lx))*Mathf.Rad2Deg;
        distBetweenWings = Mathf.Sqrt(Mathf.Pow(rx-lx,2f)+ Mathf.Pow(ry-ly,2f));
        if (lx>rx){
            if (ly> ry) angle = -90f;
            else if (ly < ry) angle = 90f;
        }
        if (distBetweenWings< minDistancebetweenHands){
            cameraa.enabled = true;
            return rotateTowardsZero();
        }else {
            cameraa.enabled = false;
        }
        float newAngle = Mathf.Lerp(tilt,angle,Time.deltaTime*swivelSpeed);
        transform.eulerAngles = new Vector3(0,yRotation,newAngle);
        return newAngle;
        
    }
    private float rotateTowardsZero(){
        float currAngle = tilt;
        float newAngle = Mathf.Lerp(tilt,0f,Time.deltaTime*swivelSpeed);
        transform.eulerAngles = new Vector3(0,yRotation,newAngle);
        return newAngle;
    }
    private void fly(){
        direction -= ((tilt * turningScale)* Mathf.Deg2Rad);
        direction = direction %(Mathf.PI*2);
        Vector2 location2 = new Vector2(-1,1);
        float cos = Mathf.Cos(direction);
        float sin = Mathf.Sin (direction);
        float newX = location2.x *cos - location2.y * sin;
        float newY = location2.x *sin + location2.y * cos;
        location.x += newX*speed;
        location.y += newY*speed;

        
    }

}
