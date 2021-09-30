using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject endScreen;

    private void Awake()
    {
        GetComponent<GameManager>().EndGame += ShowEndScreen;
    }

    private void ShowEndScreen() { endScreen.SetActive(true); }

    //BUTTON FUNCTIONS
    public void PlayAgain() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
}
