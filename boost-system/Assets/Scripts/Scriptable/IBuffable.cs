using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IBuffable
{
    float Value { get; set; }
    float CurrentCooldown { get; set; }
    float MaxCoolDown { get; set; }
    bool IsActive { get; set; }
    GameObject ImageInPanel { get; set; }
    void Active();
    void DeActive();
    PlayerAttributes Player { get; set; }
}