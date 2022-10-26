using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SubMenuControl : MonoBehaviour
{
    private int currentPoints;
    private int currentContent = 0;

    public TMP_Text pointsText;
    public TMP_Text contentNameText;
    public TMP_Text contentPriceText;
    public TMP_Text CurrentLevelText;

    public Image nextLevelImage;
    public Button contentSelectButton, contentPurchaseButton, watchVideoButton;

    [SerializeField] public Sprite img1, img2, img3, img4, img5, img6, img7, img8, img9, img10, img11, img12, img13, img14, img15, img16, img17, img18,
        img19, img20, img21, img22, img23, img24, img25, img26, img27, img28, img29, img30;

    public GameObject ufo1, ufo2, ufo3, ufo4, ufo5;
    public GameObject selectedNotice, purchasedNotice;
    public GameObject selectedText;

    List<Ufo> Ufolist = new List<Ufo>();


    void Start()
    {
        currentPoints = PlayerPrefs.GetInt("CurrentPoints");
        PointsToString();
        Sprite[] lvlPictures = new Sprite[] {img1, img2, img3, img4, img5, img6, img7, img8, img9, img10, img11, img12, img13, img14, img15, img16, img17, img18,
        img19, img20, img21, img22, img23, img24, img25, img26, img27, img28, img29, img30};
        nextLevelImage.sprite = lvlPictures[(PlayerPrefs.GetInt("CurrentLevelPassed"))];
        Ufo FirstUFO = new Ufo("Simple UFO",  "UFO1S", "UFO1p", 5000, ufo1, Convert.ToBoolean(PlayerPrefs.GetInt("Ufo1p")));
        Ufo SecondUFO = new Ufo("Second_UFO",  "UFO2S", "UFO2p", 10000, ufo2, Convert.ToBoolean(PlayerPrefs.GetInt("UFO2p")));
        Ufo ThirdUFO = new Ufo("Third_UFO",  "UFO3S", "UFO3p", 20000, ufo3, Convert.ToBoolean(PlayerPrefs.GetInt("UFO3p")));
        Ufo ForthUFO = new Ufo("Forth_UFO",  "UFO4S", "UFO4p", 25000, ufo4, Convert.ToBoolean(PlayerPrefs.GetInt("UFO4p")));
        Ufo FifthUFO = new Ufo("Fifth_UFO",  "UFO5S", "UFO5p", 30000, ufo5, Convert.ToBoolean(PlayerPrefs.GetInt("UFO5p")));
        Ufolist.Add(FirstUFO);
        Ufolist.Add(SecondUFO);
        Ufolist.Add(ThirdUFO);
        Ufolist.Add(ForthUFO);
        Ufolist.Add(FifthUFO);
        ContentToString();
        CurrentLevelText.text = "Level" + PlayerPrefs.GetInt("CurrentLevel") + 1;
    }

    public void ContentToString() //viewing current selected ufo's information
    {
        foreach(Ufo ufo in Ufolist) { ufo.gameModel.SetActive(false);}
        Ufolist[currentContent].gameModel.SetActive(true);
        contentNameText.text = Ufolist[currentContent].name.ToString();
        contentPriceText.text = Ufolist[currentContent].price.ToString();
        if (Ufolist[currentContent].isPurchased || currentPoints < Ufolist[currentContent].price) { contentPurchaseButton.interactable = false;}
        else { 
            contentPurchaseButton.interactable = true;
            watchVideoButton.interactable = true;    
        }
        if (Ufolist[currentContent].isPurchased) { contentSelectButton.interactable = true; };
        if(currentContent == PlayerPrefs.GetInt("CurrentSelected")) { 
            contentSelectButton.interactable = false;
            selectedText.SetActive(true);
        }
        else { selectedText.SetActive(false); }
    }

    public void SelectButtonAction()
    {
        selectedNotice.SetActive(true);
        PlayerPrefs.SetInt("CurrentSelected", currentContent);
    }

    public void PurchaseButtonAction()
    {
        currentPoints -= Ufolist[currentContent].price;
        PointsToString();
        SaveCurrentPoints();
        purchasedNotice.SetActive(true);
        Ufolist[currentContent].isPurchased = true;
        PlayerPrefs.SetInt(Ufolist[currentContent].purchasedSave, 1);
    }

    public void SwitchContentLeft()
    {
        currentContent--;
        if(currentContent < 0) { currentContent = 4; };
        ContentToString();
    }

    public void SwitchContentRight()
    {
        currentContent++;
        if (currentContent > 4) { currentContent = 0; };
        ContentToString();
    }

    void PointsToString() => pointsText.text = currentPoints.ToString();
    void SaveCurrentPoints() => PlayerPrefs.SetInt("CurrentPoints", currentPoints);
    public void LoadNextLevel() => SceneManager.LoadScene((PlayerPrefs.GetInt("CurrentLevelPassed") + 1));
    public void QuitGame() => Application.Quit();
    
    class Ufo
    {
        public string purchasedSave;
        public string name;
        public int price;
        public GameObject gameModel;
        public bool isPurchased;

        public Ufo(string name, string selsave, string pursave, int price, GameObject gameobject, bool purchased)
        {
            this.name = name;
            this.price = price;
            this.gameModel = gameobject;
            this.isPurchased = purchased;
        }
    }

}
