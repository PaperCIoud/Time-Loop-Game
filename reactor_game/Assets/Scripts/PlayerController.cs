using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 7.5f;
    public float lookSens = 2.0f;
    public float lookLimit = 45.0f;
    public float interactDist = 10.0f;
    public Camera playerCamera;
    public Canvas playerHUD;
    public bool inInteract = false;

    CharacterController charControl;
    Vector3 moveDir = Vector3.zero;
    float Xrot = 0;

    void Start()
    {
        charControl = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //only allow movement when not in interact mode
        if (!inInteract)
        {
            // Movement
            float Xspeed = walkSpeed * Input.GetAxis("Vertical");
            float Yspeed = walkSpeed * Input.GetAxis("Horizontal");
            moveDir = transform.TransformDirection(Vector3.forward) * Xspeed + transform.TransformDirection(Vector3.right) * Yspeed;
            charControl.Move(moveDir * Time.deltaTime);

            // Rotation
            Xrot += -Input.GetAxis("Mouse Y") * lookSens;
            Xrot = Mathf.Clamp(Xrot, -lookLimit, lookLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(Xrot, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSens, 0);
        }


    }

    public bool checkInteractable()
    {
        // Ray casting for interactables

        // Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward));
        // Debug.DrawLine(ray.origin, ray.origin + interactDist * ray.direction, Color.red);
        CrosshairController crosshair = playerHUD.GetComponent<CrosshairController>();
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, interactDist))
        {
            crosshair.setHighlighted();
            //Debug.Log("Pointing at object: " + hit.collider.gameObject.name); // Insert interaction logic
            return true;
        }
        else
        {
            crosshair.setNotHighlighted();
            //Debug.Log("Pointing at nothing.");
            return false;
        }
    }

    //Change whether the player is interacting with an object
    public void setInteract(bool input)
    {
        inInteract = input;
    }
}
