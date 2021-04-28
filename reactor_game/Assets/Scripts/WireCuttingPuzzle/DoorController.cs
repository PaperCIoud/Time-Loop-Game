using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float animationDur = 1;
    public float rotationMag = 90;

    public void open()
    {
        StartCoroutine(rotationAnimation(rotationMag));
    }

    IEnumerator rotationAnimation(float yRot)
    {
        Quaternion targetRot = transform.localRotation * Quaternion.Euler(0, yRot, 0);
        while (transform.localRotation != targetRot)
        {
            float speed = rotationMag / animationDur;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRot, speed * Time.deltaTime);
            yield return null;
        }
    }
}
