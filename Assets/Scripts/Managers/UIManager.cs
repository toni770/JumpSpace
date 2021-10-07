using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject endScreen;
    [SerializeField]
    private GameObject PlayScreen;

    [SerializeField]
    private Text collectorText;

    [SerializeField]
    private Text endText;


    public void ShowEndScreen(bool victory)
    { 
        endScreen.SetActive(true);
        PlayScreen.SetActive(false);

        endText.text = victory ? "YOU WIN" : "YOU LOSE";
    }

    public void UpdateCollectorText(int quantity, int minQuantity)
    {
        collectorText.text = quantity.ToString() + "/" + minQuantity;
    }
    //BUTTON FUNCTIONS
    public void PlayAgain() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

}
