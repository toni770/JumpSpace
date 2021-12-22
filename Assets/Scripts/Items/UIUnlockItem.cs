using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUnlockItem : UIItem
{
    public override void Unlock()
    {
        WatchVideo();
    }

    private void WatchVideo()
    {
        AdsManager.Instance.PlayRewardedAd(Reward);
    }

    private void Reward()
    {
        itemController.Unlock(this);
    }
}
