using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public GameObject floorObject;
    public GameObject blockObject;
    public GameObject roofObject;
    public GameObject fanObject;
    public GameObject map;
    public Character character;
    // Start is called before the first frame update
    void Start()
    {
        map = new GameObject("Map");
        // Generate floor
        GameObject floor = Instantiate(floorObject, new Vector3(0, 0, 0), Quaternion.identity);
        floor.transform.parent = map.transform;
        
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                // Generate random height building
                GameObject building = new GameObject("Building");
                int rand = Random.Range(20, 35);
                GameObject batiment = Instantiate(blockObject, new Vector3((i - 5) * 15f + 7.5f, rand / 2f, (j - 5) * 15f + 7.5f), Quaternion.identity);
                batiment.transform.localScale = new Vector3(batiment.transform.localScale.x, rand, batiment.transform.localScale.z);
                batiment.transform.parent = building.transform;
                GameObject roof = Instantiate(roofObject, new Vector3((i - 5) * 15f + 7.5f, rand+0.5f, (j - 5) * 15f + 7.5f), Quaternion.identity);
                roof.transform.parent = building.transform;
                if (i == 0 && j == 0)
                {
                    //� mettre dans le game manager
                    character.SetCheckpoint(new Vector3(batiment.transform.position.x, rand + 2, batiment.transform.position.z));
                    character.ResetPosition();
                } else
                {
                    // Set roof obstacles
                    GameObject obstacle = Instantiate(fanObject, new Vector3((i - 5) * 15f + 7.5f, rand + 1f, (j - 5) * 15f + 7.5f), Quaternion.identity);
                    float[] obstacleSizes = new float[3] { 0.8f, 1f, 1.2f };
                    float obstSize = obstacleSizes[Random.Range(0, 2)];
                    obstacle.transform.localScale = new Vector3(obstSize, obstSize, obstSize);
                    Vector3[] obstaclePositions = new Vector3[4] 
                    { 
                        new Vector3(obstacle.transform.position.x - 1.5f, rand + 0.5f + obstSize, obstacle.transform.position.z - 1.5f),
                        new Vector3(obstacle.transform.position.x - 1.5f, rand + 0.5f + obstSize, obstacle.transform.position.z + 1.5f),
                        new Vector3(obstacle.transform.position.x + 1.5f, rand + 0.5f + obstSize, obstacle.transform.position.z + 1.5f),
                        new Vector3(obstacle.transform.position.x + 1.5f, rand + 0.5f + obstSize, obstacle.transform.position.z - 1.5f)
                    };
                    Vector3 obstPos = obstaclePositions[Random.Range(0, 3)];
                    int obstRot = 90*Random.Range(0, 3);
                    obstacle.transform.SetPositionAndRotation(obstPos, Quaternion.Euler(new Vector3(0,obstRot,0)));
                    obstacle.transform.parent = building.transform;
                }
                building.transform.parent = map.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}