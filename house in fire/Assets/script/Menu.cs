using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public InputAction start;

    void Start()
    {
        start= InputSystem.actions.FindAction("Start");
        
    }
    

    void OnEnable()
    {
        start.Enable();
    }

    void OnDisable()
    {
        start.Disable();
    }

    void Update()
    {
        if (start.WasPressedThisFrame())
        {
            SceneManager.LoadScene("SampleScene");
            Debug.Log("Scene change worked");
        }
    }
}