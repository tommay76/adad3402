using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(mapGenerator))]
public class mapGeneratorEditor : Editor {
    // Start is called before the first frame update
    public override void OnInspectorGUI(){

        mapGenerator mapGen = (mapGenerator)target;
        if (DrawDefaultInspector()){
            if (mapGen.autoUpdate){
                mapGen.GenerateMap();
            }
        }
        // DrawDefaultInspector();
        if (GUILayout.Button("Generate!")){
            mapGen.GenerateMap();
        }
    }
    
}
