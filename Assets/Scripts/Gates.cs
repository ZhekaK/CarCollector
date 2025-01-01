using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class Gates : MonoBehaviour
{
    public float value;
    public bool x;
    public int id;

    private TextMeshPro tmp;

    private void Start()
    {
        tmp = GetComponentInChildren<TextMeshPro>();
        if (!x)
        {
            if (value < 0)
            {
                tmp.text = value.ToString() + "$";
            }
            else
            {
                tmp.text = "+" + value.ToString() + "$";
            }
        }
        else
        {
            tmp.text = "x" + value.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Frames Detect");
            if (id < 6)
            {
                GenerateTrack.instance.GatesObjects[id + GenerateTrack.instance.countObjects].GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                GenerateTrack.instance.GatesObjects[id - GenerateTrack.instance.countObjects].GetComponent<BoxCollider>().enabled = false;
            }
            gameObject.GetComponent<BoxCollider>().enabled = false;

            if(x)
            {
                PlayerController.instance.currentMoney *= value;
                if (PlayerController.instance.currentMoney < 0)
                    PlayerController.instance.currentMoney = 0;
            }
            else
            {
                PlayerController.instance.currentMoney += value;
                if (PlayerController.instance.currentMoney < 0)
                    PlayerController.instance.currentMoney = 0;
            }
            PlayerController.instance.Frames?.Invoke();
        }
    }
}
