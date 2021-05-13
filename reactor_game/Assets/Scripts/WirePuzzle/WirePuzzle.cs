using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzle : MonoBehaviour
{
    public int[] solution;
    public bool broken = false;

    public WireHook getSelected()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).name.Contains("Hook"))
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

    public int checkSolution()
    {
        // Return 0 if puzzle wrong but GMA pin right -> does nothing but doesnt break
        // Return 1 if gma pin wrong -> puzzle breaks
        // Return 2 if correct
        if (broken)
        {
            return 1;
        }
        int correct = 2;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).name.Contains("Hook"))
            {
                continue;
            }
            WireHook child = transform.GetChild(i).GetComponent<WireHook>();
            if (child.pair == null)
            {
                correct = 0;
                continue;
            }
            if (solution[child.ID] != child.pair.ID)
            {
                correct = 0;
            }
            if (child.ID == 2 && solution[child.ID] != child.pair.ID)
            {
                broken = true;
                return 1;
            }
        }
        return correct;
    }
}
