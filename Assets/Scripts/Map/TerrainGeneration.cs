using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TerrainGeneration : MonoBehaviour
{
    [SerializeField] private Vector2 mapSize;
    public GameObject rockPrefab;
    public GameObject GrassPrefab;
    private float floor = 0.8f;
    private float ceiling = 1f;

    // private List<GameObject> positions;


    void Start()
    {
        // steps to getting perlin noise rock generation:
        //0. get the terrain size
        Terrain terrain = GetComponent<Terrain>();

        //1. create a perlin noise map
        float[,] noiseMap = Noise.GenerateNoiseMap(Mathf.CeilToInt(terrain.terrainData.size.x / 10), Mathf.CeilToInt(terrain.terrainData.size.z / 10), 0, 2.5f, 3, 3f, 1f, new Vector2(1, 1));

        //2. iterate over the noisemap
        for (int i = 0; i < noiseMap.GetLength(0); i++)
        {
            for (int j = 0; j < noiseMap.GetLength(1); j++)
            {
                if (noiseMap[i, j] > floor && noiseMap[i, j] < ceiling)
                {
                    //4. instantiate a rock on every rock spot
                    int posX = i * 10;
                    int posY = j * 10;
                    //5. make them spawn ontop of the terrain
                    Instantiate(rockPrefab, new Vector3(posX, terrain.SampleHeight(new Vector3(posX, 0, posY)), posY), Quaternion.identity);
                    //4. put positions into a new array
                    // positions.Add(rock);
                    // GameObject rock =
                }
                else if (noiseMap[i, j] > 0.1f && noiseMap[i, j] < 0.3f)
                {
                    int posX = i * 10;
                    int posY = j * 10;
                    Instantiate(GrassPrefab, new Vector3(posX, terrain.SampleHeight(new Vector3(posX, 0, posY)), posY), Quaternion.identity);
                }
            }
        }
    }

    // public IEnumerator RockTimer(GameObject rock)
    // {
    //     //5. deactivate rock when broken.
    //     rock.SetActive(false);
    //     //6. if broken, replace the rock after timer

    // }




}
