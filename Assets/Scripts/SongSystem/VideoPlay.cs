using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlay : MonoBehaviour
{
    VideoPlayer videoPlayer;
    public bool videoPlaying = false;
    bool videoPlayed = false;
    private void Start()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
    }
    private void Update()
    {
        if (videoPlaying && !videoPlayed)
        {
            videoPlayed = true;
            videoPlayer.Play();
        }
    }
}