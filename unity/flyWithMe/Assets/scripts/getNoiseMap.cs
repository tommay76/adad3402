using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class getNoiseMap {
    // Start is called before the first frame update
    public static float[,] generateNoiseMap(int width, int height, int seed, float scale, float rotation, int octaves, float persistance, float lacurnarity, Vector2 offset){
        if (scale <= 0){
            scale = 0.0001f;
        }
        System.Random prng = new System.Random(seed);
        Vector2[] octavesOffsets = new Vector2[octaves];
        for (int i =0; i < octaves; i ++){
            float offsetX = prng.Next(-100000,100000) + offset.x;
            float offsetY = prng.Next(-100000,100000) + offset.y;
            octavesOffsets[i] = new Vector2 (offsetX,offsetY);
        }
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;
        float halfWidth =  width / 2f;
        float halfHeight = height / 2f;
        float[,] noiseMap = new float[width,height];
        for (int y = 0; y < height; y++){
            for (int x = 0; x < width; x++){
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i ++){
                    float sampleX = (x-halfWidth)/scale * frequency ;
                    float sampleY = (y-halfHeight)/scale * frequency ;
                    float endX = sampleX*Mathf.Cos(rotation) - sampleY*Mathf.Sin(rotation)+ octavesOffsets[i].x;
                    float endY = sampleX*Mathf.Sin(rotation) + sampleY*Mathf.Cos(rotation)+ octavesOffsets[i].y;
                    float perlinValue = Mathf.PerlinNoise(endX,endY)* 2 - 1;
                    noiseMap[x,y] = perlinValue;
                    noiseHeight += perlinValue * amplitude;
                    amplitude *= persistance;
                    frequency *= lacurnarity;
                }
                if (noiseHeight > maxNoiseHeight){
                    maxNoiseHeight = noiseHeight;
                }else if (noiseHeight < minNoiseHeight){
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x,y] = noiseHeight;
            }
            
        }
        for (int y = 0; y < height; y++){
            for (int x = 0; x < width; x++){
                noiseMap[x,y] = Mathf.InverseLerp(minNoiseHeight,maxNoiseHeight,noiseMap[x,y]);
            }
        }
        return noiseMap;
    }
}
