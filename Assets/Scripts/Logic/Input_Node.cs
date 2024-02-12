using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Input_Node : MonoBehaviour
{
    bool recievingInput = false;
    float inputStrength = 0f;

    public bool RecievingInput => recievingInput;
    public float RecievedInputStrength => recievingInput ? inputStrength : 0;

    public void RecieveInput(float strength)
    {
        if (strength > 0)
        {
            recievingInput = true;
            inputStrength = strength;
        }
        else
        {
            recievingInput = false;
            inputStrength = 0f;
        }

        if (transform.parent.TryGetComponent(out ILogicUpdate logicUpdate))
        {
            logicUpdate.LogicUpdate();
        }
    }
}
