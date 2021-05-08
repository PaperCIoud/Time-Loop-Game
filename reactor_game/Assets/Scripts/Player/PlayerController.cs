using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 7.5f;
    public float lookSens = 1.0f;
    public float lookLimit = 90.0f;
    public float interactDist = 10.0f;
    public Canvas playerHUD;
    public GameObject TimeTravelTube;
    public GameObject TimeTravelCam;
    public GameObject reactor;

    public LayerMask rayMask;

    public Camera playerCamera;
    public Camera puzzleCam;

    public bool canMove = true;
    private CharacterController charControl;
    private Vector3 moveDir = Vector3.zero;
    private float Xrot = 0;

    void Start()
    {
        charControl = GetComponent<CharacterController>();
        this.releaseMoveLock();
        TimeTravelTube.SetActive(false);
        toMainCam();
    }

    void Update()
    {
        // Movement
        float Xspeed = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
        float Yspeed = canMove ? walkSpeed * Input.GetAxis("Horizontal"): 0;
        moveDir = transform.TransformDirection(Vector3.forward) * Xspeed + transform.TransformDirection(Vector3.right) * Yspeed;
        charControl.Move(moveDir * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);

        // Rotation
        if (canMove) {
            Xrot += -Input.GetAxis("Mouse Y") * lookSens;
            Xrot = Mathf.Clamp(Xrot, -lookLimit, lookLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(Xrot, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSens, 0);
        }

        //Interaction ray cast
        this.checkInteraction();

        //Adjust look sensitivity
        if (Input.GetKey(KeyCode.Period))
        {
            lookSens *= 1.02f;
        }
        if (Input.GetKey(KeyCode.Comma))
        {
            lookSens *= 0.98f;
        }
        
    }

    private void checkInteraction()
    {
        // Debugging option to draw ray cast
        // Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward));
        // Debug.DrawLine(ray.origin, ray.origin + interactDist * ray.direction, Color.red);

        CrosshairController crosshair = playerHUD.GetComponent<CrosshairController>();
        crosshair.setNotHighlighted();
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, interactDist, ~(1 << LayerMask.NameToLayer("Player"))))
        {
            GameObject targetObj = hit.collider.gameObject;
            Debug.Log(targetObj.name);
            if (targetObj.tag == "Interactable" && canMove)
            {
                crosshair.setHighlighted();
                if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Mouse0))
                {
                    Interactable interactScript = targetObj.GetComponent<Interactable>();
                    interactScript.enterInteract(this);
                }
            }
        }
    }

    public Ray getViewRay()
    {
        return new Ray(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward));
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

    public void toMainCam()
    {
        playerCamera.enabled = true;
        puzzleCam.enabled = false;
        playerHUD.enabled = true;
    }

    public void toPuzzleCam()
    {
        playerCamera.enabled = false;
        puzzleCam.enabled = true;
        playerHUD.enabled = false;
    }

    public void updateSens(float sens)
    {
        lookSens = sens;
    }

    public void updateVol(float vol)
    {
        AudioListener.volume = vol;
    }

    public void timeTravel()
    {
        playerCamera.enabled = false;
        playerHUD.enabled = false;

        TimeTravelTube.SetActive(true);
        //TimeTravelTube.transform.position = playerCamera.transform.position;
        //TimeTravelTube.transform.localRotation = playerCamera.transform.rotation * Quaternion.Euler(0, 180, 0);
        
        DollyController animCam = TimeTravelCam.GetComponent<DollyController>();
        animCam.animate();
    }
}
