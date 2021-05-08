using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public int framesPerSecond = 10;

    void Update()
    {
        int index = ((int) (Time.time * framesPerSecond)) % frames.Length;
        GetComponent<Image>().sprite = frames[index];
    }
}
