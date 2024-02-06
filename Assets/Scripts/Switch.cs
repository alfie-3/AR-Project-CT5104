using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : LogicObjectBase
{
    bool isActive;

    [SerializeField] Input_Node input;
    [SerializeField] Output_Node output;

    public void ProcessInputs()
    {
        if (isActive && input.RecievingInput)
        {
            output.Emit(input.RecievedInputStrength);
        }
        else
        {
            output.Emit(0);
        }
    }
}
