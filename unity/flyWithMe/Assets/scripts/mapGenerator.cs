using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour
{
    public enum DrawMode {NoiseMap,ColourMap,Mesh};
    public DrawMode drawMode;
    public BirdController bird;
    public int mapWidth;
    public int mapHeight;
    public float meshHeightMultiplyer;
    public AnimationCurve meshHeightCurve;
    public float noiseScale;
    public bool autoUpdate;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public float rotation;
    public Vector2 offset;
    public float newOffsetX;
    public float newOffsetY;
    public TerrainType[] regions;
    public void GenerateMap(){
        Vector2 newOffset = new Vector2(offset.x,offset.y);
        newOffset.x = bird.location.x;
        newOffset.y = bird.location.y;
        rotation = bird.direction;
        // newOffset = new Vector2(bird.location.x,bird.location.y);
        float[,] noiseMap = getNoiseMap.generateNoiseMap(mapWidth,mapHeight,seed,noiseScale,rotation,octaves,persistance,lacunarity,newOffset);
        Color[] colourMap = new Color[mapWidth*mapHeight];
        for (int y = 0; y < mapHeight; y++){
            for (int x = 0; x < mapWidth; x++){
                float currentHeight = noiseMap[x,y];
                for (int i = 0; i < regions.Length;i++){
                    if (currentHeight <= regions[i].height){
                        colourMap[y*mapWidth+x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        mapDisplay display = FindObjectOfType<mapDisplay>();
        if (drawMode == DrawMode.NoiseMap){
            display.drawTexture(textureGenerator.TextureFromHeightMap(noiseMap));
        }else if ( drawMode == DrawMode.ColourMap){
            display.drawTexture(textureGenerator.TextureFromColourMap(colourMap,mapWidth,mapHeight));
        }else if (drawMode == DrawMode.Mesh){
            display.drawMesh (meshGenerator.GenerateTerrainMesh(noiseMap,meshHeightMultiplyer, meshHeightCurve), textureGenerator.TextureFromColourMap(colourMap,mapWidth,mapHeight));
        }
        
    }
    void OnValidate() {
        if (mapWidth < 1){
            mapWidth = 1;
        }
        if (mapHeight < 1){
            mapHeight = 1;
        }
        if (lacunarity < 1){
            lacunarity = 1;
        }
        if ( octaves < 0){
            octaves = 0;
        }
    }
    void Start()
    {
        GenerateMap();
    }
    void Update()
    {
        GenerateMap();
        //Debug.Log("Map refreshing!"+ bird.location.x);
    }
}
[System.Serializable]
public struct TerrainType {
    public string name;
    public float height;
    public Color colour;
}