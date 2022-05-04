using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tutorialll : MonoBehaviour
{
    private VideoPlayer vp;
    public List<VideoClip> VideoClipList;

    public bool videoPlaying = false;
    private bool videoPlayed = false;

    private float videoFrame;

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
        vp.clip = VideoClipList[0];

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
            if (vp.clip.name == "T1" && videoFrame >= vp.frameCount - 1)//check whether the video is over
            {
                vp.clip = VideoClipList[1];

                Instantiate(sakuraPink, step1Transform_P.position, step1Transform_P.rotation);
                Instantiate(sakuraBlue, step1Transform_B.position, step1Transform_B.rotation);

                //if ()
                //{
                //    //只有在good或perfect的条件下才能play 不然直接重新生成
                //    
                //}

                vp.Play();

            }
            else if(vp.clip.name == "T2" && videoFrame >= vp.frameCount - 1)
            {
                vp.clip = VideoClipList[2];

                Instantiate(sakuraPink_Arrow, step1Transform_P.position, step1Transform_P.rotation);
                Instantiate(sakuraBlue_Arrow, step1Transform_B.position, step1Transform_B.rotation);
                Instantiate(sakuraPink, step2Transform_P.position, step2Transform_P.rotation);
                Instantiate(sakuraBlue, step2Transform_B.position, step2Transform_B.rotation);
                //vp.Play();
            }
            else if(vp.clip.name == "T3" && videoFrame >= vp.frameCount - 1)
            {
                vp.clip = VideoClipList[3];
               // vp.Play();
            }
            else if(vp.clip.name == "T4" && videoFrame >= vp.frameCount - 1)
            {
                // Tutorial over
            }
        }


    }
}
