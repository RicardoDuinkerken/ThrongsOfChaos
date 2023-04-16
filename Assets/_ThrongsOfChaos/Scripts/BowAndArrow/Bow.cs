using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : XRGrabInteractable
{
    // Empty
    [SerializeField] private MeshCollider meshCollider;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        meshCollider.isTrigger = true;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        meshCollider.isTrigger = false;
    }
}
