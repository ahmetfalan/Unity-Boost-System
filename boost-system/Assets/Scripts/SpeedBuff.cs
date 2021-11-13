using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBuff :IBuffable
{
    public float Value { get; set; }
    public bool IsActive { get; set; }
    public float CurrentCooldown { get; set ; }
    public float MaxCoolDown { get; set; }
    public GameObject ImageInPanel { get; set; }
    public PlayerAttributes Player { get; set; }
    public SpeedBuff(PlayerAttributes Player, bool IsActive, float Value, float MaxCoolDown, GameObject ImageInPanel)
    {
        this.Player = Player;
        this.Value = Value;
        this.MaxCoolDown = MaxCoolDown;
        this.ImageInPanel = ImageInPanel;
        this.IsActive = IsActive;
    }
    public void Active()
    {
        if (!IsActive)
        {
            Player.Speed += Value;
            IsActive = true;
            CooldownManager.Instance.StartCooldown(this);
        }
        else
        {
            CurrentCooldown += MaxCoolDown;
        }
    }
    public void DeActive()
    {
        Player.Speed -= Value;
        IsActive = false;
    }
}
