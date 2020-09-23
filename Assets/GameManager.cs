﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Token { get; set; }
    public int LumberjackLevel { get; set; }
    public int StonequarryLevel { get; set; }
    public int GoldmineLevel { get; set; }
    public int BarracksLevel { get; set; }
    public int WallLevel { get; set; }
    public int WatchtowerLevel { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}