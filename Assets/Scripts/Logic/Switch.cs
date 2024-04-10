using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Switch : LogicObjectBase, IPointerClickHandler, ILogicUpdate
{
    [SerializeField] bool isActive;

    [SerializeField] Input_Node input;
    [SerializeField] Output_Node output;

    [SerializeField] Animator switchAnimator;

    public static AudioClip buttonClip;
    private void Start()
    {
        if (buttonClip == null)
         buttonClip = (AudioClip)Resources.Load("Audio/Button 1", typeof(AudioClip));
    }

    public void LogicUpdate()
    {
        output.Emit(ProcessInputs());
    }

    public float ProcessInputs()
    {
        if (isActive && input.RecievingInput)
        {
            return (input.RecievedInputStrength);
        }
        else
        {
            return 0;
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        isActive = !isActive;

        if (switchAnimator != null) { switchAnimator.Play(isActive ? "SwitchToggleOn" : "SwitchToggleOff"); }
        AudioManager.PlayClip(buttonClip, pitch: isActive ? 1.05f : 0.95f);
    }
}
