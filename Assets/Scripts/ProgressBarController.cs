using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private Image actualPlanet;
    [SerializeField] private Image nextPlanet;
    [SerializeField] private Image fillImage;
    [SerializeField] private float[] progressValue;

    [SerializeField] private Sprite[] planets;


    private int lvl;

    private bool fromScene = false;
    private void Awake() {
        
        int lvl = DataManager.Instance.actualLevel;

        if(DataManager.Instance.gameFinished)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if(fromScene)
            {
                LoadInfo(lvl-1);
            }
            else
            {
                LoadInfo(lvl);
            }
        }
    }

    private void Update() 
    {
        if(fromScene)
            nextLevel();
    }

    private void LoadInfo(int level)
    {
        int planProgress = GetPlanetProgress(level);

        fillImage.fillAmount = progressValue[planProgress-1];

        int plan = GetCurrentPlanet(level);

        actualPlanet.sprite = planets[plan-1];
        nextPlanet.sprite = planets[plan];        
    }

    private void nextLevel()
    {
         fillImage.fillAmount = Mathf.Lerp(GetCurrentPlanet(lvl-1), GetCurrentPlanet(lvl), Time.time);
    }

    private int GetPlanetProgress(int level)
    {
        int progress = (int)level%5;

        if(progress == 0) progress = 5;
        return progress;
    }

    private int GetCurrentPlanet(int level)
    {
        return (int) Mathf.Ceil(((float)level)/5);
    }


}
