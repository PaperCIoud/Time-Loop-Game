using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class LinePuzzle : MonoBehaviour
{

    private List<string> answer = new List<string> { "0-2","0-6","1-6","1-3","2-6","3-6","4-5","4-6","5-6" };
    private List<string> userAnswer = new List<string> { };
    private bool completed = false;

    public GameObject successScreen;
    public GameObject failScreen;
    


    void Start()
    {
        successScreen.SetActive(false);
        failScreen.SetActive(false);
    }


    public void resetPuzzle()
    {
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Lines");
        foreach (GameObject line in lines)
        {
            GameObject.Destroy(line);
        }
        userAnswer = new List<string> { };
    }

    public bool checkAnswer()
    {
        userAnswer.Sort();
        answer.Sort();
        Debug.Log(Enumerable.SequenceEqual(answer, userAnswer));
        completed = Enumerable.SequenceEqual(answer, userAnswer);

        resetPuzzle();
        return completed;
    }

    public void done()
    {
        gameObject.SetActive(false);
        if (checkAnswer())
        {
            successScreen.SetActive(true);
        }
        else
        {
            failScreen.SetActive(true);
        }
    }

    public void addline(int id1, int id2)
    {
        userAnswer.Add(Mathf.Min(id1, id2) + "-" + Mathf.Max(id1, id2));
    }

    public bool isSolved()
    {
        return completed;
    }


}
