using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Output_Node : MonoBehaviour
{
    [SerializeField] Input_Node connectedNode;
    float strength;

    public void Emit(float strength)
    {
        if (connectedNode == null) return;
        this.strength = strength;
        connectedNode.RecieveInput(strength);
    }

    private void OnDrawGizmos()
    {
        if (!connectedNode) return;

        if(strength > 0)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, connectedNode.transform.position);
    }
}
