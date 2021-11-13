using UnityEngine;
using System.Collections;

public class StatBuff : TimedEffect
{
    public PlayerAttributes stat;
    public int buffValue;

    protected override void ApplyEffect()
    {
        //target.BuffStat(stat, buffValue);
    }

    protected override void EndEffect()
    {
        //target.BuffStat(stat, -buffValue);
        base.EndEffect();
    }
}