using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tutorialll : MonoBehaviour
{
    private VideoPlayer vp;
    public List<VideoClip> VideoClipList;

    public bool videoPlaying = false;
    bool videoPlayed = false;

    private float videoFrame;

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
            if (videoFrame >= vp.frameCount-1)//check whether the video is over
            {
                //Debug.Log("Spawn Particle Here!");

                vp.clip = VideoClipList[1];
                vp.Play();
                videoPlaying = false;
            }
        }


    }
}
