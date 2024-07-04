using UnityEngine;
using UnityEngine.SceneManagement;

public class UIOptions : MonoBehaviour
{        
    //intefaz button
    //[SerializeField] AudioSource buttonClip;
    [SerializeField] GameObject PauseOn;
    [SerializeField] bool paused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
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
        AudioManager.instance.Play("Button");
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        PauseOn.SetActive(paused);
    }

    public void RestartGame()
    {
        AudioManager.instance.Play("Button");
        paused = !paused;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuGame()
    {
        AudioManager.instance.Play("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

        //cambio de music
        AudioManager.instance.PlayMusic("MusicMainMenu");
        AudioManager.instance.Stop("MusicGameplay");
        AudioOptionManager.instance.GetAudio();
    }
}
