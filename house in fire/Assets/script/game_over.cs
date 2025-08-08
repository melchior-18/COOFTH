using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class NewMonoBehaviourScript : MonoBehaviour
{
    private InputAction restart;

    void Start()
    {
        restart = InputSystem.actions.FindAction("Restart");
    }

    // Update is called once per frame
    void Update()
    {
        if (restart.WasPressedThisFrame())
        {
            print("RestartAction was pressed");
            SceneManager.LoadScene("SampleScene");
            print("Scene have been changed");
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space has been pressed");
            SceneManager.LoadScene("SampleScene");
            print("The scene changement worked");
        }*/
    }
}
