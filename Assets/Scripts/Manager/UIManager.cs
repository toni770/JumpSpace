using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private Text trashText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private TextMeshProUGUI endMoneyText;

    [SerializeField] private TextMeshProUGUI endTrashText;

    [Header("OTHERS")]
    [SerializeField] private Image fuelSlider;
    [SerializeField] private GameObject reviveButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject multiplyButton;

    [SerializeField] private Animator fuelAnim;

    private SceneLoader _sceneLoader;

        private void Awake() {
        _sceneLoader = GetComponent<SceneLoader>();
    }
    //BUTTONS
    public void PlayGame()
    {
        //SceneManager.LoadScene((int)level.Game);
        gameMenu.SetActive(true);
        mainMenu.SetActive(false);

        coinsText.transform.parent.gameObject.SetActive(false);
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
            reviveButton.SetActive(!GameManager.Instance.revived);

        }  
    }

    public void Revive()
    {
        loseMenu.SetActive(false);
        gameMenu.SetActive(true);
    }

    public void Replay()
    {
        _sceneLoader.ReloadScene();
    }
   
    public void MainMenu()
    {
        _sceneLoader.ReloadScene();
    }

    public void OpenShop(bool shop)
    {        
        mainMenu.SetActive(!shop);
        shopMenu.SetActive(shop);
    }

    public void ShowNextBtn()
    {
        nextButton.SetActive(true);
    }

    public void OpenMoney(bool money)
    {
        shopMenu.SetActive(!money);
        moneyMenu.SetActive(money);
    }

    public void HideMultiply()
    {
        multiplyButton.SetActive(false);
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

    public void UpdateEndMoney(int actual)
    {
        endMoneyText.text = actual.ToString();
    }

    public void UpdateEndTrash(int actual, int max)
    {
        endTrashText.text = actual.ToString() + '/' + max.ToString();
    }

    //SliderFuel
    public void FuelDamaged()
    {
        fuelAnim.SetTrigger("Damaged");
    }

    public void FuelHealed()
    {
        fuelAnim.SetTrigger("Healed");
    }

    public void ReserveMode(bool reserve)
    {
        fuelAnim.SetBool("Reserve", reserve);
    }


}
