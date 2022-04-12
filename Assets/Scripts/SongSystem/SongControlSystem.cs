using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SongControlSystem : MonoBehaviour
{
    float timer = 0;

    public static int score = 0;

    public bool playing = false;
    bool songPlayed = false;
    bool scoreShowed = false;

    int index = 0;
    string[][] timeLineData;
    int numberOfLine;

    public GameObject startButton;
    public GameObject songHintSakura;
    public GameObject waveDetect;

    public AK.Wwise.Event song;

    private void Start()
    {
        index = 0;

        TextAsset baseAsset = Resources.Load("TimeLine", typeof(TextAsset)) as TextAsset;

        string[] lineTimeLineData = Regex.Split(baseAsset.text, "\r\n", RegexOptions.IgnoreCase);

        numberOfLine = lineTimeLineData.Length;

        timeLineData = new string[numberOfLine][];

        for (int i = 0 ; i < numberOfLine; i++)
        {
            timeLineData[i] = lineTimeLineData[i].Split(',');
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
                score = 0;
                song.Post(gameObject, (uint)AkCallbackType.AK_Marker, ButtonAppear);
                songPlayed = true;
            }
            //if (index == numberOfLine)
            //{
            //    playing = false;
            //    songPlayed = false;
            //    Instantiate(startButton, gameObject.transform);
            //}
        }
        if (index == numberOfLine - 1 && !scoreShowed)
        {
            timer += Time.deltaTime;
            if (timer >= 3.0)
            {
                Debug.Log("Final Score: " + score);
                scoreShowed = true;
                timer = 0;
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