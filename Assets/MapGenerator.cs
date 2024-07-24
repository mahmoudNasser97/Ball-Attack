using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 20;
    public int height = 20;
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public GameObject powerUpPrefab;
    public GameObject smallBalls;
    public GameObject enemyBalls;
    public GameObject enemyAIPrefab;
    public GameObject player;
    public float obstacleDensity = 0.2f;
    public int powerUpCount = 5;
    public int trapCount = 5;

    private void Start()
    {
        {
            GenerateMap();
            Instantiate(player);
        }

        void GenerateMap()
        {
            Vector3 position = new Vector3(0, 0, 0);
            Instantiate(floorPrefab, position, Quaternion.identity);
            int obstacleCount = Mathf.FloorToInt(width * height * obstacleDensity);
            PlaceRandomObjects(smallBalls, trapCount);
            PlaceRandomObjects(enemyBalls, trapCount);
            PlaceRandomObjects(enemyAIPrefab, trapCount);
        }

        void PlaceRandomObjects(GameObject prefab, int count)
        {
            HashSet<Vector2> occupiedPositions = new HashSet<Vector2>();

            while (count > 0)
            {
                int x = Random.Range(0, width);
                int z = Random.Range(0, height);
                Vector2 position = new Vector2(x, z);

                if (!occupiedPositions.Contains(position))
                {
                    Instantiate(prefab, new Vector3(x, 15, z), Quaternion.identity);
                    occupiedPositions.Add(position);
                    count--;
                }
            }
        }
    }
}
