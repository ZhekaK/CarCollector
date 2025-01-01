using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Saver
{
    private static DataObj dataObj;
    public static void Init()
    {
        dataObj = (DataObj)Resources.Load("DataObj");
        if (MainData.carsAvailable.Length < dataObj.carsObjects.Length)
        {
            MainData.carsAvailable = new int[dataObj.carsObjects.Length];
            Debug.Log("QQ");
        }
        if(MainData.parkingRegions.Length < dataObj.parkingRegionsPrice.Length)
        {
            MainData.parkingRegions = new int[dataObj.parkingRegionsPrice.Length];
            Debug.Log("QQ2");
        }
    }

    public static void Save()
    {
        MainData.money = (float)Math.Round(MainData.money, 1);
        PlayerPrefs.SetFloat("money", MainData.money);

        PlayerPrefs.SetInt("lvl", MainData.lvl);

        PlayerPrefs.SetFloat("exp", MainData.exp);

        PlayerPrefs.SetInt("mainHouseLvl", MainData.mainHouseLvl);
    }

    public static void load()
    {
        MainData.money = PlayerPrefs.GetFloat("money");
        MainData.lvl = PlayerPrefs.GetInt("lvl");
        MainData.exp = PlayerPrefs.GetFloat("exp");
        MainData.mainHouseLvl = PlayerPrefs.GetInt("mainHouseLvl");

        for (int i = 0; i < MainData.carsAvailable.Length; i++)
        {
            MainData.carsAvailable[i] = PlayerPrefs.GetInt("carsAvailable" + i.ToString());
        }
        for (int i = 0; i < MainData.parkingRegions.Length; i++)
        {
            MainData.parkingRegions[i] = PlayerPrefs.GetInt("parkingRegions" + i.ToString());
        }
    }
}
