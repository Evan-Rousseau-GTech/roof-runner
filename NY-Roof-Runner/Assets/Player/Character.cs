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

    float speed;

    public static Vector2 sensitivity = new Vector2(2000f, 2000f);

    private float rotationX;

    Vector3 lastDirectionForward;
    Vector3 lastDirectionRight;

    float lastAngle;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        isJumping = false;
        rb = GetComponent<Rigidbody>();
        position = transform.position;
        speed = speedWalking;
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
        

        if (!isJumping)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = speedRunning;
            }
            else
            {
                speed = speedWalking;
            }

            if (Input.GetKey(KeyCode.Z))
            {
                /*position.x += transform.forward.x * speed * Time.deltaTime;
                position.z += transform.forward.z * speed * Time.deltaTime;*/
                position += transform.forward * speed * Time.deltaTime;
                lastDirectionForward = transform.forward;
                lastAngle = transform.rotation.y;
            }
            if (Input.GetKey(KeyCode.S))
            {
                position -= transform.forward * speed * Time.deltaTime;
                lastDirectionForward = -transform.forward;
                lastAngle = transform.rotation.y;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                position -= transform.right * speed * Time.deltaTime;
                lastDirectionRight = -transform.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                position += transform.right * speed * Time.deltaTime;
                lastDirectionRight = transform.right;

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector3.up * jumpSpeed;
                isJumping = true;
            }
            CheckKeyUp();
        }
        else
        {
            position += lastDirectionForward * speed * Time.deltaTime;
            position += lastDirectionRight * speed * Time.deltaTime;
        }

    }


    void CheckKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            lastDirectionForwardnull();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            lastDirectionForwardnull();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            lastDirectionRightnull();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            lastDirectionRightnull();
        }
    }


    Vector3 lastDirectionRightnull()
    {
        return lastDirectionRight = Vector3.zero;
    }

    Vector3 lastDirectionForwardnull()
    {
        return lastDirectionRight = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("col");
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Floor")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            isJumping = false;
            lastDirectionForward = Vector3.zero;
            lastDirectionRight = Vector3.zero;
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
