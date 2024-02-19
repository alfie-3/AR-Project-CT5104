using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuMarksManager : MonoBehaviour
{
    [SerializeField] VuMarkComp[] vuMarkComps = null;
    Dictionary<string, Stack<GameObject>> instancedVumarkListDict = new();

    Dictionary<string, VuMarkComp> vumarkDict = new();

    // Start is called before the first frame update
    void Start()
    {
        GenerateMarkers();
        VuforiaConfiguration.Instance.Vuforia.MaxSimultaneousImageTargets = vuMarkComps.Length;
    }

    void GenerateMarkers()
    {
        for (int i = 0; i < vuMarkComps.Length; i++)
        {
            GameObject newComptObj = Instantiate(vuMarkComps[i].prefab, transform);

            instancedVumarkListDict.Add(vuMarkComps[i].name, new Stack<GameObject>());
            instancedVumarkListDict[vuMarkComps[i].name].Push(newComptObj);

            vumarkDict.Add(vuMarkComps[i].name, vuMarkComps[i]);
        }
    }

    public void AddNewMarker(string markerID)
    {
        instancedVumarkListDict[markerID].Push(Instantiate(vumarkDict[markerID].prefab, transform));
        VuforiaConfiguration.Instance.Vuforia.MaxSimultaneousImageTargets++;
    }

    public void RemoveMarker(string markerID)
    {
        if (instancedVumarkListDict[markerID].Count > 1)
            instancedVumarkListDict[markerID].Pop();

        VuforiaConfiguration.Instance.Vuforia.MaxSimultaneousImageTargets--;
    }

    [System.Serializable]
    public struct VuMarkComp
    {
        public string name;
        public GameObject prefab;
    }
}
