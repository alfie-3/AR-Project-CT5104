using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vuforia;

[RequireComponent(typeof(VuMarkBehaviour))]
public class VuforiaObjectDetection : MonoBehaviour
{
    [SerializeField] VuMarkBehaviour vuMarkBehaviour;

    private void OnEnable()
    {
        //vuMarkBehaviour.
    }

    private void Start()
    {
        
    }
}
