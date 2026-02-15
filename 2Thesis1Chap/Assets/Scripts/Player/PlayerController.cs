using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private Camera mainCamera;

    [SerializeField] private GunController equippedGun;
    [SerializeField] private Transform equippedGunSlot;
    public GameObject TakedownTimeLine;
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
        TakedownTimeLine.SetActive(true);
        StartCoroutine(nameof(TakeDownReset));
    }

    public IEnumerator TakeDownReset()
    {
        yield return new WaitUntil(() => TakedownTimeLine.GetComponent<PlayableDirector>().state != PlayState.Playing);
        TakedownTimeLine.SetActive(false);
    }
    public GunController GetEquippedGun()
    {
        return equippedGun;
    }
}