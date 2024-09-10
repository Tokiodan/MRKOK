using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    [SerializeField] private Vector2 mapSize;
    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();

        mapSize = new(terrain.terrainData.size.x, terrain.terrainData.size.z);
        float[,] noiseMap = Noise.GenerateNoiseMap(Mathf.RoundToInt(mapSize.x), Mathf.RoundToInt(mapSize.y), 0, 5.5f, 5, 3, 2, new Vector2(2, 2));

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }
}
