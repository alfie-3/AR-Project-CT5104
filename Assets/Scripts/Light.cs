using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : LogicObjectBase
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

    private void FixedUpdate()
    {
        ProcessInput();
    }

    public void ProcessInput()
    {
        if (input == null) return;

        propertyBlock.SetFloat("_EmissionMult", input.RecievedInputStrength);
        rend.SetPropertyBlock(propertyBlock);

        if (output != null)
        {
            output.Emit(input.RecievedInputStrength);
        }
    }
}
