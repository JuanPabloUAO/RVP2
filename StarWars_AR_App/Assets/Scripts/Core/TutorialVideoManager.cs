using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TutorialVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        GoToMenu();
    }

    public void Skip()
    {
        GoToMenu();
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}