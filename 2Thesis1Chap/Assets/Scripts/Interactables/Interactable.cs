using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public string interactString;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Transform interactor)
    {
        OpenDoor();
    }

    public string GetInteractText()
    {
        return interactString;
    }

    public Transform GetInteractableTransform()
    {
        return transform;
    }

    private void OpenDoor()
    {
        //For now  just set it as inactive
        gameObject.SetActive(false);
    }
}
