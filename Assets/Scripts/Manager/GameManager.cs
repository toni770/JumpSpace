using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int[] trashNeeded;
    [SerializeField] [Range(0, 1)] private float percentExtraTrash = 0.2f;
    [SerializeField] private CameraController cameraController;

    [Header("DEBUG")]
    [SerializeField] private bool debugMode = false;
    [SerializeField] private int actualLevel = 1;

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
        if (!debugMode)
        {
            if (DataManager.Instance != null)
            {
                actualLevel = DataManager.Instance.actualLevel;

                levelManager.LoadLevel(actualLevel);
                levelManager.playerStats.InitStats(DataManager.Instance.GetSpeedValue(),
                                                    DataManager.Instance.GetRangeValue(),
                                                    DataManager.Instance.GetFuelValue());

                print("Speed(" + DataManager.Instance.statsLvl[0] + ") = " + DataManager.Instance.GetSpeedValue());
                print("Range(" + DataManager.Instance.statsLvl[1] + ") = " + DataManager.Instance.GetRangeValue());
                print("Fuel(" + DataManager.Instance.statsLvl[2] + ") = " + DataManager.Instance.GetFuelValue());
            }
        }
               

        cameraController.target = levelManager.player.transform;

        isPlaying = true;
        actualTrash = 0;
        uiManager.UpdateTrash(actualTrash, trashNeeded[actualLevel-1]);

        print("Loaded Level " + actualLevel + " and you need " + trashNeeded[actualLevel - 1] + " of trash");
    }

    public void EndGame(bool win)
    {
        isPlaying = false;
        uiManager.EndGame(win);

        if(win)
        {
            if(DataManager.Instance!=null)
            {
                DataManager.Instance.IncreaseLevel();
                DataManager.Instance.SaveData();
            }
        }
    }

    public void GetTrash()
    {
        actualTrash += 1;
        uiManager.UpdateTrash(actualTrash, trashNeeded[actualLevel-1]);
    }

    public void CheckEnd()
    {
        if (actualTrash >= trashNeeded[actualLevel - 1] && isPlaying)
        {
            EndGame(true);
        }
    }

    public void UpdateFuel(float actual, float max)
    {
        uiManager.UpdateFuel(actual, max);
    }

    public int GetExtraTrash()
    {
        return trashNeeded[actualLevel - 1] + (int)(trashNeeded[actualLevel-1] * percentExtraTrash);
    }
}
