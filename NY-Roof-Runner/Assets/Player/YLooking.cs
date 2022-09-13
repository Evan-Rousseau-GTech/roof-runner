using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YLooking : MonoBehaviour
{
    Vector2 sensitivity;
    private float rotationY;
    // Start is called before the first frame update
    void Start()
    {
        sensitivity = Character.sensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        SetCamera();
        transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    }

    //Set la rotation Y en fonction de la souris
    public void SetCamera()
    {
        float wantedVelocity = GetMouseInput() * sensitivity.y;
        rotationY += wantedVelocity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -80, 80);
    }

    //Retourne l'entrée Y de la souris
    private float GetMouseInput()
    {
        float input = -Input.GetAxis("Mouse Y");
        return input;
    }
}
