using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Character : MonoBehaviour
{
    Vector3 velocityForward;
    Vector3 velocityRight;
    Vector3 position;

    float forceWalking = 35f;

    float forceRunning = 60f;

    float forceFalling = 20f;

    float jumpForce = 5f;

    bool isFalling;

    bool isWalling;

    bool isJumping;

    bool wallJumping;

    float oldY;

    float speed;

    float lastSpeed;

    public float sensitivity;

    private float rotationX;

    float timeToucheWall;

    Vector3 lastDirectionForward;
    Vector3 lastDirectionRight;

    Rigidbody rb;

    GameObject fadePanel;
    Vector3 currentCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        isFalling = false;
        wallJumping = false;
        rb = GetComponent<Rigidbody>();
        //rb.position = this.position;
        speed = forceWalking;
        fadePanel = GameObject.Find("Fade");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GameState == 1)
        {
            CheckInput();
        }
        SetCamera();
        transform.eulerAngles = new Vector3(0, rotationX, 0);
        float old = rb.velocity.y;
        rb.velocity = velocityForward + velocityRight;
        rb.velocity = new Vector3(rb.velocity.x, old, rb.velocity.z);

        /*Debug.Log("falling : " + isFalling);
        Debug.Log("isWalling: " + isWalling);
        Debug.Log("jump : " + isJumping);*/
        

        if (rb.position.y - 2.8f < 15f)
        {
            float fadePercent = 1 - ((rb.position.y - 2.8f) / 15f);
            fadePanel.GetComponent<Image>().color = new Vector4(255, 255, 255, fadePercent);
        }
        if (rb.position.y - 2.8f < 0f)
        {
            ResetPosition();
        }
    }

    //Vérifie les entrées du clavier puis fait avancer le personnage en fonction.
    public void CheckInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = forceRunning;
        }
        else
        {
            speed = forceWalking;
        }

        if (isFalling == true)
        {
            //Debug.Log(lastDirectionRight);
            rb.AddForce(lastDirectionForward * lastSpeed);
            rb.AddForce(lastDirectionRight * lastSpeed);
            speed = forceFalling;
        }
        else
        {
            CheckKeyUp();
            if (!isWalling)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //Permet le saut
                    isFalling = true;
                    lastSpeed = speed;
                }
            }
        }

        if (Input.GetKey(KeyCode.Z))
        {
            rb.AddForce(rb.transform.forward * speed);
            if (!isFalling)
            {
                lastDirectionForward = rb.transform.forward;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.forward * -speed);
            if (!isFalling)
            {
                lastDirectionForward = -rb.transform.forward;
            }
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddForce(transform.right * -speed);
            if (!isFalling)
            {
                lastDirectionRight = -rb.transform.right;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * speed);
            if (!isFalling)
            {
                lastDirectionRight = rb.transform.forward;
            }
        }
        
    }

    //Vérifie si une des entrées est relaché puis réinitialise la direction actuelle
    void CheckKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            velocityForward = Vector3.zero;
            lastDirectionForwardnull();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            velocityForward = Vector3.zero;
            lastDirectionForwardnull();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            velocityRight = Vector3.zero;
            lastDirectionRightnull();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            velocityRight = Vector3.zero;
            lastDirectionRightnull();
        }
    }

    void lastDirectionRightnull()
    {
        lastDirectionRight = Vector3.zero; 
    }

    void lastDirectionForwardnull()
    {
        lastDirectionForward = Vector3.zero;
    }

    private void OnCollisionExit(Collision collision)
    {
        isFalling = true;
        isWalling = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        isFalling = false;

        if (collision.gameObject.tag == "Floor")
        {
            isFalling = false;
            wallJumping = true;
            lastDirectionForward = Vector3.zero;
            lastDirectionRight = Vector3.zero;
            velocityForward = Vector3.zero;
            velocityRight = Vector3.zero;
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Wall");
            timeToucheWall = Time.time;
            wallJumping = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        
        isFalling = false;

        if (collision.gameObject.tag == "Wall")
        {
            isWalling = true;
            //Debug.Log(wallJumping);
            if (Input.GetKey(KeyCode.Space) && wallJumping)
            {

                wallJumping = false;

                lastSpeed = 100f;

                Vector3 reflectDirection = Vector3.Reflect(lastDirectionForward, contactPoints[0].normal);
                rb.AddForce(reflectDirection * lastSpeed, ForceMode.VelocityChange);
                lastDirectionForward = reflectDirection;

                //lastDirectionRight = reflectDirection;
                //lastDirectionRight = contactPoints[0].normal;

                isFalling = true;

            }
            else
            {
                lastDirectionForward = Vector3.zero;
                lastDirectionRight = Vector3.zero;
                velocityForward = Vector3.zero;
                velocityRight = Vector3.zero;
            }

        }
    }
    public void OnTriggerEnter(Collider other)
    {
       // Debug.Log("trigger !");

        if (other.gameObject.tag == "Checkpoint")
        {
            other.gameObject.GetComponent<CheckPoint>().isWaitingColision = false;
            SetCheckpoint(other.transform.position);
        }
    }
    //Set la rotation X en fonction de la souris
    public void SetCamera()
    {
        float wantedVelocity = GetMouseInput() * sensitivity;
        rotationX += wantedVelocity * Time.deltaTime;
    }

    //Retourne l'entrée X de la souris
    private float GetMouseInput()
    {
        float input = Input.GetAxis("Mouse X");

        return input;
    }

    public void SetPosition(Vector3 position)
    {
        rb.position = position;
    }

    public void SetCheckpoint(Vector3 position)
    {
        currentCheckpoint = position;
    }

    public void ResetPosition()
    {
        SetPosition(currentCheckpoint);
        fadePanel.GetComponent<Image>().color = new Vector4(255, 255, 255, 0);
    }

    
}
