using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzle : MonoBehaviour
{
    public int[] solution;

    public WireHook getSelected()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Dialogs" || transform.GetChild(i).name == "PowerButton")
            {
                continue;
            }
            WireHook childController = transform.GetChild(i).GetComponent<WireHook>();
            if (childController.selected)
            {
                return childController;
            }
        }
        return null;
    }

    public bool checkSolution()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Dialogs" || transform.GetChild(i).name == "PowerButton")
            {
                continue;
            }
            WireHook child = transform.GetChild(i).GetComponent<WireHook>();
            if (child.pair == null)
            {
                return false;
            }
            if (solution[child.ID] != child.pair.ID)
            {
                return false;
            }
        }
        return true;
    }
}
