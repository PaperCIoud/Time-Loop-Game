using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyController : MonoBehaviour
{
    public GameObject[] path;
    public int pathTarget = 0;
    public GameObject gameCont;
    public float segmentDuration = 0.7f;

    public void Start()
    {
        animate();
    }

    public void animate()
    {
        StartCoroutine(MoveTrack());
        AudioSource reverseFX = gameObject.GetComponent<AudioSource>();
        reverseFX.Play();
        Camera animCam = gameObject.GetComponent<Camera>();
        animCam.enabled = true;
    }

    public IEnumerator MoveTrack()
    {
        Vector3 start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 target = new Vector3(path[pathTarget].transform.position.x, path[pathTarget].transform.position.y + 0.5f, path[pathTarget].transform.position.z);
        Quaternion targetRot = path[pathTarget].transform.localRotation * Quaternion.Euler(0, 180, 0);
        float elapsedTime = 0;
        float speed = 20f / segmentDuration;

        while (elapsedTime < segmentDuration)
        {
            transform.position = Vector3.Lerp(start, target, (elapsedTime / segmentDuration));
            elapsedTime += Time.deltaTime;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRot, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
        pathTarget += 1;
        if (pathTarget < path.Length)
        {
            StartCoroutine(MoveTrack());
        }
        else
        {
            GameController control = gameCont.GetComponent<GameController>();
            control.resetScene();
        }
    }
}
