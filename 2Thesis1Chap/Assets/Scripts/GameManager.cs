using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Animator loadingScreenAnimator;
    bool hasPressedStart = true;
    [SerializeField] private UnityEvent startGame;
    private IDisposable m_EventListener;
    void OnEnable() => m_EventListener =  InputSystem.onAnyButtonPress.Call(AnyButton);
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        hasPressedStart = false;
        DontDestroyOnLoad(this);
    }

    private void AnyButton(InputControl control)
    {
        if(!hasPressedStart) startGame?.Invoke();
    }
    private void OnDisable()
    {
        m_EventListener?.Dispose();
    }

   
    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadNextScene(sceneName));
    }
    
    IEnumerator LoadNextScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while(!operation.isDone)
        {
            //GameObjectLoading + animation
            loadingScreenAnimator.SetTrigger("LoadingOn");
            // progress
            yield return null;
        }
        //play animation for transition
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        if(loadingScreenAnimator != null)
            loadingScreenAnimator.SetTrigger("LoadingOff");
    }
}
