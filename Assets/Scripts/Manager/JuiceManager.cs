using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JuiceManager : Singleton<JuiceManager>
{
    public void DownUpConstantMovement(Transform target)
    {
        target.DOMove(target.position + new Vector3(0,10,0),0.7f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
    }   
    public void ConstantSizable(Transform target, float delay)
    {
        target.DOScale(new Vector3(1.1f ,1.1f,1.1f),0.5f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
    }   

    public void ShakeScale(Transform target, float duration, float strength)
    {
        target.DOShakeScale(duration, strength);
    }

    public void ShakePos(Transform target, float duration, float strength)
    {
        target.DOShakePosition(duration, strength);
    }
}
