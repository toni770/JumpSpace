using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int[] trashNeeded;
    [SerializeField] [Range(0, 1)] private float percentExtraTrash = 0.2f;
    [SerializeField] private CameraController cameraController;

    [SerializeField] bl_Joystick joystick;

    [Header("DEBUG")]
    [SerializeField] private bool debugMode = false;
    [SerializeField] private int actualLevel = 1;

    public bool isPlaying { get; private set; } = false;

    private int actualTrash = 0;

    private UIManager uiManager;
    private LevelManager levelManager;
    

    protected override void Awake()
    {
        base.Awake();
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
                levelManager.playerStats.InitStats(DataManager.Instance.GetStatValue(((int)GlobalVars.Stats.speed)),
                                                    DataManager.Instance.GetStatValue(((int)GlobalVars.Stats.range)),
                                                    DataManager.Instance.GetStatValue(((int)GlobalVars.Stats.fuel)));

                levelManager.player.GetComponent<PlayerInput>().SetJoystick(joystick);

                print("Speed(" + DataManager.Instance.statsLvl[0] + ") = " + DataManager.Instance.GetStatValue(((int)GlobalVars.Stats.speed)));
                print("Range(" + DataManager.Instance.statsLvl[1] + ") = " + DataManager.Instance.GetStatValue(((int)GlobalVars.Stats.range)));
                print("Fuel(" + DataManager.Instance.statsLvl[2] + ") = " + DataManager.Instance.GetStatValue(((int)GlobalVars.Stats.fuel)));
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
