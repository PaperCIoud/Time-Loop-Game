using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wire : MonoBehaviour, IPointerClickHandler
{
    public string color;
    public WireCuttingGame game;

    public void OnPointerClick(PointerEventData eventData)
    {
        game.cutWire(gameObject);
    }
}
