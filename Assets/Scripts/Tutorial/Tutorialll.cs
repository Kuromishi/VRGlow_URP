using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tutorialll : MonoBehaviour
{
    public static VideoPlayer vp;
    public List<VideoClip> videoClipList;
    public static int videoIndex = 0;

    public bool videoPlaying = false;
    bool videoPlayed = false;

    public static int perfectCount = 0;
    public static int[] perfectCountList = { 2, 4, 0, 0 };

    public float videoFrame;

    public GameObject sakuraPink;
    public GameObject sakuraBlue;
    public GameObject sakuraPink_Arrow;
    public GameObject sakuraBlue_Arrow;

    public Transform step1Transform_P;
    public Transform step1Transform_B;
    public Transform step2Transform_P;
    public Transform step2Transform_B;

    private void Start()
    {
        vp = gameObject.GetComponent<VideoPlayer>();
        vp.clip = videoClipList[videoIndex];

        videoPlaying = true;
    }
    private void Update()
    {
        if (videoPlaying && !videoPlayed)
        {
            videoPlayed = true;
            vp.Play();
        }

        if(vp.isPlaying)
        {
            videoFrame = vp.frame;

            if (vp.clip.name == "T1" && videoFrame == vp.frameCount - 1)//check whether the video is over
            {
                Instantiate(sakuraPink, step1Transform_P);
                Instantiate(sakuraBlue, step1Transform_B);
            }
            else if(vp.clip.name == "T2" && videoFrame == vp.frameCount - 1)
            {
                Instantiate(sakuraPink_Arrow, step1Transform_P);
                Instantiate(sakuraBlue_Arrow, step1Transform_B);
                Invoke("DetectStep2", 1.1108f);
            }
            else if(vp.clip.name == "T3" && videoFrame == vp.frameCount - 1)
            {

            }
            else if(vp.clip.name == "T4" && videoFrame == vp.frameCount - 1)
            {

            }
        }
    }
    public static void PlayVideo()
    {
        perfectCount++;
        if (perfectCount == perfectCountList[videoIndex])
        {
            videoIndex++;
            vp.Play();
        }
        else
        {
            perfectCount = 0;
            Debug.Log(perfectCount);
            vp.Play();
        }
    }
    void DetectStep2()
    {
        Instantiate(sakuraPink, step2Transform_P);
        Instantiate(sakuraBlue, step2Transform_B);
    }
}