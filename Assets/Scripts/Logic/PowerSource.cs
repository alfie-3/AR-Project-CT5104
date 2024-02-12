using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : LogicObjectBase
{
    [SerializeField] Output_Node output;
    [Space]
    [SerializeField] bool active;
    [SerializeField, Range(0, 2)] float strength = 1;

    private void FixedUpdate()
    {
        output.Emit(active ? strength : 0);
    }
}
