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
    [SerializeField] private GameObject endMenu;
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
        SceneManager.LoadScene((int)level.Game);
    }

    public void EndGame(bool win)
    {
        gameMenu.SetActive(false);
        endMenu.SetActive(true);

        endText.text = win ? "Victory" : "Defeat";
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
