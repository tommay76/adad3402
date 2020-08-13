using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planesController : MonoBehaviour
{
    public BirdController[] planes;
    public Vector2 offset;
    public float planeHeight;
    public float boundLength;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void makePlane(){
        // GameObject plane = Instantiate(planePrefab);
        // planes.Append(plane);
        // int sideSelection = Random.Range(0,4);
        // switch (sideSelection){
        //     case 0:
        //         plane.transform.position = new Vector3(0,planeHeight,Random.Range(0,boundLength));
        //         break;
        //     case 1:
        //         plane.transform.position = new Vector3(boundLength,planeHeight,Random.Range(0,boundLength));
        //         break;
        //     case 2:
        //         plane.transform.position = new Vector3(Random.Range(0,boundLength),planeHeight,0);
        //         break;
        //     case 3:
        //         plane.transform.position = new Vector3(Random.Range(0,boundLength),planeHeight,boundLength);
        //         break;

        // }
        // plane.transform.position = new Vector3()
    }
}
