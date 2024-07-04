using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIOptions : MonoBehaviour
{        
    //intefaz button
    //[SerializeField] AudioSource buttonClip;
    [SerializeField] GameObject PauseOn;
    [SerializeField] bool paused = false;
    [SerializeField] Button[] buttonsPause;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && paused == false) 
        {
            Pause();
        }
    }

    void Pause()
    {
        buttonsPause[1].interactable = true;
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
        Cursor.visible = true;
        AudioManager.instance.Play("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
