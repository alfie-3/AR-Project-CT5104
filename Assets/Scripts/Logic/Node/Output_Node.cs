using UnityEngine;

public class Output_Node : NodeBase
{
    float strength;

    bool firstLinkedEmit = true;

    static AudioClip connectAudio;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (connectAudio == null)
            connectAudio = (AudioClip)Resources.Load("Audio/Electric Sound 2", typeof(AudioClip));
    }

    public void Emit(float strength)
    {
        if (connectedNode == null) return;

        this.strength = strength;
        Input_Node inputNode = connectedNode as Input_Node;
        inputNode.RecieveInput(strength);

        CheckPlayEffect();
    }

    public void CheckPlayEffect()
    {
        if (firstLinkedEmit && strength > 0)
        {
            StartCoroutine(FlashWireEffect());
            firstLinkedEmit = false;
        }

        firstLinkedEmit = strength <= 0;
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
        AudioManager.PlayClip(connectAudio);
    }

    public override void UnlinkNode()
    {
        Emit(0);
        firstLinkedEmit = true;
        base.UnlinkNode();
    }

    private void OnDrawGizmos()
    {
        if (!connectedNode) return;

        if (strength > 0)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, connectedNode.transform.position);
    }
}
