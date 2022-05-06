using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tutorialll : MonoBehaviour
{
    bool leftAttached = false;
    bool rightAttached = false;

    public VideoPlayer vp;
    public List<VideoClip> videoClipList;
    public int videoIndex = 0;

    bool videoPlaying = false;
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
    public GameObject waveDetectPre;
    public GameObject upDraft_Blue;
    public GameObject upDraft_Pink;

    public GameObject uIObject;
    public Transform uiPos;

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

        //videoPlaying = true;
    }
    private void Update()
    {
        videoFrame = vp.frame;

        if (leftAttached && rightAttached && !videoPlayed)
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

        if (missCount > 0)
        {
            missCount = 0;
            perfectCount = 0;
            videoFrame = 0;
            phaseBool[videoIndex] = false;
            vp.Play();
        }

        if (vp.isPlaying)
        {
            if (vp.clip.name == "T1" && videoFrame >= vp.frameCount - 5 && !phaseBool[videoIndex])
            {
                phaseBool[videoIndex] = true;
                Instantiate(sakuraPink, step1Transform_P);
                Instantiate(sakuraBlue, step1Transform_B);
            }
            else if(vp.clip.name == "T2" && videoFrame >= vp.frameCount - 5 && !phaseBool[videoIndex])
            {
                phaseBool[videoIndex] = true;
                Instantiate(sakuraPink_Arrow, step1Transform_P);
                Instantiate(sakuraBlue_Arrow, step1Transform_B);
                Invoke(nameof(DetectStep2), 2f);
            }
            else if(vp.clip.name == "T3" && videoFrame >= vp.frameCount - 5 && !phaseBool[videoIndex])
            {
                phaseBool[videoIndex] = true;
                WavePreAppear();
                Invoke(nameof(WaveAppearHint), 1.4f);
                Invoke(nameof(WaveAppearNoHint), 2.5108f);
                Invoke(nameof(WaveAppearHint), 3.6216f);
                Invoke(nameof(WaveAppearNoHint), 4.7324f);
            }
            else if(vp.clip.name == "T4" && videoFrame >= vp.frameCount - 5 && !phaseBool[videoIndex])
            {
                phaseBool[videoIndex] = true;
                Instantiate(upDraft_Pink, step1Transform_P);
                Instantiate(upDraft_Blue, step1Transform_B);
                Invoke(nameof(UiUp), 6);
            }
        }
    }
    void DetectStep2()
    {
        Instantiate(sakuraPink, step2Transform_P);
        Instantiate(sakuraBlue, step2Transform_B);
    }
    void WaveAppearHint()
    {
        GameObject Hint = Instantiate(waveDetect, gameObject.transform);
        Hint.GetComponent<WaveDetect>().needHint = true;
    }
    void WaveAppearNoHint()
    {
        GameObject Hint = Instantiate(waveDetect, gameObject.transform);
        Hint.GetComponent<WaveDetect>().needHint = false;
    }
    void WavePreAppear()
    {
        Instantiate(waveDetectPre, gameObject.transform);
    }
    void UiUp()
    {
        Instantiate(uIObject, uiPos);
        Debug.Log("1");
    }
    public void LeftAttached()
    {
        leftAttached = true;
    }
    public void RightAttached()
    {
        rightAttached = true;
    }
}