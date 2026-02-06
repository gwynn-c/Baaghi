using UnityEngine;

public interface IInteractable
{
    void Interact(Transform interactor);
    public string GetInteractText();
    public Transform GetInteractableTransform();
    
}