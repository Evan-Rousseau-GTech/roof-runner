using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector3 position;

    float speedWalking = 7.5f;

    float speedRunning = 15f;

    float jumpSpeed = 5f;

    bool isJumping;

    public static Vector2 sensitivity = new Vector2(2000f, 2000f);

    private float rotationX;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        isJumping = false;
        rb = GetComponent<Rigidbody>();
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        SetCamera();
        CheckInput();
        position.y = rb.position.y;
        transform.SetPositionAndRotation(position, Quaternion.Euler(0, rotationX, 0));
    }

    public void CheckInput()
    {
        float speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
             speed = speedRunning;
        }
        else
        {
            speed = speedWalking;
        }

        if (!isJumping)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                position.x += transform.forward.x * speed * Time.deltaTime;
                position.z += transform.forward.z * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                position.x -= transform.forward.x * speed * Time.deltaTime;
                position.z -= transform.forward.z * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                position.x -= transform.right.x * speed * Time.deltaTime;
                position.z -= transform.right.z * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                position.x += transform.right.x * speed * Time.deltaTime;
                position.z += transform.right.z * speed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector3.up * jumpSpeed;
                isJumping = true;
            }
        }
        else
        {
            position.z += 
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Floor")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            isJumping = false;
        }
    }

    /*private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            isJumping = true;
        }
    }*/



    public void SetCamera()
    {
        float wantedVelocity = GetInput() * sensitivity.x;
        rotationX += wantedVelocity * Time.deltaTime;
        //rotation.y = Mathf.Clamp(rotation.y, -80, 80);
    }

    private float GetInput()
    {
        float input = Input.GetAxis("Mouse X");

        return input;
    }
}
