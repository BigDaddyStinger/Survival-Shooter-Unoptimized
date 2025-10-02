using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;

    void Start()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync("Pause");
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
