using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] public bool isWaitingColision = true;
    [SerializeField] public int idCheckpoint = 0;
    [SerializeField] GameObject PrefabCheckpoint;
    [SerializeField] Material newMatrial;
    [SerializeField] MeshRenderer ThisMEsh;
    // Start is called before the first frame update
    void Start()
    {

    }
    public CheckPoint()//Vector3 Pos
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        if(isWaitingColision == false)
        {
            ThisMEsh.material = newMatrial;
        }

    }
    void OnTriggerEnter(Collider other)
    {

    }

    void OnCollisionEnter(Collision collision)
    {

    }
}
