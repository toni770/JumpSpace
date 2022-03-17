using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int[] trashNeeded;
    [SerializeField] [Range(0, 1)] private float percentExtraTrash = 0.2f;

    [SerializeField] private int trashValue = 5;
    [SerializeField] private CameraController cameraController;

    [SerializeField] bl_Joystick joystick;

    [Header("DEBUG")]
    [SerializeField] private bool debugMode = false;
    [SerializeField] private int actualLevel = 1;

    [SerializeField] private GameObject player;

    [Header("End Menu")]
    [SerializeField] private int multiplyReward = 3;

    [SerializeField] private float nextbuttonShowTime =  2; 

    [SerializeField] private float deathTime = 2;

    [SerializeField] private ArrowIndicator arrow;

    [SerializeField] private AudioConfig[] _planetMusics;

    [SerializeField] private AudioConfig _unlockSound;

    public bool isPlaying { get; private set; } = false;

    private int actualTrash = 0;

    private UIManager uiManager;
    private LevelManager levelManager;
    
    private GameObject shipZone;
    public bool revived {get; private set;}

    private AudioSource _audioSource;


    protected override void Awake()
    {
        base.Awake();
        uiManager = GetComponent<UIManager>();
        levelManager = GetComponent<LevelManager>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        InitGame();
        AdsManager.Instance.PlayAd();
    }

    private void InitGame()
    {
         if (!debugMode)
        {
            if (DataManager.Instance != null)
            {
                actualLevel = DataManager.Instance.actualLevel;

                levelManager.LoadLevel(actualLevel);

                player.GetComponent<PlayerInput>().SetJoystick(joystick);
            }
        }
    }

    public void StartGame()
    {              
        player.GetComponent<PlayerStats>().InitStats(DataManager.Instance.GetStatValue(((int)GlobalVars.Stats.speed)),
                                    DataManager.Instance.GetStatValue(((int)GlobalVars.Stats.range)),
                                    DataManager.Instance.GetStatValue(((int)GlobalVars.Stats.fuel)));

        cameraController.target = player.transform;

        isPlaying = true;
        revived = false;
        actualTrash = 0;
        uiManager.UpdateTrash(actualTrash, trashNeeded[levelManager.Level]);

        int actualPlanet = (int) Mathf.Ceil(((float)levelManager.Level)/5);
        if(levelManager.Level %5 == 0) actualPlanet += 1;
        
        print(actualPlanet.ToString());

        _audioSource.clip = _planetMusics[actualPlanet-1].Clip;
        _audioSource.volume = _planetMusics[actualPlanet-1].Volume;
        _audioSource.pitch = _planetMusics[actualPlanet-1].Pitch;
        _audioSource.Play();
        print("Loaded Level " + actualLevel + " and you need " + trashNeeded[levelManager.Level] + " of trash");
    }

    public void EndGame(bool win)
    {
        _audioSource.Pause();
        isPlaying = false;
        if(!win) player.GetComponent<PlayerStats>().Destroy(true);

        StartCoroutine(FinishGame(win));        
    }

    public void SetShip(GameObject zone)
    {
        shipZone = zone;
        arrow.AssignTarget(shipZone.transform);
    }

    IEnumerator FinishGame(bool win)
    {
        yield return new WaitForSeconds(deathTime);
        uiManager.EndGame(win);

        if(win)
        {
            uiManager.UpdateEndTrash(actualTrash,trashNeeded[actualLevel-1]);
            uiManager.UpdateEndMoney(actualTrash * trashValue);
            if(DataManager.Instance!=null)
            {
                DataManager.Instance.IncreaseCoins(actualTrash * trashValue);
                DataManager.Instance.IncreaseLevel();
                DataManager.Instance.SaveData();
            }
            StartCoroutine(showNext());
        }
    }

    IEnumerator showNext()
    {
        yield return new WaitForSeconds(nextbuttonShowTime);
        uiManager.ShowNextBtn();
    }
    public void Revive()
    {
        _audioSource.Play();
        AdsManager.Instance.PlayRewardedAd(ResetGame);
    }

    public void Multiply()
    {
        AdsManager.Instance.PlayRewardedAd(MultiplyMoney);
    }

    private void MultiplyMoney()
    {
        SoundManager.Instance.MakeSound(_unlockSound);
        uiManager.HideMultiply();
        uiManager.UpdateEndMoney(actualTrash * trashValue * multiplyReward);
        if(DataManager.Instance!=null)
            {
                DataManager.Instance.IncreaseCoins(actualTrash * trashValue * multiplyReward);
                DataManager.Instance.IncreaseLevel();
                DataManager.Instance.SaveData();
            }
    }

    private void ResetGame()
    {
        isPlaying = true;
        uiManager.Revive();
        revived = true;

        player.GetComponent<PlayerFuel>().RefillFuel();
        player.GetComponent<PlayerStats>().Destroy(false);

    }

    public void GetTrash()
    {
        actualTrash += 1;
        uiManager.UpdateTrash(actualTrash, trashNeeded[levelManager.Level]);

        shipZone.SetActive(EnoughTrash());
        arrow.gameObject.SetActive(EnoughTrash());
    }

    public void CheckEnd()
    {
        EndGame(true);
        /*if (EnoughTrash())
        {
            EndGame(true);
        }*/
    }

    private bool EnoughTrash()
    {
        return (actualTrash >= trashNeeded[levelManager.Level] && isPlaying);
    }

    public void UpdateFuel(float actual, float max)
    {
        uiManager.UpdateFuel(actual, max);
    }

    public int GetExtraTrash(int level)
    {
        return trashNeeded[level] + (int)(trashNeeded[level] * percentExtraTrash);
    }

    public void FuelDamaged()
    {
        uiManager.FuelDamaged();
    }

    public void FuelHealed()
    {
        uiManager.FuelHealed();
    }

    public void ReserveMode(bool reserve)
    {
        uiManager.ReserveMode(reserve);
    }


}
