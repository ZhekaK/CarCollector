using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class GenerateTrack : MonoBehaviour
{
    public static GenerateTrack instance;
    public Transform startSpawnPoint, finishSpawnPoint;
    [HideInInspector] public float range;
    public float distanceBetweenObjects;
    [HideInInspector] public int countObjects;
    [HideInInspector] public List<GameObject> GatesObjects;
    private DataObj data;

    private void Start()
    {
        instance = this;
        data = (DataObj)Resources.Load("DataObj");
        Generate();
    }

    [ContextMenu("Generate")]
    private void Generate()
    {
        range = Vector3.Distance(startSpawnPoint.position, finishSpawnPoint.position);
        countObjects = (int)(range / distanceBetweenObjects) + 1;

        Clear();

        SpawnObjects(0, 0);
        SpawnObjects(6.25f, countObjects);

        //Debug.Log("TEST: " + Math.Floor(1272.9274f) + " | " + Math.Round(0.871258813f, 2));
    }

    private void SpawnObjects(float offset, int startIndex)
    {
        for (int i = 0; i < countObjects; i++)
        {
            bool x = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));
            float value = 0;
            if (!x)
            {
                value = UnityEngine.Random.Range(data.trackBonusValueMinMax[0], data.trackBonusValueMinMax[1]);
                value = (float)Math.Floor(value);
            }
            else
            {
                value = UnityEngine.Random.Range(data.trackBonusValueFloatMinMax[0], data.trackBonusValueFloatMinMax[1]);
                value = (float)Math.Round(value, 2);
            }

            if (value < 1)
            {
                GatesObjects.Add(Instantiate(data.trackBonusObj[0], new Vector3(offset, 0, i * distanceBetweenObjects + startSpawnPoint.position.z), Quaternion.identity, gameObject.transform));
                GatesObjects[startIndex + i].GetComponent<Gates>().x = x;
                GatesObjects[startIndex + i].GetComponent<Gates>().value = value;
                GatesObjects[startIndex + i].GetComponent<Gates>().id = startIndex + i;
            }
            else
            {
                GatesObjects.Add(Instantiate(data.trackBonusObj[1], new Vector3(offset, 0, i * distanceBetweenObjects + startSpawnPoint.position.z), Quaternion.identity, gameObject.transform));
                GatesObjects[startIndex + i].GetComponent<Gates>().x = x;
                GatesObjects[startIndex + i].GetComponent<Gates>().value = value;
                GatesObjects[startIndex + i].GetComponent<Gates>().id = startIndex + i;
            }
        }
    }

    [ContextMenu("Clear")]
    private void Clear()
    {
        if (GatesObjects != null)
        {
            for (int i = 0; i < GatesObjects.Count; i++)
            {
                DestroyImmediate(GatesObjects[i]);
            }
            GatesObjects.Clear();
        }
    }
}
