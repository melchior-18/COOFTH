using UnityEngine;

public class CrouchController : MonoBehaviour
{
    private GameObject square;
    [SerializeField] private LayerMask triggerLayer;
    [SerializeField] private Collider2D triggerCollider;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        square = GameObject.Find("Idle_0");
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((triggerLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            animator.SetBool("canUncrouch", true);
        }
    }
}