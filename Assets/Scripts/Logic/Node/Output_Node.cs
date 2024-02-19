using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Output_Node : NodeBase
{
    float strength;

    public void Emit(float strength)
    {
        if (connectedNode == null) return;

        this.strength = strength;
        Input_Node inputNode = connectedNode as Input_Node;
        inputNode.RecieveInput(strength);
    }

    public void Update()
    {
        UpdateLineRenderer();
    }

    public void UpdateLineRenderer()
    {
        if (connectedNode == null) return;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, connectedNode.transform.position);
    }

    public override void LinkNode(NodeBase otherNode)
    {
        base.LinkNode(otherNode);
        lineRenderer.enabled = true;
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
