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
    public GameObject incorrectDialog;
    public GameObject failScreen;
    public int numTries;

    private bool valid = false;
    public int failedCount = 0;

    void Start()
    {
        foreach (GameObject dialog in dialogs)
            dialog.SetActive(false);
        failScreen.SetActive(false);
        incorrectDialog.SetActive(false);
    }

    private void Update()
    {
        if(failedCount == numTries)
        {
            Debug.Log("failed");
            gameObject.SetActive(false);
            incorrectDialog.SetActive(false);
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
                gameObject.SetActive(false);
                incorrectDialog.SetActive(false);
                dialogs[i].SetActive(true);
            }
        }
        if(!valid)
        {
            Debug.Log("no match");
            incorrectDialog.SetActive(true);
            failedCount++;
        }
        valid = false;
    }

    public void checkAnwserLogin()
    {
        checkAnswer();

        GameObject userCheck = dialogs[2];
        GameObject petCheck = dialogs[3];
        GameObject colorCheck = dialogs[4];
        GameObject invalid = dialogs[5];

        userCheck.SetActive(false);
        petCheck.SetActive(false);
        colorCheck.SetActive(false);
        invalid.SetActive(false);

        string pet = inputs[0].text;
        string color = inputs[1].text;
        string user = inputs[2].text;


        if (user.Equals("scout", System.StringComparison.OrdinalIgnoreCase))
        {
            userCheck.SetActive(true);
            if(pet.Equals("kevin", System.StringComparison.OrdinalIgnoreCase))
            {
                petCheck.SetActive(true);
            }
            if (color.Equals("blue", System.StringComparison.OrdinalIgnoreCase))
            {
                colorCheck.SetActive(true);
            }
        } 
        else if (user.Equals("lisa", System.StringComparison.OrdinalIgnoreCase))
        {
            userCheck.SetActive(true);
            if (pet.Equals("whiskers", System.StringComparison.OrdinalIgnoreCase))
            {
                petCheck.SetActive(true);
            }
            if (color.Equals("purple", System.StringComparison.OrdinalIgnoreCase))
            {
                colorCheck.SetActive(true);
            }
        }
        else
        {
            incorrectDialog.SetActive(false);
            invalid.SetActive(true);
        }

    }

    public void clearInput()
    {
        foreach(InputField input in inputs)
        {
            input.text = "";
        }
    }


}
