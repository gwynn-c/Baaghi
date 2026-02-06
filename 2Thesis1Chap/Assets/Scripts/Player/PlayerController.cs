using StarterAssets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private Camera mainCamera;

    [SerializeField] private GunController equippedGun;
    [SerializeField] private Transform equippedGunSlot;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(_input == null) return;
        
        InputHandler();
    }


    private void InputHandler()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        equippedGun.GunInputHandler(_input, ray);
    }

    public void Takedown()
    {
        Debug.Log("Taking Down");
    }
}