using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour, Interactable
{
    public GameObject parent;
    public GameObject winningDialog;
    public GameObject losingDialog;

    private PlayerController player;
    private WirePuzzle parentController;

    public void Start()
    {
        parentController = parent.GetComponent<WirePuzzle>();
        winningDialog.SetActive(false);
        losingDialog.SetActive(false);
    }

    public void enterInteract(PlayerController playerChar)
    {
        player = playerChar;
        player.getMoveLock();
        bool correct = parentController.checkSolution();
        if (correct)
        {
            winningDialog.SetActive(true);
        }
        else
        {
            losingDialog.SetActive(true);
        }
    }
    public void exitInteract()
    {
        winningDialog.SetActive(false);
        losingDialog.SetActive(false);
        player.releaseMoveLock();
    }
}