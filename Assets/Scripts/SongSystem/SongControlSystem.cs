using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SongControlSystem : MonoBehaviour
{
    float timer = 0;

    public static int score = 0;
    public static int scoreRatio;
    public static int combo = 0;
    int highestCombo = 0;

    public bool playing = false;
    bool songPlayed = false;
    bool scoreShowed = false;

    public Text scoreText;
    public Text comboText;

    [Header("SongDetectOriginPosition")]
    public Vector3 leftPosition;
    public Vector3 rightPosition;

    int index = 0;
    string[][] timeLineData;
    int numberOfLine;
    //时间轴标记：
    //0：none；
    //1：平敲；1.1:仅左侧；1.2：仅右侧
    //2：上敲；2.1:仅左侧；2.2：仅右侧
    //2.5：wave起手；
    //3：wave(仅判定，无动画)；
    //3.5：wave；
    //4：里跳低敲；4.1:仅左侧；4.2：仅右侧
    //5：里跳高敲；5.1:仅左侧；5.2：仅右侧
    //6：上升气流；6.1:仅左侧；6.2：仅右侧
    //8：画心；
    //9：敲三下（起手）；9.1：仅出现判定点，用于三连敲的第二、三下
    //10：欢呼；

    [Header("SongDetect")]
    public GameObject startButton;
    public GameObject songHintSakura_Blue;
    public GameObject songHintSakura_Blue_Arrow;
    public GameObject songHintSakura_Pink;
    public GameObject songHintSakura_Pink_Arrow;
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
            //Debug.Log(float.Parse(timeLineData[i][0]));
            //Debug.Log(float.Parse(timeLineData[i][1]));

            //Debug.Log(float.Parse(timeLineData[i][2]));
            //Debug.Log(float.Parse(timeLineData[i][3]));
            //Debug.Log(float.Parse(timeLineData[i][4]));

            //Debug.Log(float.Parse(timeLineData[i][5]));
            //Debug.Log(float.Parse(timeLineData[i][6]));
            //Debug.Log(float.Parse(timeLineData[i][7]));

            //Debug.Log(float.Parse(timeLineData[i][8]));
            //Debug.Log(float.Parse(timeLineData[i][9]));
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
                combo = 0;
                highestCombo = 0;
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

        scoreText.text = "Score\n" + score;
        comboText.text = "Combo\n" + combo;

        if (combo > highestCombo)
        {
            highestCombo = combo;
        }
        scoreRatio = (combo / 20) + 1;
        Debug.Log("Score Ratio: " + scoreRatio);
    }
    public void DetectAppear(object in_cookie, AkCallbackType in_type, object in_info)
    {
        GameObject Hint;
        switch (float.Parse(timeLineData[index][1]))
        {
            case 1:
                //whole li da 2
                Vector3 bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                if (float.Parse(timeLineData[index][8]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3 (0, 0, float.Parse(timeLineData[index][8]));
                    Hint = Instantiate(songHintSakura_Blue_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 0;

                Vector3 pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                if (float.Parse(timeLineData[index][9]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][9]));
                    Hint = Instantiate(songHintSakura_Pink_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 1;

                Invoke("AnimLiDa2", 1);

                break;

            case 1.1f:
                //whole li da 2
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                if (float.Parse(timeLineData[index][8]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][8]));
                    Hint = Instantiate(songHintSakura_Blue_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 0;

                Invoke("AnimLiDa2", 1);

                break;

            case 1.2f:
                //whole li da 2
                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                if (float.Parse(timeLineData[index][9]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][9]));
                    Hint = Instantiate(songHintSakura_Pink_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 1;

                Invoke("AnimLiDa2", 1);

                break;


            case 2:
                //whole gao da
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                if (float.Parse(timeLineData[index][8]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][8]));
                    Hint = Instantiate(songHintSakura_Blue_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 0;

                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                if (float.Parse(timeLineData[index][9]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][9]));
                    Hint = Instantiate(songHintSakura_Pink_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 1;

                Invoke("AnimGaoDa", 0.92f);

                break;

            case 2.1f:
                //whole gao da
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                if (float.Parse(timeLineData[index][8]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][8]));
                    Hint = Instantiate(songHintSakura_Blue_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 0;

                Invoke("AnimGaoDa", 0.92f);

                break;

            case 2.2f:
                //whole gao da
                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                if (float.Parse(timeLineData[index][9]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][9]));
                    Hint = Instantiate(songHintSakura_Pink_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 1;

                Invoke("AnimGaoDa", 0.92f);

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

                break;

            case 3.5f:
                Hint = Instantiate(waveDetect, gameObject.transform);
                Hint.GetComponent<WaveDetect>().needHint = true;

                Invoke("AnimWave", 1.5f);

                break;

            case 4:
                //Whole_LiDa2
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                if (float.Parse(timeLineData[index][8]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][8]));
                    Hint = Instantiate(songHintSakura_Blue_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 0;

                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                if (float.Parse(timeLineData[index][9]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][9]));
                    Hint = Instantiate(songHintSakura_Pink_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 1;

                Invoke("AnimLiDa", 1.13f);

                break;

            case 4.1f:
                //Whole_LiDa2
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                if (float.Parse(timeLineData[index][8]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][8]));
                    Hint = Instantiate(songHintSakura_Blue_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 0;

                Invoke("AnimLiDa", 1.13f);

                break;

            case 4.2f:
                //Whole_LiDa2
                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                if (float.Parse(timeLineData[index][9]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][9]));
                    Hint = Instantiate(songHintSakura_Pink_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 1;

                Invoke("AnimLiDa", 1.13f);

                break;

            case 5:
                //Whole_GaoDa
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                if (float.Parse(timeLineData[index][8]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][8]));
                    Hint = Instantiate(songHintSakura_Blue_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 0;

                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                if (float.Parse(timeLineData[index][9]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][9]));
                    Hint = Instantiate(songHintSakura_Pink_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 1;

                break;

            case 5.1f:
                //Whole_GaoDa
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                if (float.Parse(timeLineData[index][8]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Blue, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][8]));
                    Hint = Instantiate(songHintSakura_Blue_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 0;

                break;

            case 5.2f:
                //Whole_GaoDa
                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                if (float.Parse(timeLineData[index][9]) == 200)
                {
                    Hint = Instantiate(songHintSakura_Pink, gameObject.transform);
                }
                else
                {
                    Vector3 rotateAngle = new Vector3(0, 0, float.Parse(timeLineData[index][9]));
                    Hint = Instantiate(songHintSakura_Pink_Arrow, gameObject.transform);
                    Hint.transform.GetChild(2).transform.Rotate(rotateAngle);
                }
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;
                Hint.GetComponent<SongButtonHintSakura>().hintType = 1;

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

                Invoke("AnimUpDraft", 1.16f);

                break;

            case 6.1f:
                //Whole_Updraft
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                Hint = Instantiate(upDraft, gameObject.transform);
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;

                Invoke("AnimUpDraft", 1.16f);

                break;

            case 6.2f:
                //Whole_Updraft
                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                Hint = Instantiate(upDraft, gameObject.transform);
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;

                Invoke("AnimUpDraft", 1.16f);

                break;
        }
        index++;
    }
    public void AnimLiDa()//里跳起手
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Play("LiDa_whole");
        }
    }
    public void AnimLiDa2()//平敲
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Play("LiDa2_whole");
        }
    }
    public void AnimWaveStart()//wave
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
    public void AnimGaoDa()//高打
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Play("Whole_GaoDa");
        }
    }
    public void AnimUpDraft()//上升气流
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Play("UpDraft_ShirtGuy02");
        }
    }
}