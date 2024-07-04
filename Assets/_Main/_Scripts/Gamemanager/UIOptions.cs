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
        if (Input.GetKeyDown(KeyCode.P) && paused == false) 
        {
            Pause();
        }
    }

    void Pause()
    {
        GameManager.instance.IsPlayerOn(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        paused = !paused;
        PauseOn.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }

    public void ContinueGame()
    {
        GameManager.instance.IsPlayerOn(true);
        Cursor.visible = false;
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
