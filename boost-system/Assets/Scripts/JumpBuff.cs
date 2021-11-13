using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBuff : IBuffable
{
    public float Value { get; set; }
    public bool IsActive { get; set; }
    public float CurrentCooldown { get; set; }
    public float MaxCoolDown { get; set; }
    public GameObject ImageInPanel { get; set; }
    public PlayerAttributes Player { get; set; }
    public JumpBuff(PlayerAttributes Player, bool IsActive, float Value, float MaxCoolDown, GameObject ImageInPanel)
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
            Player.Jump += Value;
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
        Player.Jump -= Value;
        IsActive = false;
    }

}
