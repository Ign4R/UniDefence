using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIOptions : MonoBehaviour
{        
    //intefaz button
    [SerializeField] AudioSource buttonClip;
    [SerializeField] GameObject PauseOn;
    [SerializeField] bool paused = false;
    [SerializeField] GameObject ContinueB;
    [SerializeField] GameObject RestartB;
    [SerializeField] GameObject MenuB;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    void Pause()
    {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        PauseOn.SetActive(paused);
    }

    public void ContinueGame()
    {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        PauseOn.SetActive(paused);
        //buttonClip.Play();
    }

    public void RestartGame()
    {
        //buttonClip.Play();
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuGame()
    {
        //buttonClip.Play();
        //SceneManager.LoadScene(changeScene);
        SceneManager.LoadScene("Title");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);       
    }
}
