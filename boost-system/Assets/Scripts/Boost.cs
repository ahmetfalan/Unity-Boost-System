using UnityEngine;

[CreateAssetMenu(fileName ="New Boost", menuName ="Boost")]
public class Boost: ScriptableObject
{
    public new string boostName = "New Boost";
    public Sprite boostImage;
    public int duration;
    public int value;
    public bool buff;
    public bool debuff;
}