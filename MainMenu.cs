using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject ufo;
    public int rotationSpeed;
    public void LoadSubmenu()
    {
        SceneManager.LoadScene("SubMenu");
    }

    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        ufo.transform.Rotate(0, 0, rotationSpeed);
    }
}
