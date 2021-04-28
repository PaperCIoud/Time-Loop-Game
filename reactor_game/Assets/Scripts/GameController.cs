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

    public GameObject wireCuttingPuzzle;
    public GameObject dotPuzzle;
    public GameObject leverPuzzle;

    public GameObject wireCuttingRewardDoor;
    public GameObject timeIndicatorLight;

    private PlayerController playerCont;
    private GameObject restartCounterObject;
    private RestartCounter restartCounter;

    private LinePuzzle dotPuzzleCont;
    private WireCuttingGame wireCuttingCont;
    private LeverPuzzle leverPuzzleCont;

    private DoorController doorCont;


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

        dotPuzzleCont = dotPuzzle.GetComponent<LinePuzzle>();
        wireCuttingCont = wireCuttingPuzzle.GetComponent<WireCuttingGame>();
        leverPuzzleCont = leverPuzzle.GetComponent<LeverPuzzle>();

        doorCont = wireCuttingRewardDoor.GetComponent<DoorController>();

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
        StartCoroutine(blinkLight());
        StartCoroutine(dotPuzzleListener());
        StartCoroutine(wireCuttingListener());
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

    IEnumerator blinkLight()
    {
        Light blinkingLight = timeIndicatorLight.GetComponent<Light>();
        while(timeBeforeMeltdown > 0.5)
        {
            if (Mathf.Sin(4*Mathf.PI*(180-timeBeforeMeltdown)/timeBeforeMeltdown) > 0.7)
            {
                blinkingLight.color = Color.red;
            }
            else
            {
                blinkingLight.color = Color.white;
            }
            yield return null;
        }
    }

    IEnumerator dotPuzzleListener()
    {
        while (!dotPuzzleCont.isSolved())
        {
            yield return null;
        }
        leverPuzzleCont.dotPuzzleSolved = true;
    }

    IEnumerator wireCuttingListener()
    {
        while (!wireCuttingCont.isSolved())
        {
            yield return null;
        }
        doorCont.open();
    }
}
