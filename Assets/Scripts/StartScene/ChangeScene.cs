using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void StartScene()
    {
        AkSoundEngine.StopAll();
        SceneManager.LoadScene("StartScene");
    }
    public void TutorialScene()
    {
        AkSoundEngine.StopAll();
        SceneManager.LoadScene("TutorialTest");
    }

    public void GameScene()
    {
        AkSoundEngine.StopAll();
        SceneManager.LoadScene("VRGame");
    }

    public void Restart()
    {
        AkSoundEngine.StopAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        AkSoundEngine.StopAll();
        Application.Quit();
    }
}
