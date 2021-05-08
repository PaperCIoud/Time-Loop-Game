using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartCounter : MonoBehaviour
{
    public int count = 0;

    private float volSetting = 1.0f;
    private float sensSetting = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void increment()
    {
        count += 1;
    }

    public void setSensitivitySetting(float sens)
    {
        sensSetting = sens;
    }

    public float getSensitivitySetting()
    {
        return sensSetting;
    }

    public void setVolumeSetting(float vol)
    {
        volSetting = vol;
    }

    public float getVolumeSetting()
    {
        return volSetting;
    }
}
