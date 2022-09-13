using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public GameObject floorObject;
    public GameObject blockObject;
    public GameObject roofObject;
    public GameObject fanObject;

    // Start is called before the first frame update
    void Start()
    {
        // Generate floor
        Instantiate(floorObject, new Vector3(0, 0, 0), Quaternion.identity);
        
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                // Generate random height building
                int rand = Random.Range(20, 35);
                GameObject batiment = Instantiate(blockObject, new Vector3((i - 5) * 15f + 7.5f, rand / 2f, (j - 5) * 15f + 7.5f), Quaternion.identity);
                batiment.transform.localScale = new Vector3(batiment.transform.localScale.x, rand, batiment.transform.localScale.z);
                Instantiate(roofObject, new Vector3((i - 5) * 15f + 7.5f, rand+0.5f, (j - 5) * 15f + 7.5f), Quaternion.identity);
                if (i == 0 && j == 0)
                {
                    // Set player height
                } else
                {
                    // Set roof obstacles
                    GameObject obstacle = Instantiate(fanObject, new Vector3((i - 5) * 15f + 7.5f, rand + 1f, (j - 5) * 15f + 7.5f), Quaternion.identity);
                    int obstSize = Random.Range(1, 3);
                    int obstPos = Random.Range(0, 3);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
