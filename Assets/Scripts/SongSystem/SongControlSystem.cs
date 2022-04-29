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

    [Header("SongDetectOriginPosition")]
    public Vector3 leftPosition;
    public Vector3 rightPosition;

    int index = 0;
    string[][] timeLineData;
    int numberOfLine;
    //时间轴标记：
    //0：none；
    //1：平敲；
    //2：上敲；
    //2.5：wave起手；
    //3：wave；
    //3.5：wave(仅判定，无动画)；
    //4：里跳低敲；
    //5：里跳高敲；
    //6：上升气流；
    //7：左右上滑；
    //8：画心；
    //9：敲三下；
    //10：欢呼；

    [Header("SongDetect")]
    public GameObject startButton;
    public GameObject songHintSakura_Blue;
    public GameObject songHintSakura_Pink;
    public GameObject waveDetect;
    public GameObject upDraft;

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
            //if (float.Parse(timeLineData[i][1]) == 1)
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
        GameObject Hint;
        switch (float.Parse(timeLineData[index][1]))
        {
            case 1:
                //whole li da 2
                Vector3 bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 0;

                Vector3 pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 1;

                Invoke("AnimLiDa2", 1);

                break;

            case 2:
                //whole gao da
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 0;

                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 1;

                Invoke("AnimLiDa2", 1);

                break;

            case 2.5f:
                //wave start
                Hint = Instantiate(waveDetect, gameObject.transform);
                Hint.GetComponent<WaveDetect>().needHint = true;

                Invoke("AnimWaveStart", 0.5f);

                break;

            case 3:
                //wave
                Hint = Instantiate(waveDetect, gameObject.transform);
                Hint.GetComponent<WaveDetect>().needHint = false;

                Invoke("AnimWave", 1.5f);

                break;

            case 3.5f:
                Hint = Instantiate(waveDetect, gameObject.transform);
                Hint.GetComponent<WaveDetect>().needHint = false;
                break;

            case 4:
                //Whole_LiDa2
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 0;

                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 1;

                Invoke("AnimLiDa2", 1);

                break;

            case 5:
                //Whole_GaoDa
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 0;

                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 1;

                Invoke("AnimLiDa2", 1);

                break;

            case 6:
                //Whole_Updraft
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                Hint = Instantiate(upDraft, gameObject.transform);
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;

                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                Hint = Instantiate(upDraft, gameObject.transform);
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;

                Invoke("AnimLiDa2", 1);

                break;
        }
        index++;
    }
    public void AnimLiDa2()
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Play("LiDa2_whole");
        }
    }
    public void AnimWaveStart()
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Play("IdleToWave");
        }
    }
    public void AnimWave()
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Play("Wave_ShirtGuy02");
        }
    }
    public void AnimGaoDa()
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Play("Wave_ShirtGuy02");
        }
    }
    public void AnimUpDraft()
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Play("UpDraft_ShirtGuy02");
        }
    }
}