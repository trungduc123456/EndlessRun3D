using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    private const string HIGH_SCORE = "highScore";
    private const string COUNT_ITEM_FLY = "countItemFly";
    private const string COUNT_ITEM_MAGNET = "countItemMagnet";
    private const string COIN = "coin";
    public static int COIN_SCORE_AMOUNT = 5;
    public static float DEAD_ZONE = 100.0f;
    public static float DISTANCE_TO_RESPAWN = 5f;
    public static float LANE_DISTANCE = 2f;
    public static float DISTANCE_BEFORE_SPAWN = 100.0f;
    public static int INITIAL_SEGMENTS = 10;
    public static int INITIAL_TRANSITON_SEGMENTS = 2;
    public static int MAX_SEGMENTS_ON_SCREEN = 15;
    public static float TIME_WAIT_SKILL = 20f;
    public static float HighScore
    {
        get
        {
            return PlayerPrefs.GetFloat(HIGH_SCORE, 0);
        }
        set
        {
            PlayerPrefs.SetFloat(HIGH_SCORE, value);
        }

    }
    public static int CountItemFly
    {
        get
        {
            return PlayerPrefs.GetInt(COUNT_ITEM_FLY, 0);
        }
        set
        {
            PlayerPrefs.SetInt(COUNT_ITEM_FLY, value);
        }
    }
    public static int CountItemMagnet
    {
        get
        {
            return PlayerPrefs.GetInt(COUNT_ITEM_MAGNET, 0);
        }
        set
        {
            PlayerPrefs.SetInt(COUNT_ITEM_MAGNET, value);
        }
    }
    public static int Coin
    {
        get
        {
            return PlayerPrefs.GetInt(COIN, 100);
        }
        set
        {
            PlayerPrefs.SetInt(COIN, value);
        }
    }
	
}
