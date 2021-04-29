using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireCuttingGame : MonoBehaviour
{
    public GameObject successDialog;
    public GameObject failDialog;
    public Wire[] wires;


    private string[] order = new string[] { "black", "red", "blue", "white", "yellow" };
    private int numCut = 0;
    private enum State { Valid, Failed, Success };
    private State state;

    // Start is called before the first frame update
    void Start()
    {
        failDialog.SetActive(false);
        successDialog.SetActive(false);
        state = State.Valid;
    }


    public void cutWire(GameObject input)
    {

        var wireToCut = input.GetComponent<Wire>();
        if (state == State.Valid)
        {
            if (wireToCut.color != order[numCut])
            {
                state = State.Failed;
                failDialog.SetActive(true);
                Debug.Log("Wrong Order");
            }
            Destroy(input);
            numCut++;

            if (numCut == order.Length)
            {
                state = State.Success;
                successDialog.SetActive(true);
            }
        }


    }

    public bool isSolved()
    {
        return (state == State.Success);
    }
}
