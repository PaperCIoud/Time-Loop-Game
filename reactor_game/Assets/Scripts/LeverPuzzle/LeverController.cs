using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour, Interactable
{
    public float animationDur = 1;
    public float rotationMag = 45;

    private PlayerController player;
    public bool triggered = false;

    public void enterInteract(PlayerController playerChar)
    {
        triggered = !triggered;
        player = playerChar;
        player.getMoveLock();
        float zRot = triggered ? -rotationMag : rotationMag;
        StartCoroutine(rotationAnimation(zRot));
    }

    public void exitInteract()
    {
        player.releaseMoveLock();
    }

    IEnumerator rotationAnimation(float zRot)
    {
        Quaternion targetRot = transform.localRotation * Quaternion.Euler(0, 0, zRot);
        while (transform.localRotation != targetRot)
        {
            float speed = rotationMag / animationDur;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRot, speed * Time.deltaTime);
            yield return null;
        }
        exitInteract();
    }
}
