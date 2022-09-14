using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    float x;
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //x += 1 * Time.deltaTime;
       
        //transform.position = new Vector3(x, 2, 9.3f);
    }
}
