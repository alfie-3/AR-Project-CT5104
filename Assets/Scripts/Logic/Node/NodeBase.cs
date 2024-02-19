using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
public abstract class NodeBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] protected NodeBase connectedNode;
    protected LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public virtual void LinkNode(NodeBase otherNode)
    {
        connectedNode = otherNode;
    }

    public void UnlinkNode()
    {
        if (connectedNode == null) { return; }
        connectedNode.RemoveNode();
        RemoveNode();
    }

    public virtual void RemoveNode()
    {
        lineRenderer.enabled = false;
        connectedNode = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        UnlinkNode();
        lineRenderer.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateDragLineRenderer(eventData);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);

        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 20);

        if (Physics.Raycast(ray.origin, ray.direction * 20, out hit, 20, LayerMask.GetMask("Node")))
        {
            if (hit.transform.gameObject == this.gameObject) { return; }

            if (hit.transform.TryGetComponent(out NodeBase node))
            {
                LinkNode(node);
                node.LinkNode(this);

                Debug.Log($"Link node {this.gameObject.name} to {node.gameObject.name}");
            }
        }
        else
        {
            UnlinkNode();

            lineRenderer.enabled=false;
        }
    }

    public void UpdateDragLineRenderer(PointerEventData eventData)
    {
        lineRenderer.SetPosition(0, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(eventData.position);

        Plane plane = new(-Camera.main.transform.forward, transform.position);
        float enter = 0.0f;

        if (plane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            lineRenderer.SetPosition(1, hitPoint);
        }
    }
}
