using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image crosshair;
    public Image crosshairHighlighted;

    private bool isHighlighted = false;

    private void Start()
    {
        crosshairHighlighted.enabled = false;
    }

    public void setHighlighted()
    {
        isHighlighted = true;
        crosshair.enabled = false;
        crosshairHighlighted.enabled = true;
    }

    public void setNotHighlighted()
    {
        isHighlighted = false;
        crosshair.enabled = true;
        crosshairHighlighted.enabled = false;
    }
}
