using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private enum level
    {
        MainMenu,
        Game, 
        Shop
    }

    [Header("MENUS")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject moneyMenu;
    [Header("TEXTS")]
    [SerializeField] private Text endText;
    [SerializeField] private Text trashText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [Header("OTHERS")]
    [SerializeField] private Image fuelSlider;

    //BUTTONS
    public void PlayGame()
    {
        //SceneManager.LoadScene((int)level.Game);
        gameMenu.SetActive(true);
        mainMenu.SetActive(false);

        coinsText.gameObject.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void EndGame(bool win)
    {
        gameMenu.SetActive(false);
        if(win)
        {
            winMenu.SetActive(true);
        }
        else
        {
            loseMenu.SetActive(true);
        }
        
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
    public void MainMenu()
    {
        SceneManager.LoadScene((int)level.MainMenu);
    }

    public void OpenShop(bool shop)
    {
        mainMenu.SetActive(!shop);
        shopMenu.SetActive(shop);
    }

    public void OpenMoney(bool money)
    {
        shopMenu.SetActive(!money);
        moneyMenu.SetActive(money);
    }

    //TEXTS
    public void UpdateTrash(int actual, int max)
    {
        trashText.text = actual.ToString() + '/' + max.ToString();
    }

    public void UpdateFuel(float actual, float max)
    {
        fuelSlider.fillAmount = actual / max;
    }

    public void UpdateCurrentLevel(int actual)
    {
        levelText.text = actual.ToString();
    }

    public void UpdateCoins(int actual)
    {
        coinsText.text = actual.ToString();
    }

}
