using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class game_over : MonoBehaviour
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

    void OnDisable()
    {
        restart.Disable();
    }

    void Update()
    {
        
        if (restart.WasPressedThisFrame())
        {
            SceneManager.LoadScene("SampleScene");
            Debug.Log("Scene change worked");
        }
    }
}