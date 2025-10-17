using UnityEngine;
using UnityEngine.Video;

public class IntroStage : BaseStage
{
    [Header("Video Settings")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip introVideo;

    public override void OnEnter()
    {
        Debug.Log("Intro Stage Enter");
        PlayIntroVideo();
    }

    public override void OnExit()
    {
        Debug.Log("Intro Stage Exit");

        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
    }

    private void PlayIntroVideo()
    {
        if (videoPlayer && introVideo)
        {
            videoPlayer.clip = introVideo;
            videoPlayer.Play();
        }
    }
}