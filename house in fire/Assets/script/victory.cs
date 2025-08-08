using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class victory : MonoBehaviour
{
    public InputAction restart;

    void Start()
    {
        restart = InputSystem.actions.FindAction("Restart");
    }


    void OnEnable()
    {
        restart.Enable();
    }

    /*
    void OnDisable()
    {
        restart.Disable();
    }*/

    void Update()
    {
        
        if (restart.WasPressedThisFrame())
        {
            SceneManager.LoadScene("Menu");
            Debug.Log("Scene change worked");
        }
    }
}