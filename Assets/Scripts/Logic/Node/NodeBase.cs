using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
public abstract class NodeBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] protected NodeBase connectedNode;
    protected LineRenderer lineRenderer;

    static AudioClip grabNoise;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (grabNoise == null) grabNoise = (AudioClip)Resources.Load("Audio/Grab", typeof(AudioClip));

        if (connectedNode != null)
         RemoveNode();
    }

    private void OnEnable()
    {
        if (connectedNode == null)
        {
            lineRenderer.enabled = false;
        }
    }

    public virtual void LinkNode(NodeBase otherNode)
    {
        UnlinkNode();
        connectedNode = otherNode;
    }

    public virtual void UnlinkNode()
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
        AudioManager.PlayClipWithSemitonePitch(grabNoise, 0.5f);
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
            if (hit.transform.gameObject == gameObject || hit.transform.parent == transform.parent) { UnlinkNode(); lineRenderer.enabled = false; return; }

            if (hit.transform.TryGetComponent(out NodeBase node))
            {
                if (node.GetType() == GetType()) { UnlinkNode(); lineRenderer.enabled = false; return; }

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

    public IEnumerator FlashWireEffect(float duration = 0.5f)
    {
        MaterialPropertyBlock propertyBlock = new();

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            propertyBlock.SetFloat("_Stripe_Blend", 1 * Mathf.PingPong(elapsedTime, duration / 2));

            elapsedTime += Time.deltaTime;

            lineRenderer.SetPropertyBlock(propertyBlock);

            yield return null;
        }

        propertyBlock.SetFloat("_Stripe_Blend", 0);
        lineRenderer.SetPropertyBlock(propertyBlock);
    }

    public void CancelEffects()
    {
        StopAllCoroutines();

        MaterialPropertyBlock propertyBlock = new();
        propertyBlock.SetFloat("_Stripe_Blend", 0);
        lineRenderer.SetPropertyBlock(propertyBlock);
    }
}
