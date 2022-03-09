using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JuiceManager : Singleton<JuiceManager>
{
    [SerializeField] Transform _player;
    [SerializeField] Transform _camera;

    public void DownUpConstantMovement(Transform target, float distance = 10)
    {
        target.DOMove(target.position + new Vector3(0,distance,0),0.7f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
    }   
    public void ConstantSizable(Transform target, float strength = 1.1f, float duration = 0.5f)
    {
        target.DOScale(new Vector3(strength ,strength,strength),duration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
    }   

    public void ShakeScale(Transform target, float duration, float strength)
    {
        target.DORestart();
        target.DOShakeScale(duration, strength);
    }

    public void ShakePos(Transform target, float duration, float strength)
    {
        target.DORestart();
        target.DOShakePosition(duration, strength);
    }

    public void StopAnimation(Transform target, bool pause)
    {
        if(pause)
        {
            target.DORestart();
            target.DOPause();
        }
        else
        {
            target.DOPlay();
        }
    }

    public void PlayerImprovement()
    {
        _player.DORestart();
        _player.DOMove(_player.position + new Vector3(0,2,0), 0.25f).SetEase(Ease.InOutSine).SetLoops(2,LoopType.Yoyo);
        //_player.DORotate(new Vector3(0,360,0),  0.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
    }

    public void PlayerDamaged(float strength = 0.2f)
    {
        _camera.DORestart();
        ShakePos(_camera,1f, strength);
    }

    public void PlayerJumpToPosition(Vector3 pos)
    {
        _player.DOJump(pos, 10, 1, 1);
    }

}
