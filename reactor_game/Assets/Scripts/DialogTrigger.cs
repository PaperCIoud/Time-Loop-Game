using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public GameObject dialog;
    public GameObject character;
    PlayerController player;
    //public bool enterInteract = false;

    void Start()
    {
        player = character.GetComponent<PlayerController>();
        dialog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.checkInteractable())
        {
            if (Input.GetKey(KeyCode.E))
            {
                enterInteract();
                Debug.Log("worked");
            }
        }
    }


    public void enterInteract()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        dialog.SetActive(true);
        player.setInteract(true);
    }

    public void exitInteract()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        dialog.SetActive(false);
        player.setInteract(false);
    }
}
