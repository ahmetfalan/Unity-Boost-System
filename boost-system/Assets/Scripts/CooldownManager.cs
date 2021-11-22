using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownManager : MonoBehaviour
{
    public static CooldownManager Instance;
    public List<IBuffable> Buffables = new List<IBuffable>();
    public GameObject activeBuffs;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
    public void StartCooldown(IBuffable buffable)
    {
        buffable.CurrentCooldown = buffable.MaxCoolDown;
        Buffables.Add(buffable);
        Instantiate(buffable.ImageInPanel, activeBuffs.transform);
    }

    void Update()
    {
        if (Buffables.Count > 0)
        {
            for (int i = 0; i < Buffables.Count; i++)
            {
                Buffables[i].CurrentCooldown -= Time.deltaTime;
                Buffables[i].ImageInPanel.SetActive(true);
                Buffables[i].ImageInPanel.GetComponentInChildren<Text>().text = Convert.ToInt32(Buffables[i].CurrentCooldown).ToString();
                if (Buffables[i].CurrentCooldown <= 0)
                {
                    Buffables[i].ImageInPanel.SetActive(false);
                    Buffables[i].CurrentCooldown = 0;
                    Buffables[i].DeActive();
                    Buffables.Remove(Buffables[i]);
                }
            }
        }
    }
}
