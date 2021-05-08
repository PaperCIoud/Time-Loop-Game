using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float timeBeforeMeltdown = 180;
    public GameObject gameStartDialog;
    public GameObject gameRestartDialog;
    public GameObject gamePauseDialog;
    public GameObject restartCounterPrefab;
    public GameObject player;
    public Image fadeMask;
    public GameObject reactor;

    public GameObject wireCuttingPuzzle;
    public GameObject dotPuzzle;
    public GameObject leverPuzzle;

    public Slider volSlider;
    public Slider SensSlider;

    public GameObject wireCuttingRewardDoor;
    public GameObject timeIndicatorLight;

    private PlayerController playerCont;
    private GameObject restartCounterObject;
    private RestartCounter restartCounter;

    private LinePuzzle dotPuzzleCont;
    private WireCuttingGame wireCuttingCont;
    private LeverPuzzle leverPuzzleCont;

    private DoorController doorCont;
    private bool paused = false;


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

        playerCont.updateVol(restartCounter.getVolumeSetting());
        playerCont.updateSens(restartCounter.getSensitivitySetting());
        volSlider.value = restartCounter.getVolumeSetting();
        SensSlider.value = restartCounter.getSensitivitySetting();

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
        gamePauseDialog.SetActive(false);

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
        if (Input.GetKey(KeyCode.P) && playerCont.canMove)
        {
            pause();
        }
    }

    public void meltdown()
    {
        restartCounter.increment();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Interactable script = obj.GetComponent<Interactable>();
            if (script != null)
            {
                script.exitInteract();
            }
        }

        explode();
    }

    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void pause()
    {
        paused = true;
        playerCont.getMoveLock();
        gamePauseDialog.SetActive(true);
    }

    public void unpause()
    {
        paused = false;
        playerCont.releaseMoveLock();
        gamePauseDialog.SetActive(false);
    }

    public void setSensSetting(System.Single sens)
    {
        restartCounter.setSensitivitySetting(sens);
        playerCont.updateSens(sens);
    }

    public void setVolSetting(System.Single vol)
    {
        restartCounter.setVolumeSetting(vol);
        playerCont.updateVol(vol);
    }

    IEnumerator runCountdown()
    {
        while(timeBeforeMeltdown > 0)
        {
            if (!paused)
            {
                timeBeforeMeltdown -= Time.deltaTime;
            }
            yield return null;
        }
        meltdown();
    }

    IEnumerator blinkLight()
    {
        Light blinkingLight = timeIndicatorLight.GetComponent<Light>();
        while(timeBeforeMeltdown > 0.5)
        {
            if (Mathf.Sin(8*Mathf.PI*(180-timeBeforeMeltdown)/(timeBeforeMeltdown+10f)) > 0.7)
            {
                blinkingLight.color = Color.red;
            }
            else
            {
                blinkingLight.color = Color.yellow;
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

    public void explode()
    {
        AudioSource reactorSource = reactor.GetComponent<AudioSource>();
        reactorSource.Play();
        AudioSource music = player.GetComponent<AudioSource>();
        music.volume = 0.05f;
        StartCoroutine(waitToTimeTravel());
    }

    public IEnumerator waitToTimeTravel()
    {
        float elapsed = 0;
        while (elapsed < 2f)
        {
            float fadeMaskAlpha = Mathf.Lerp(0, 1, elapsed/2f);
            fadeMask.color = new Color(1, 1, 1, fadeMaskAlpha);

            elapsed += Time.deltaTime;
            yield return null;
        }
        playerCont.timeTravel();
    }

    public void win()
    {
        SceneManager.LoadScene("BootlegWinScreen");
    }
}
