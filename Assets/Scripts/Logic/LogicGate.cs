using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LogicGate : LogicObjectBase, ILogicUpdate
{
    [SerializeField] protected Output_Node output;
    [Space]
    [SerializeField] protected Input_Node input_a;
    [SerializeField] protected Input_Node input_b;

    public void LogicUpdate()
    {
        output.Emit(ProcessInputs());
    }

    public virtual float ProcessInputs() { return 0; }
}
