using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [HideInInspector] public float currentMoney;
    public GameObject platform;
    private GameObject currentCar;
    public TextMeshPro currentMoneyText;

    private DataObj data;
    public Action Frames;

    [HideInInspector] public int currentCarId;

    private void Start()
    {
        instance = this;
        Frames += CheckCar;
        data = (DataObj)Resources.Load("DataObj");

        //if(MainData.carsAvailable.Length < data.carsObjects.Length)
        //{
        //    MainData.carsAvailable = new int[data.carsObjects.Length];
        //    Debug.Log("QQ");
        //}

        currentMoney = 5000f;
        Debug.Log(MainData.carsAvailable);
        Frames?.Invoke();
    }

    private void CheckCar()
    {
        for(int i = 0; i < data.carsPrice.Length; i++)
        {
            if(i != 0 && currentMoney < data.carsPrice[i])
            {
                if(currentCar != null)
                {
                    Destroy(currentCar);
                }
                currentCarId = i - 1;
                currentCar = Instantiate(data.carsObjects[currentCarId], transform.position, Quaternion.identity, platform.transform);
                currentCar.transform.localPosition = new Vector3(0, 0.85f, 0);
                currentCar.transform.localEulerAngles = new Vector3(0, 35, 0);
                currentCar.transform.localScale = new Vector3(0.25f, 2.5f, 0.25f);
                currentMoneyText.text = currentMoney.ToString() + "$";
                break;
            }
        }
    }

    private void OnDestroy()
    {
        Frames -= CheckCar;
    }
}
