using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Character : MonoBehaviour
{
    public GameObject Camera;

    Vector3 velocityForward;
    Vector3 velocityRight;
    Vector3 position;

    float forceWalking = 100f;

    float forceRunning = 200f;

    float forceFalling = 40f;

    float jumpForce = 7f;

    bool isFalling;

    bool jumpInVoid;

    bool isWalling;

    bool isJumping;

    bool wallJumping;

    float oldY;

    float speed;

    float lastSpeed;

    public float sensitivity;

    private float rotationX;

    float timeTouche;

    Vector3 stayWall;

    Vector3 lastDirectionForward;
    Vector3 lastDirectionRight;

    Rigidbody rb;

    GameObject fadePanel;
    Vector3 currentCheckpoint;
    // Start is called before the first frame update

    private void Awake()
    {
        Application.targetFrameRate = 120;

    }

    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        isFalling = false;
        wallJumping = false;
        rb = GetComponent<Rigidbody>();
        //rb.position = this.position;
        speed = forceWalking;
        fadePanel = GameObject.Find("Fade");
        timeTouche = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(GameManager.GameState == 1)
        {
            SetCamera();
            CheckInput();
        }*/

        SetCamera();
        CheckInput();
        transform.eulerAngles = new Vector3(0, rotationX, 0);
        float old = rb.velocity.y;
        rb.velocity = velocityForward + velocityRight;
        rb.velocity = new Vector3(rb.velocity.x, old, rb.velocity.z);

        if (jumpInVoid)
        {
            if ((Time.time - timeTouche) < 0.5)
            {
                jumpInVoid = false;
                timeTouche = Time.time;
            }
            else
            {
                timeTouche = Time.time;
            }
        }

        Debug.Log("falling : " + isFalling);
        /*Debug.Log("isWalling: " + isWalling);
        Debug.Log("jump : " + isJumping);

        Debug.Log("speed : " + speed);*/


        if (rb.position.y - 1.6f < 15f)
        {
            float fadePercent = 1 - ((rb.position.y - 1.6f) / 15f);
            fadePanel.GetComponent<Image>().color = new Vector4(255, 255, 255, fadePercent);
        }
        if (rb.position.y - 1.6f < 0f)
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

    void directionNull()
    {
        velocityForward = Vector3.zero;
        velocityRight = Vector3.zero;
        lastDirectionForwardnull();
        lastDirectionRightnull();
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
        if (collision.gameObject.tag == "Untagged")
        {
            isFalling = false;
        }
        //Debug.Log(collision.gameObject.);
        isFalling = false;
        if (collision.gameObject.tag == "Wall")
        {
            //timeToucheWall = Time.time;
            wallJumping = true;
            stayWall = rb.position;
        }

        if (collision.gameObject.tag == "Floor")
        {
            isFalling = false;
            directionNull();
            wallJumping = true;
            lastDirectionForward = Vector3.zero;
            lastDirectionRight = Vector3.zero;
            velocityForward = Vector3.zero;
            velocityRight = Vector3.zero;
        }

        if (collision.gameObject.tag == "Checkpoint")
        {
            collision.gameObject.GetComponent<CheckPoint>().isWaitingColision = false;
        }


    }


    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;

        //isFalling = false;

        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.tag == "Untagged")
        {
            isFalling = false;
        }

        if (collision.gameObject.tag == "Wall")
        {
            isWalling = true;

            if (!isFalling)
            {
                //Debug.Log("oui");
                transform.position = stayWall;
            }
            else
            {
                //Debug.Log("non");
            }

            //Debug.Log(wallJumping);
            if (Input.GetKey(KeyCode.Space))
            {

                wallJumping = false;

                lastSpeed = 100f;
                rb.AddForce((rb.transform.forward + Camera.transform.forward) * 2, ForceMode.Impulse);
                //Debug.Log(rb.transform.forward + Camera.transform.forward);
                lastDirectionForward = rb.transform.forward;

                /*Vector3 reflectDirection = Vector3.Reflect(lastDirectionForward, contactPoints[0].normal);
                rb.AddForce(reflectDirection * lastSpeed, ForceMode.VelocityChange);
                lastDirectionForward = reflectDirection;*/

                //lastDirectionRight = reflectDirection;
                //lastDirectionRight = contactPoints[0].normal;
                //Debug.Log(isFalling);
                isFalling = true;
                jumpInVoid = true;

            }
            else
            {
                lastDirectionForward = Vector3.zero;
                lastDirectionRight = Vector3.zero;
                velocityForward = Vector3.zero;
                velocityRight = Vector3.zero;
            }

        }
        else if (collision.gameObject.tag == "Floor")
        {
            if (!jumpInVoid)
            {
                isFalling = false;
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
