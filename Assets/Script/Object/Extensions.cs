using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static bool IsInteractable(this RaycastHit2D hit)
    {
        return hit.transform.GetComponent<TInteractable>();
    }
    public static void Interact(this RaycastHit2D hit)
    {
        hit.transform.GetComponent<TInteractable>().Interact();
    }

    public static void IsOpenable(this RaycastHit2D hit)
    {
        hit.transform.GetComponent<TInteractable>().SetOpen();
    }
}