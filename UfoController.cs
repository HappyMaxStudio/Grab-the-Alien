using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class UfoController : MonoBehaviour
{
    private Rigidbody rb;
    private float moveX;
    private float moveZ;
    private Vector3 movement;
    public int speed;
    private int aliensCount;
    public GameObject successMessage;
    public GameObject failMessage;
    public GameObject pauseMenu;
    public TMP_Text countText;
    public TMP_Text AliensNeedText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveX = Input.acceleration.x; //taking input from the smartphone's accelerometer
        moveZ = Input.acceleration.y;
        AliensNeedText.text = AliensNeeded(SceneManager.GetActiveScene().buildIndex).ToString();
    }

    private void OnTriggerEnter(Collider other) //what happens if ufo collides with an other object with collider attached
    {
        if(other.gameObject.CompareTag("alien")) //if it collides with alien
        {
            aliensCount++;
            other.gameObject.SetActive(false);
            countText.text = aliensCount.ToString();
            if(aliensCount >= AliensNeeded(SceneManager.GetActiveScene().buildIndex))
            {
                Time.timeScale = 0.0f;
                successMessage.SetActive(true);
            }
        }
        else //if it collides with smth else(can only be an non-cloud space)
        {
            Time.timeScale = 0.0f;
            failMessage.SetActive(true);
        }
    }
    void Update()
    {
        movement = new Vector3(moveX, 0, moveZ);
        rb.AddForce(movement * speed);
    }

    
    public void SaveNewData() //saving points and passed level
    {
        PlayerPrefs.SetInt("CurrentPoints", PlayerPrefs.GetInt("CurrentPoints") + (SceneManager.GetActiveScene().buildIndex * 100));
        PlayerPrefs.SetInt("CurrentLevelPassed", SceneManager.GetActiveScene().buildIndex);
    }
    int AliensNeeded(int sceneIndex) //counting amount of aliens needed to pass current level
    {
        if (sceneIndex >= 1 && sceneIndex < 10)
        {
            return 3;
        }
        if (sceneIndex >= 10 && sceneIndex < 20)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }

    public void PauseMenuAction()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else { 
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LevelPassed()
    {
        SceneManager.LoadScene("SubMenu");
    }







}
