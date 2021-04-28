using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class Dot : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    private LineRenderer lr;
    public Vector3 mouse;
    public Vector3 selfPos;
    private CanvasGroup canGroup;


    public LinePuzzle game;
    public int id;
    public bool successDrop = false;
    public Material mat;

    void Start()
    {
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
        Dot startPoint = eventData.pointerDrag.GetComponent<Dot>();
        LineRenderer curLine = startPoint.lr;

        //snaps endpoint of line to end dot 
        curLine.SetPosition(1, selfPos);
        startPoint.successDrop = true;
        game.addline(id, startPoint.id);



    }

    public void createLine()
    {
        lr = new GameObject("Line").AddComponent<LineRenderer>();
        lr.tag = "Lines";
        lr.positionCount = 2;
        lr.material = mat;
        lr.startWidth = 0.015f;
        lr.endWidth = 0.015f;
    }

    
}
