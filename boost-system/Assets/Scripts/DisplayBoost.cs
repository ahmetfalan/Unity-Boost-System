using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBoost : MonoBehaviour
{
    public Boost boost;

    public SpriteRenderer sprite;
    public int value;
    public int duration;
    public bool buff;
    public bool debuff;
    void Start()
    {
        sprite.sprite = boost.boostImage;
        value = boost.value;
        duration = boost.duration;
        buff = boost.buff;
        debuff = boost.debuff;
    }
}
