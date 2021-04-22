using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour, Interactable
{
    public GameObject dialog;
    private PlayerController player;

    void Start()
    {
        dialog.SetActive(false);
    }

    public void enterInteract(PlayerController playerChar)
    {
        player = playerChar;
        dialog.SetActive(true);
        player.getMoveLock();
    }

    public void exitInteract()
    {
        dialog.SetActive(false);
        player.releaseMoveLock();
    }
}
