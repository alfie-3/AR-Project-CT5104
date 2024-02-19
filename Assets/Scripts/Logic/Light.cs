using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : LogicObjectBase, ILogicUpdate
{
    [SerializeField] Input_Node input;
    [SerializeField] Output_Node output;
    [Space]
    [SerializeField] GameObject light_element;
    MaterialPropertyBlock propertyBlock;
    Renderer rend;

    private void Awake()
    {
        rend = light_element.GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
    }

    public void ProcessInput()
    {
        propertyBlock.SetFloat("_EmissionMult", input.RecievedInputStrength);
        rend.SetPropertyBlock(propertyBlock);

        if (output != null)
        {
            output.Emit(input.RecievedInputStrength);
        }
    }

    public void LogicUpdate()
    {
        ProcessInput();
        output.Emit(input.RecievedInputStrength);
    }
}
