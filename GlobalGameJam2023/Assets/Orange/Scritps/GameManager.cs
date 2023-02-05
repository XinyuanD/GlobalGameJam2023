using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject deathScreen;

    private void Start()
    {
        deathScreen.SetActive(false);
    }

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

    public void InvokePlayerDeath()
    {
        deathScreen.SetActive(true);
        player.GetComponent<PlayerRespawn>().Respawn();
        player.GetComponent<PlayerMovement>().SetToIdleAnim();
        player.SetActive(false);
    }

    public void RespawnPlayer()
    {
        deathScreen.SetActive(false);
        player.SetActive(true);
    }

    public void BackToTitleScreen()
    {
        // reset the level complete count
        SwitchScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
