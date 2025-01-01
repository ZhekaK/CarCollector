using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataObj : ScriptableObject
{
    public GameObject[] carsObjects;
    public float[] carsPrice;
    public Material[] carsColors;
    [Space]
    public float[] parkingRegionsPrice;
    public float[] mainHousePrice;
    [Space]
    public GameObject[] trackBonusObj;
    public float[] trackBonusValueMinMax;
    public float[] trackBonusValueFloatMinMax;
}
