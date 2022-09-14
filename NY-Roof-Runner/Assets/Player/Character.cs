using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Character : MonoBehaviour
{
    public Vector3 position;

    float speedWalking = 7.5f;

    float speedRunning = 15f;

    float jumpSpeed = 5f;

    bool isJumping; 

    float speed;

    public float sensitivity;

    private float rotationX;

    Vector3 lastDirectionForward;
    Vector3 lastDirectionRight;

    Rigidbody rb;

    GameObject fadePanel;
    Vector3 currentCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        isJumping = false;
        rb = GetComponent<Rigidbody>();
        speed = speedWalking;
        fadePanel = GameObject.Find("Fade");
    }

    // Update is called once per frame
    void Update()
    {
        SetCamera();
        CheckInput();
        
        position.y = rb.position.y;
        transform.SetPositionAndRotation(position, Quaternion.Euler(0, rotationX, 0));
        if(position.y - 1.6f < 15f)
        { 
            float fadePercent = 1 - ((position.y - 1.6f) / 15f);
            fadePanel.GetComponent<Image>().color = new Vector4(255,255,255,fadePercent);
        }
        if (position.y - 1.6f < 0f)
        {
            ResetPosition();
        }
    }


    //Vérifie les entrées du clavier puis fait avancer le personnage en fonction.
    public void CheckInput()
    {

        if (!isJumping) 
        {
            //Verifie si le personnage court
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
                position += transform.forward * speed * Time.deltaTime;
                lastDirectionForward = transform.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                position -= transform.forward * speed * Time.deltaTime;
                lastDirectionForward = -transform.forward;
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
                rb.velocity = Vector3.up * jumpSpeed; //Permet le saut
                isJumping = true;
            }
            CheckKeyUp();
        }
        else
        {
            //Inertie du joueur
            position += lastDirectionForward * speed * Time.deltaTime;
            position += lastDirectionRight * speed * Time.deltaTime;
        }

    }

    //Vérifie si une des entrées est relaché puis réinitialise la direction actuelle
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
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = false;
            lastDirectionForward = Vector3.zero;
            lastDirectionRight = Vector3.zero;
        }

        if (collision.gameObject.tag == "Wall")
        {

        }

        if (collision.gameObject.tag == "Checkpoint")
        {
            collision.gameObject.GetComponent<CheckPoint>().isWaitingColision = false;
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
        this.position = position;
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
