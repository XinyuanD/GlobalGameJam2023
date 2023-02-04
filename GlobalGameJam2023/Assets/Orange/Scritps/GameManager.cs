using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        // potentially fade in and fade out??
        SceneManager.LoadScene(sceneName);
    }

    public void SetVolume(float volume)
    {
        Debug.Log("Volume set to " + volume);
    }

    public void SetBrightness(float brightness)
    {
        Debug.Log("Brightness set to " + brightness);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
