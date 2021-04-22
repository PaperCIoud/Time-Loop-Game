using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 7.5f;
    public float lookSens = 2.0f;
    public float lookLimit = 60.0f;
    public float interactDist = 10.0f;
    public Camera playerCamera;
    public Canvas playerHUD;

    private bool canMove = true;
    private CharacterController charControl;
    private Vector3 moveDir = Vector3.zero;
    private float Xrot = 0;

    void Start()
    {
        charControl = GetComponent<CharacterController>();
        this.releaseMoveLock();
    }

    void Update()
    {
        // Movement
        float Xspeed = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
        float Yspeed = canMove ? walkSpeed * Input.GetAxis("Horizontal"): 0;
        moveDir = transform.TransformDirection(Vector3.forward) * Xspeed + transform.TransformDirection(Vector3.right) * Yspeed;
        charControl.Move(moveDir * Time.deltaTime);

        // Rotation
        if (canMove) {
            Xrot += -Input.GetAxis("Mouse Y") * lookSens;
            Xrot = Mathf.Clamp(Xrot, -lookLimit, lookLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(Xrot, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSens, 0);
        }

        //Interaction ray cast
        this.checkInteraction();
        
    }

    private void checkInteraction()
    {
        // Debugging option to draw ray cast
        // Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward));
        // Debug.DrawLine(ray.origin, ray.origin + interactDist * ray.direction, Color.red);

        CrosshairController crosshair = playerHUD.GetComponent<CrosshairController>();
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, interactDist))
        {
            crosshair.setHighlighted();
            GameObject targetObj = hit.collider.gameObject;
            if (targetObj.tag == "Interactable" && Input.GetKey(KeyCode.E))
            {
                Interactable interactScript = targetObj.GetComponent<Interactable>();
                interactScript.enterInteract(this);
            }
        }
        else
        {
            crosshair.setNotHighlighted();
        }
    }

    //Change whether the player is interacting with an object
    public void getMoveLock()
    {
        canMove = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void releaseMoveLock()
    {
        canMove = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
