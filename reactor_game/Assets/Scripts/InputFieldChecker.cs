using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Linq;

public class InputFieldChecker : MonoBehaviour
{

    public InputField[] inputs;
    public string[] answers;
    public GameObject[] dialogs;
    public GameObject failScreen;
    public int numTries;

    private bool valid = false;
    private int failedCount = 0;

    void Start()
    {
        foreach (GameObject dialog in dialogs)
            dialog.SetActive(false);
        failScreen.SetActive(false);
    }

    private void Update()
    {
        if(failedCount == numTries)
        {
            gameObject.SetActive(false);
            failScreen.SetActive(true);
        }
    }
    public void checkAnswer()
    {
        string userInput = "";
        foreach(InputField input in inputs)
        {
            userInput = System.String.Concat(userInput, input.text);
        }

        for(int i = 0; i < answers.Length; i++)
        {
            if(answers[i].Equals(userInput, System.StringComparison.OrdinalIgnoreCase))
            {
                valid = true;
                //Debug.Log("correct");
                gameObject.SetActive(false);
                dialogs[i].SetActive(true);
            }
        }
        if(!valid)
        {
            Debug.Log("no match");
            failedCount++;
        }
        valid = false;


    }

    public void clearInput()
    {
        foreach(InputField input in inputs)
        {
            input.text = "";
        }
    }
}
