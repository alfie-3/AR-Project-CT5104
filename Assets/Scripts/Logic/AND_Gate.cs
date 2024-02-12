using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AND_Gate : LogicGate
{
    public override float ProcessInputs()
    {
        if (input_a == null || input_b == null) return 0;

        if (input_a.RecievingInput &&  input_b.RecievingInput)
            return input_a.RecievedInputStrength + input_b.RecievedInputStrength / 2;

        return 0;
    }
}
