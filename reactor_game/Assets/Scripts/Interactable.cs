using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    void enterInteract(PlayerController playerChar);

    void exitInteract();
}
