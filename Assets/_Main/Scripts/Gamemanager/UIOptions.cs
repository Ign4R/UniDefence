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
        if (Input.GetKeyDown(KeyCode.P))
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
    }
}
