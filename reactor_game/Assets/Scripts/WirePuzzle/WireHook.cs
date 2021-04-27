using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireHook : MonoBehaviour, Interactable
{
    public float wireDrawDist = 2.0f;
    public bool selected = false;
    public GameObject parent;
    public int ID;

    public WireHook pair;
    private PlayerController player;
    private WirePuzzle parentController;
    private float timeSinceInteract = 0;
    private LineRenderer wireRenderer;

    // Start is called before the first frame update
    void Start()
    {
        parentController = parent.GetComponent<WirePuzzle>();
        wireRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceInteract += Time.deltaTime;

        wireRenderer.enabled = false;
        if (selected)
        {
            wireRenderer.enabled = true;
            Ray toPlayerFocus = player.getViewRay();
            Debug.DrawLine(transform.position, toPlayerFocus.origin + wireDrawDist * toPlayerFocus.direction, Color.gray);
            wireRenderer.SetPositions(new Vector3[] { transform.position, toPlayerFocus.origin + wireDrawDist * toPlayerFocus.direction });
        }
        else if (pair != null)
        {
            Debug.DrawLine(transform.position, pair.transform.position, Color.blue);
            wireRenderer.enabled = true;
            wireRenderer.SetPositions(new Vector3[] { transform.position, pair.transform.position });
        }
    }

    public void enterInteract(PlayerController playerChar)
    {
        if (timeSinceInteract < 2)
        {
            return;
        }
        timeSinceInteract = 0;

        player = playerChar;
        WireHook selectedHook = parentController.getSelected();
        if (selectedHook == this) { }
        else if (selectedHook == null)
        {
            selected = true;
        } else
        {
            setPair(selectedHook);
            selectedHook.exitInteract();
        }
    }

    public void exitInteract()
    {
        selected = false;
    }

    public void clearPair()
    {
        if (pair != null)
        {
            pair.pair = null;
        }
        pair = null;
    }

    public void setPair(WireHook toPair)
    {
        clearPair();
        toPair.clearPair();
        toPair.pair = this;
        pair = toPair;
    }
}
