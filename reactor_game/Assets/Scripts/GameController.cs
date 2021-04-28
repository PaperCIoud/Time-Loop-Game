using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float timeBeforeMeltdown = 180;
    public GameObject gameStartDialog;
    public GameObject gameRestartDialog;
    public GameObject restartCounterPrefab;
    public GameObject player;

    private PlayerController playerCont;
    private GameObject restartCounterObject;
    private RestartCounter restartCounter;
    private bool completedStartDialog = false;

    void Start()
    {
        restartCounterObject = GameObject.FindGameObjectWithTag("RestartCounter");
        if (restartCounterObject == null)
        {
            restartCounterObject = Instantiate(restartCounterPrefab);
        }
        restartCounter = restartCounterObject.GetComponent<RestartCounter>();
        playerCont = player.GetComponent<PlayerController>();
        playerCont.getMoveLock();
        if (restartCounter.count > 0)
        {
            gameRestartDialog.SetActive(true);
            gameStartDialog.SetActive(false);
        }
        else
        {
            gameStartDialog.SetActive(true);
            gameRestartDialog.SetActive(false);
        }
        StartCoroutine(runCountdown());
    }

    public void exitStartInteraction()
    {
        playerCont.releaseMoveLock();
        gameRestartDialog.SetActive(false);
        gameStartDialog.SetActive(false);
        completedStartDialog = true;
    }

    void Update()
    {
        if (!completedStartDialog)
        {
            playerCont.getMoveLock();
        }
    }

    public void meltdown()
    {
        restartCounter.increment();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator runCountdown()
    {
        while(timeBeforeMeltdown > 0)
        {
            timeBeforeMeltdown -= Time.deltaTime;
            yield return null;
        }
        meltdown();
    }
}
