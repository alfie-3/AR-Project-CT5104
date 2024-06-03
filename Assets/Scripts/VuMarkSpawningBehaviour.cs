using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuMarkSpawningBehaviour : MonoBehaviour
{
    [SerializeField] string compName;

    public event Action<string> OnFirstTimeFound = delegate { };
    bool triggeredFirstTimeFound = false;

    private void Awake()
    {
        GetComponent<DefaultObserverEventHandler>().OnTargetFound.AddListener(OnFound);
    }

    private void OnFound()
    {
        if (!triggeredFirstTimeFound)
        {
            OnFirstTimeFound.Invoke(compName);
            triggeredFirstTimeFound = true;
        }
    }
}
