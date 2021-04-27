using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    public string solution;

    public bool checkSolution()
    {
        string currentState = "";
        for (int i = 0; i < transform.childCount; i++)
        {
            
            if (transform.GetChild(i).name == "EnterButton" || transform.GetChild(i).name == "Dialogs")
            {
                continue;
            }
            LeverController lever = transform.GetChild(i).GetComponent<LeverController>();
            if (lever.triggered)
            {
                currentState += "1";
            } else
            {
                currentState += "0";
            }
            
        }
        
        return currentState == solution;
    }
}
