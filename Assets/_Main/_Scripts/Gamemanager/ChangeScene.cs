using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] GameObject CreditOn;
    [SerializeField] GameObject SettingOn;
    [SerializeField] bool paused = false;

    public void PlayAgain()
    {
        AudioManager.instance.Play("Button");
        SceneManager.LoadScene(1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //cambio de music
        AudioManager.instance.Stop("MusicMainMenu");
        AudioManager.instance.PlayMusic("MusicGameplay");
        AudioOptionManager.instance.GetAudio();
    }

    public void Credit()
    {
        AudioManager.instance.Play("Button");
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        CreditOn.SetActive(paused);
    }

    public void Setting()
    {
        AudioManager.instance.Play("Button");
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        SettingOn.SetActive(paused);
    }
    
    public void ContinueGame()
    {
        AudioManager.instance.Play("Button");
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        CreditOn.SetActive(paused);
    }

    public void Exit()
    {
        AudioManager.instance.Play("Button");
        Application.Quit();
    }

}
