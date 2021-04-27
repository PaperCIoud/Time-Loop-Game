using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class DotPuzzle : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private LineRenderer lr;
    private Vector3 mouse;
    private Vector3 selfPos;
    private CanvasGroup canGroup;

    private static int num = 1;
    private static List<string> answer = new List<string> { "0-1", "1-2", "2-3" };
    private static List<string> userAnswer = new List<string> { };

    public GameObject successScreen;
    public GameObject failScreen;
    public GameObject game;

    public float id;
    public bool successDrop = false;
    public Material mat;

    void Start()
    {
        successScreen.SetActive(false);
        failScreen.SetActive(false);
        selfPos = GetComponent<Transform>().position;
        canGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        mouse = Input.mousePosition;
        mouse.z = 1;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
    }


    // Create a new line after leftclick 
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down");
        if (lr == null)
        {
            createLine();
        }
        lr.SetPosition(0, selfPos);
        lr.SetPosition(1, mouse);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        lr.SetPosition(1, mouse);
        canGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        canGroup.blocksRaycasts = true;
        if (!successDrop)
        {
            GameObject.Destroy(lr);
        }
        else
        {
            lr = null;
            num++;
        }
        //resets for next line
        successDrop = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        lr.SetPosition(1, mouse);
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Drop");
        DotPuzzle startPoint = eventData.pointerDrag.GetComponent<DotPuzzle>();
        LineRenderer curLine = startPoint.lr;

        //snaps endpoint of line to end dot 
        curLine.SetPosition(1, selfPos);
        startPoint.successDrop = true;

        userAnswer.Add(Mathf.Min(id, startPoint.id) + "-" + Mathf.Max(id, startPoint.id));


    }

    public void createLine()
    {
        lr = new GameObject("Line" + num).AddComponent<LineRenderer>();
        lr.tag = "Lines";
        lr.positionCount = 2;
        lr.material = mat;
        lr.startWidth = 0.015f;
        lr.endWidth = 0.015f;
    }

    public static void resetPuzzle()
    {
        num = 1;
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Lines");
        foreach (GameObject line in lines)
        {
            GameObject.Destroy(line);
        }
        userAnswer = new List<string> { };
    }

    public static bool checkAnswer()
    {
        userAnswer.Sort();
        answer.Sort();
        Debug.Log(Enumerable.SequenceEqual(answer, userAnswer));
        bool correct = Enumerable.SequenceEqual(answer, userAnswer);
        resetPuzzle();
        return correct; 
    }

    public void done()
    {
        game.SetActive(false);
        if(checkAnswer())
        {
            successScreen.SetActive(true);
        }
        else
        {
            failScreen.SetActive(true);
        }
    }





}
