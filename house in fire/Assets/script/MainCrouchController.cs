using UnityEngine;

public class MainCrouchController : MonoBehaviour
{
    private GameObject square;
    [SerializeField] private LayerMask triggerLayer;
    [SerializeField] private Collider2D triggerCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        square = GameObject.Find("Idle_0");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((triggerLayer.value & (1 << other.gameObject.layer)) != 0 && square.GetComponent<charactèrecontroleur>().canUncrouch == true)
        {
            square.GetComponent<charactèrecontroleur>().canUncrouch = false;
        }
    }
}