using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("MENUS")]
    [SerializeField]
    private GameObject gameMenu;
    [SerializeField]
    private GameObject endMenu;
    [Header("TEXTS")]
    [SerializeField]
    private Text endText;
    [SerializeField]
    private Text trashText;
    [Header("OTHERS")]
    [SerializeField]
    private Image fuelSlider;
    public void UpdateTrash(int actual, int max)
    {
        trashText.text = actual.ToString() + '/' + max.ToString();
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

    public void UpdateFuel(float actual, float max)
    {
        fuelSlider.fillAmount = actual / max;
    }

}
