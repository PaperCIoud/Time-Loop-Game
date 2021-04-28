using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour, Interactable
{
    public GameObject parent;
    public GameObject winningDialog;
    public GameObject losingDialog;
    public GameObject breakingDialog;

    private PlayerController player;
    private WirePuzzle parentController;

    public void Start()
    {
        parentController = parent.GetComponent<WirePuzzle>();
        winningDialog.SetActive(false);
        losingDialog.SetActive(false);
        breakingDialog.SetActive(false);
    }

    public void enterInteract(PlayerController playerChar)
    {
        player = playerChar;
        player.getMoveLock();
        int correct = parentController.checkSolution();
        if (correct == 2)
        {
            winningDialog.SetActive(true);
        }
        else if (correct == 0)
        {
            losingDialog.SetActive(true);
        }
        else
        {
            breakingDialog.SetActive(true);
        }
    }
    public void exitInteract()
    {
        winningDialog.SetActive(false);
        losingDialog.SetActive(false);
        breakingDialog.SetActive(false);
        player.releaseMoveLock();
    }
}
