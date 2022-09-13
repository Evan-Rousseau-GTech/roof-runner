using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public GameObject floorObject;
    public GameObject blockObject;
    public GameObject roofObject;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(floorObject, new Vector3(0, 0, 0), Quaternion.identity);
        
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                int rand = Random.Range(10, 15);
                GameObject batiment = Instantiate(blockObject, new Vector3((i - 5) * 15f + 7.5f, rand / 2f, (j - 5) * 15f + 7.5f), Quaternion.identity);
                batiment.transform.localScale = new Vector3(batiment.transform.localScale.x, rand, batiment.transform.localScale.z);
                Instantiate(roofObject, new Vector3((i - 5) * 15f + 7.5f, rand+0.5f, (j - 5) * 15f + 7.5f), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
