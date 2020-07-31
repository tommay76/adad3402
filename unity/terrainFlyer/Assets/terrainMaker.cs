using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (MeshFilter))]
public class terrainMaker : MonoBehaviour {  
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    int sizeX = 20;
    int sizeZ = 20;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }

    // Update is called once per frame
    void CreateShape(){
        vertices = new Vector3[(sizeX+1)*(sizeZ+1)];
        for (int i = 0,  z = 0; z <= sizeZ; z++){
            for (int x = 0; x <= sizeX; x++){
                float y = Mathf.PerlinNoise(x*.3f,z*.3f)* 2f;
                vertices[i] = new Vector3(x,y,z);
                i++;
            }
        }
        triangles = new int[sizeX*sizeZ*6];
        int tris = 0, verts = 0;
        for (int z = 0; z < sizeZ; z++){
            for (int x = 0; x < sizeX; x++){
                triangles[tris + 0] = verts + 0;
                triangles[tris + 1] = verts + sizeX + 1;
                triangles[tris + 2] = verts + 1;
                triangles[tris + 3] = verts + 1;
                triangles[tris + 4] = verts + sizeX+ 1;
                triangles[tris + 5] = verts + sizeX + 2;
                verts++;
                tris+=6;
            }
            verts++;
        }
    }
    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos(){
        if (vertices.Length == null){
            return;
        }
        for (int i = 0; i < vertices.Length; i++) {
            Gizmos.DrawSphere(vertices[i],0.1f);
        }
    }
}
