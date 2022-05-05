using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tutorialll : MonoBehaviour
{
    public VideoPlayer vp;
    public List<VideoClip> videoClipList;
    public int videoIndex = 0;

    public bool videoPlaying = false;
    bool videoPlayed = false;

    public int perfectCount = 0;
    public int missCount = 0;
    public int[] CountList;
    public bool[] phaseBool;

    public float videoFrame;

    public GameObject sakuraPink;
    public GameObject sakuraBlue;
    public GameObject sakuraPink_Arrow;
    public GameObject sakuraBlue_Arrow;

    public GameObject waveDetect;
    public GameObject upDraft;

    public Transform step1Transform_P;
    public Transform step1Transform_B;
    public Transform step2Transform_P;
    public Transform step2Transform_B;

    private void Start()
    {
        videoIndex = 0;
        missCount = 0;
        perfectCount = 0;

        vp = gameObject.GetComponent<VideoPlayer>();
        vp.clip = videoClipList[videoIndex];

        videoPlaying = true;
    }
    private void Update()
    {
        videoFrame = vp.frame;
        if (videoPlaying && !videoPlayed)
        {
            videoPlayed = true;
            vp.Play();
        }

        if (perfectCount == CountList[videoIndex])
        {
            missCount = 0;
            perfectCount = 0;
            videoIndex++;
            vp.clip = videoClipList[videoIndex];
            vp.Play();
        }

        if (missCount == CountList[videoIndex])
        {
            missCount = 0;
            perfectCount = 0;
            phaseBool[videoIndex] = false;
            vp.Play();
        }

        if (vp.isPlaying)
        {
            if (vp.clip.name == "T1" && videoFrame >= vp.frameCount - 5 && phaseBool[videoIndex] == false)//check whether the video is over
            {
                phaseBool[videoIndex] = true;
                Debug.Log(videoFrame);
                Debug.Log(vp.frameCount);
                Instantiate(sakuraPink, step1Transform_P);
                Instantiate(sakuraBlue, step1Transform_B);
            }
            else if(vp.clip.name == "T2" && videoFrame >= vp.frameCount - 5 && !phaseBool[videoIndex])
            {
                phaseBool[videoIndex] = true;
                Debug.Log(videoFrame);
                Debug.Log(vp.frameCount);
                Instantiate(sakuraPink_Arrow, step1Transform_P);
                Instantiate(sakuraBlue_Arrow, step1Transform_B);
                Invoke("DetectStep2", 2f);
            }
            else if(vp.clip.name == "T3" && videoFrame >= vp.frameCount - 5 && !phaseBool[videoIndex])
            {
                phaseBool[videoIndex] = true;
                Debug.Log(videoFrame);
                Debug.Log(vp.frameCount);
                Instantiate(waveDetect, step1Transform_P);
            }
            else if(vp.clip.name == "T4" && videoFrame >= vp.frameCount - 5 && !phaseBool[videoIndex])
            {
                phaseBool[videoIndex] = true;
                Debug.Log(videoFrame);
                Debug.Log(vp.frameCount);
                Instantiate(upDraft, step1Transform_P);
                Instantiate(upDraft, step1Transform_B);
            }
        }
    }
    void DetectStep2()
    {
        Instantiate(sakuraPink, step2Transform_P);
        Instantiate(sakuraBlue, step2Transform_B);
    }
}