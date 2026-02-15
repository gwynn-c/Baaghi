using UnityEngine;

public class Takedown : MonoBehaviour, IInteractable
{
    public string interactText;
    public 
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

        if ((transform.position - interactor.position).magnitude <= 2.5f)
        {
            interactor.GetComponent<PlayerController>().Takedown();
            Destroy(gameObject);
        }
        
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetInteractableTransform()
    {
       return transform;
    }
}
