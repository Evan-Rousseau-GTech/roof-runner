using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager
{
    public static float getHighscore(int seed)
    {
        float highscore = PlayerPrefs.GetFloat(seed.ToString(), 0);
        return highscore;
    }
    public static void setHighscore(int seed, float highscore)
    {
        PlayerPrefs.SetFloat(seed.ToString(), highscore);
    }
}
