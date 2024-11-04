using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetClosestObject : MonoBehaviour
{
    [SerializeField] private InteractionController interactionControllerReference;
    private List<GameObject> _objects = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        GameObject interactable = other.gameObject;
        if (!interactable.TryGetComponent<Interactable>( out Interactable _interactable)) return;

        if (_objects.Contains(interactable))
        {
            return;
        }
        _objects.Add(interactable);

    }

    private void OnTriggerExit(Collider other)
    {
        GameObject interactable = other.gameObject;
        if (interactable)  
        {
            _objects.Remove(interactable);
        }
    }

    private void FixedUpdate()
    {
        GameObject closest = GetCloseInteractable();

        if (closest == interactionControllerReference.currentInteractable) { return; }

        //interactionControllerReference.currentInteractable.ToggleHighlightOff();

        interactionControllerReference.currentInteractable = closest;

        //closest.ToggleHighlightOn();
    }

    private GameObject GetCloseInteractable()
    {
        var minDistance = float.MaxValue;
        GameObject closest = null;
        foreach (var interactable in _objects)
        {
            var distance = Vector3.Distance(this.gameObject.transform.position, interactable.gameObject.transform.position);
            if (distance > minDistance) continue;
            minDistance = distance;
            closest = interactable;
        }

        return closest;
    }



}
