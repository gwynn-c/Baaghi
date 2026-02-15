using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private InteractionController interactionController;
    [SerializeField] private GunController gun;
    [SerializeField] private GameObject containerGameObject;

    [SerializeField] TextMeshProUGUI interactTextMeshProUGUI;
    [SerializeField] TextMeshProUGUI bulletInfoTextMeshProUGUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gun = GetComponent<PlayerController>().GetEquippedGun();
    }

    // Update is called once per frame
    private void Update(){
        if(interactionController.GetInteractableObject() != null) {
            Show(interactionController.GetInteractableObject());
        } else {
            Hide();
        }

        var bulletInfo = $"{gun.bulletsLeft} / {gun.maxAmmo}";
        bulletInfoTextMeshProUGUI.SetText(bulletInfo);
    }
    
    private void Show(IInteractable interactable){
        containerGameObject.SetActive(true);
        interactTextMeshProUGUI.text = "E to " + interactable.GetInteractText();
    }   
    private void Hide(){
        containerGameObject.SetActive(false);
    }
    
}
