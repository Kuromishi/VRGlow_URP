using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongControlSystem : MonoBehaviour
{
    public GameObject Button;

    public GameObject WaveDetect;

    void ButtonAppear()
    {
        Instantiate(Button, gameObject.transform);
    }
    void WaveDetectStart()
    {
        Instantiate(WaveDetect, gameObject.transform);
    }
}