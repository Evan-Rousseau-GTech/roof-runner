using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigi : MonoBehaviour
{
    [SerializeField] public Character character;
    // Start is called before the first frame update
    void Start()
    {
        character.SetPosition(new Vector3(100, 3, 121));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
