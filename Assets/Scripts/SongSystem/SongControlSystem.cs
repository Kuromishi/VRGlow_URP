using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SongControlSystem : MonoBehaviour
{
    public bool playing = false;
    bool songPlayed = false;

    public float timer = 0;
    int index = 0;
    string[][] timeLineData;
    int numberOfLine;

    public GameObject startButton;
    public GameObject songHintSakura;
    public GameObject waveDetect;

    public AK.Wwise.Event song;

    private void Start()
    {
        TextAsset baseAsset = Resources.Load("TimeLine", typeof(TextAsset)) as TextAsset;

        string[] lineTimeLineData = Regex.Split(baseAsset.text, "\r\n", RegexOptions.IgnoreCase);

        numberOfLine = lineTimeLineData.Length;

        timeLineData = new string[numberOfLine][];

        for (int i = 0 ; i < numberOfLine; i++)
        {
            timeLineData[i] = lineTimeLineData[i].Split(',');
            //Debug.Log(i);
            //Debug.Log(timeLineData[i][0]);
            //Debug.Log(timeLineData[i][1]);
        }
        
    }
    private void FixedUpdate()
    {
        if (playing)
        {
            if (!songPlayed)
            {
                AkSoundEngine.StopAll();
                song.Post(gameObject, (uint)AkCallbackType.AK_Marker, ButtonAppear);
                //GetComponent<AkEvent>().enabled = true;
                songPlayed = true;
            }
            if (index == numberOfLine + 1)
            {
                playing = false;
                songPlayed = false;
                timer = 0;
                Instantiate(startButton, gameObject.transform);
            }
        }
    }
    public void ButtonAppear(object in_cookie, AkCallbackType in_type, object in_info)
    {
        if (Convert.ToInt32(timeLineData[index][1]) == 1)
        {
            Instantiate(songHintSakura, gameObject.transform);
        }
        index++;
    }
    public void WaveDetectStart()
    {
        Instantiate(waveDetect, gameObject.transform);
    }
}