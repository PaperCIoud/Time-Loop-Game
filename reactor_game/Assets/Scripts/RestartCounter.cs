using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartCounter : MonoBehaviour
{
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void increment()
    {
        count += 1;
    }
}
