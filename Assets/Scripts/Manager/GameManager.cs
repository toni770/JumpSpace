using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int trashNeeded = 20;
    [SerializeField] [Range(0, 1)] private float percentExtraTrash = 0.2f;
    [SerializeField] private int actualLevel = 1;
    [SerializeField] private CameraController cameraController;

    public bool isPlaying { get; private set; } = false;

    private int actualTrash = 0;

    private UIManager uiManager;
    private LevelManager levelManager;
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
        levelManager = GetComponent<LevelManager>();
    }
    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        levelManager.LoadLevel(actualLevel);

        cameraController.target = levelManager.player.transform;

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

    public int GetTrashNeeded()
    {
        return trashNeeded;
    }

    public int GetExtraTrash()
    {
        return trashNeeded + (int)(trashNeeded * percentExtraTrash);
    }
}
