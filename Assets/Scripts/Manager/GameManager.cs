using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int trashNeeded = 20;

    public bool isPlaying { get; private set; } = false;

    private int actualTrash = 0;

    private UIManager uiManager;
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is Null");
            }
            
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        uiManager = GetComponent<UIManager>();
    }
    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        isPlaying = true;
        actualTrash = 0;
        uiManager.UpdateTrash(actualTrash, trashNeeded);
    }

    public void EndGame(bool win)
    {
        isPlaying = false;
        uiManager.EndGame(win);
    }

    public void GetTrash()
    {
        actualTrash += 1;
        uiManager.UpdateTrash(actualTrash, trashNeeded);
    }

    public void CheckEnd()
    {
        if(actualTrash >= trashNeeded && isPlaying)
        {
            EndGame(true);
        }
    }

    public void UpdateFuel(float actual, float max)
    {
        uiManager.UpdateFuel(actual, max);
    }
}
