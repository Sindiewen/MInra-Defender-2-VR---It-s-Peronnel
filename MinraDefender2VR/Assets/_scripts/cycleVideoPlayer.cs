using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class cycleVideoPlayer : MonoBehaviour
{

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    [Range(0, 1)] public float Volume;
    public VideoClip[] clips;
    public double videoPlayTime;
    public double curClipLength;
    public bool videoPreparing;

    // Start is called before the first frame update
    void Awake()
    {
        // ensure we have the video play component
        videoPlayer = GetComponent<VideoPlayer>();
        //play video on awake
        videoPlayer.playOnAwake = true;
        
        loadNextVideo();
        StartCoroutine(initVideoPlayer());
        // loadNextVideo();


        // Get audio source component
        // audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // // If the video player has stopped playing load next video
        // if(curClipLength >= videoPlayer.time)
        // {
        //     loadNextVideo();
        // }
        // videoPlayer.SetTargetAudioSource(audioSource);
    videoPlayer.SetDirectAudioVolume(0, Volume);
    }

    IEnumerator initVideoPlayer()
    {
        // keep this always running
        while (true)
        {   
            // get video playetime
            videoPlayTime = videoPlayer.time;

            // if video player has reached end of video
            if(videoPlayer.time >= curClipLength || videoPreparing == true)
            {
                Debug.Log("Has reached end of video");
                // prepare fist video
                // If video is not prepared, prepare a video
                if(videoPreparing == false)
                {
                    Debug.Log("No video prepared. loading and preparing new video");
                    loadNextVideo();
                }
                
                // If a video is prepared, play video
                if(videoPlayer.isPrepared)
                {
                    Debug.Log("Video has been prepared, playing now");
                    videoPlayer.Play();
                    videoPreparing = false;
                }
            }



            // wait 1 frame
            yield return null;

        }
    }
    
    private void loadNextVideo()
    {

        videoPreparing = true;

        // load first video - random clip
        VideoClip newClip = clips[Random.Range(0, clips.Length-1)];
        videoPlayer.clip = newClip;
        videoPlayer.Prepare();

        // get the clip length
        curClipLength = newClip.length - 1;

        // // play said clip
        // videoPlayer.Play();
    }
}
