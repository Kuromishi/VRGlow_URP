using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SongControlSystem : MonoBehaviour
{
    public static int score = 0;
    public static int scoreRatio;
    public static int combo = 0;
    int highestCombo = 0;

    public bool playing = false;
    bool songPlayed = false;

    [Header("Score Text")]
    public Text scoreText;
    public Text comboText;

    [Header("Score TV Animator")]
    public Animator leftTVanim;
    public Animator rightTVanim;
    bool end = false;

    [Header("SongDetectOriginPosition")]
    public Vector3 leftPosition;
    public Vector3 rightPosition;

    int index = 0;
    string[][] timeLineData;
    int numberOfLine;
    //ʱ�����ǣ�
    //0��none��
    //1��ƽ�ã�1.1:����ࣻ1.2�����Ҳ�
    //2�����ã�2.1:����ࣻ2.2�����Ҳ�
    //2.4��wave321��ʾ������2.5��1.5sǰ�棻
    //2.5��wave���֣�
    //3��wave(���ж����޶���)��
    //3.5��wave��
    //4���������ã�4.1:����ࣻ4.2�����Ҳ�
    //5���������ã�5.1:����ࣻ5.2�����Ҳ�
    //6������������6.1:����ࣻ6.2�����Ҳ�
    //7���Ż𣡣�����
    //8�����ģ�
    //9�������£����֣���9.1���������ж��㣬���������õĵڶ�������
    //10��������

    [Header("SongDetect")]
    public GameObject songHintSakura_Blue;
    public GameObject songHintSakura_Blue_Arrow;
    public GameObject songHintSakura_Pink;
    public GameObject songHintSakura_Pink_Arrow;
    public GameObject waveDetect;
    public GameObject waveDetectPre;
    public GameObject upDraft_Blue;
    public GameObject upDraft_Pink;

    [Header("NPC Animation")]
    public GameObject npcController;
    Animator[] anims;

    [Header("Fire In The Middle")]
    public ParticleSystem[] fires;

    [Header("End Fires")]
    public GameObject endFire;
    public Transform[] endFirePositions;

    [Header("Wwise Event")]
    public AK.Wwise.Event song;
    public AK.Wwise.Event audience;
    public AK.Wwise.Event endFirework;
    public AK.Wwise.Event fireInMiddle;

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
        if (playing && !songPlayed)
        {
            songPlayed = true;
            AkSoundEngine.StopAll();
            score = 0;
            combo = 0;
            highestCombo = 0;
            end = false;
            song.Post(gameObject, (uint)AkCallbackType.AK_Marker, DetectAppear);
            GetComponentInChildren<VideoPlayer>().Play();
        }

        if (combo > highestCombo)
        {
            highestCombo = combo;
        }

        if (!end)
        {
            scoreText.text = "Score\n" + score;
            comboText.text = "Combo\n" + combo;
        }
        else
        {
            scoreText.text = "Score\n" + score;
            comboText.text = "Best Combo\n" + highestCombo;
        }

        scoreRatio = (combo / 20) + 1;
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

            case 2.4f:
                //wave start
                Hint = Instantiate(waveDetectPre, gameObject.transform);

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
                Hint = Instantiate(upDraft_Blue, gameObject.transform);
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;

                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                Hint = Instantiate(upDraft_Pink, gameObject.transform);
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;

                Invoke("AnimUpDraft", 1.16f);

                break;

            case 6.1f:
                //Whole_Updraft
                bluePosition = new Vector3(float.Parse(timeLineData[index][2]), float.Parse(timeLineData[index][3]), float.Parse(timeLineData[index][4]));
                Hint = Instantiate(upDraft_Blue, gameObject.transform);
                Hint.transform.position += leftPosition;
                Hint.transform.position += bluePosition;

                Invoke("AnimUpDraft", 1.16f);

                break;

            case 6.2f:
                //Whole_Updraft
                pinkPosition = new Vector3(float.Parse(timeLineData[index][5]), float.Parse(timeLineData[index][6]), float.Parse(timeLineData[index][7]));
                Hint = Instantiate(upDraft_Pink, gameObject.transform);
                Hint.transform.position += rightPosition;
                Hint.transform.position += pinkPosition;

                Invoke("AnimUpDraft", 1.16f);

                break;

            case 7:
                for (int i = 0; i < fires.Length; i++)
                {
                    fires[i].Play();
                }
                fireInMiddle.Post(gameObject);
                break;

            case 7.1f:
                for (int i = 0; i < fires.Length; i++)
                {
                    fires[i].Stop();
                }
                fireInMiddle.Post(gameObject);
                break;

            case 999:
                leftTVanim.Play("LeftTV");
                rightTVanim.Play("RightTV");
                audience.Post(gameObject);
                endFirework.Post(gameObject);
                GetComponentInChildren<VideoPlayer>().Stop();
                end = true;
                for (int i = 0; i < anims.Length; i++)
                {
                    anims[i].Play("Whole_RandomWave");
                }
                for (int i = 0; i < endFirePositions.Length; i++)
                {
                    Instantiate(endFire, endFirePositions[i]);
                }
                break;
        }
        index++;
    }
    public void AnimLiDa()//��������
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Play("LiDa_whole");
        }
    }
    public void AnimLiDa2()//ƽ��
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
    public void AnimGaoDa()//�ߴ�
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Play("Whole_GaoDa");
        }
    }
    public void AnimUpDraft()//��������
    {
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Play("UpDraft_ShirtGuy02");
        }
    }
}