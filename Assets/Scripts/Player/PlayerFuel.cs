using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuel : MonoBehaviour
{


    [SerializeField]
    private float fuelReserveTime = 5;
    [SerializeField]
    private float fuelConsumitionSpeed = 2;


    public bool isReserve { get; private set; } = false;

    private float currentFuel = 0;
    private float reserveCount = 0;

    private PlayerStats playerStats;

    private bool _alive = true;

    [SerializeField] private AudioConfig _explosionSound;
    [SerializeField] private AudioConfig _reserveSound;
    private AudioSource _audioSource;

    [SerializeField] private float _smokeTime;

    private float _smokeCount;

    [SerializeField] private GameObject explosionPrefab;
    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        _audioSource =GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if(GameManager.Instance.isPlaying)
        {
            if(!playerStats.IsGodMode())
            {
                if (!isReserve)
                    GetDamage(Time.deltaTime * fuelConsumitionSpeed);
                else
                {
                    _smokeCount += Time.deltaTime;

                    if(_smokeCount >= _smokeTime)
                    {
                        SoundManager.Instance.MakeSound(_reserveSound);
                        _smokeCount = 0;
                    }
                    reserveCount += Time.deltaTime;

                    if(reserveCount >= fuelReserveTime) Death();
                }
            }
        }
    }

    private void StartGame()
    {
        RefillFuel();
    }

    public void GetDamage(float damage, bool externalDamage = false)
    {
         if(!playerStats.IsGodMode() && _alive)
        {
            if (isReserve) 
                Death();
            else
            {
                if(externalDamage) 
                {
                    JuiceManager.Instance.PlayerDamaged();
                    GameManager.Instance.FuelDamaged();
                }
                currentFuel -= damage;
                if (currentFuel <= 0)
                {
                    currentFuel = 0;
                    GetReserve(true);
                }
                GameManager.Instance.UpdateFuel(currentFuel, playerStats.maxFuel);
            }
        }
    }

    public void ResetSound()
    {
         _smokeCount = 0;
        SoundManager.Instance.MakeSound(_reserveSound);
    }
    public void RefillFuel()
    {   
        if(!_audioSource.isPlaying)_audioSource.Play();

        GameManager.Instance.FuelHealed();
        if(!_alive) _alive = true;
        currentFuel = playerStats.maxFuel;
        GameManager.Instance.UpdateFuel(currentFuel, playerStats.maxFuel);
        if (isReserve)
            GetReserve(false);
    }

    private void GetReserve(bool reserve)
    {
        if(!reserve) _audioSource.Play();
        else _audioSource.Stop();

        GameManager.Instance.ReserveMode(reserve);
        isReserve = reserve;
        playerStats.EnterReserve(reserve);
        reserveCount = 0;
        ResetSound();
    }

    private void Death()
    {
        _audioSource.Stop();
        SoundManager.Instance.MakeSound(_explosionSound);
        ParticlesManager.Instance.SpawnParticle(explosionPrefab, transform.position, transform.rotation);

        GameManager.Instance.ReserveMode(false);
        _alive = false;
        JuiceManager.Instance.PlayerDamaged(1);
        GameManager.Instance.EndGame(false);
    }
}
