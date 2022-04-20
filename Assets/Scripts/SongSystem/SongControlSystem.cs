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

    Vector3 leftPosition = new Vector3(0, 0, -0.35f);
    Vector3 rightPosition = new Vector3(0, 0, 0.35f);

    int index = 0;
    string[][] timeLineData;
    int numberOfLine;

    [Header("SongDetect")]
    public GameObject startButton;
    public GameObject songHintSakura_Blue;
    public GameObject songHintSakura_Pink;
    public GameObject waveDetect;

    [Header("NPC Animation")]
    public GameObject npcController;
    Animator[] anims;

    [Header("Wwise Event")]
    public AK.Wwise.Event song;

    private void Start()
    {
        anims = npcController.GetComponentsInChildren<Animator>();

        index = 0;

        TextAsset baseAsset = Resources.Load("TimeLine", typeof(TextAsset)) as TextAsset;

        string[] lineTimeLineData = Regex.Split(baseAsset.text, "\r\n", RegexOptions.IgnoreCase);

        numberOfLine = lineTimeLineData.Length;

        timeLineData = new string[numberOfLine][];

        for (int i = 0 ; i < numberOfLine; i++)
        {
            timeLineData[i] = lineTimeLineData[i].Split(',');
            //if (Convert.ToInt32(timeLineData[i][1]) == 1)
            //{
            //    Debug.Log(float.Parse(timeLineData[i][2]));
            //    Debug.Log(float.Parse(timeLineData[i][3]));
            //    Debug.Log(float.Parse(timeLineData[i][4]));
            //    Debug.Log(float.Parse(timeLineData[i][5]));
            //    Debug.Log(float.Parse(timeLineData[i][6]));
            //    Debug.Log(float.Parse(timeLineData[i][7]));
            //}
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
                song.Post(gameObject, (uint)AkCallbackType.AK_Marker, DetectAppear);
                songPlayed = true;
            }
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
    public void DetectAppear(object in_cookie, AkCallbackType in_type, object in_info)
    {
        if (Convert.ToInt32(timeLineData[index][1]) == 1)
        {
            Vector3 bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
            GameObject
            Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
            Hint.transform.position += leftPosition;
            Hint.transform.position += bluePosition;
            Vector3 pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
            Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
            Hint.transform.position += rightPosition;
            Hint.transform.position += pinkPosition;

            for (int i = 0; i < anims.Length; i++)
            {
                //anims[i].Play();
            }
        }
        index++;
    }
}