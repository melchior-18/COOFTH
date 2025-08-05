using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class charactèrecontroleur : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
   // [SerializeField] private float jumpForce;
    //[SerializeField] private LayerMask groundlayer;
    private Rigidbody2D rb;
    private GameObject square;
    private Vector2 scaleChange;

    private InputAction moveAction;

    SpriteRenderer spriteRenderer;

    //private InputAction jumpAction;

    private InputAction crouchAction;

    //private int nbJumpLeft = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        // jumpAction = InputSystem.actions.FindAction("Jump");
        spriteRenderer = GetComponent<SpriteRenderer>();
        crouchAction = InputSystem.actions.FindAction("Crouch");

        square = GameObject.Find("Idle_0");
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if((groundlayer.value &(1<<other.gameObject.layer)) != 0)
        {
            nbJumpLeft = 2;
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        rb.linearVelocityX = move.x * moveSpeed;
        rb.linearVelocityY = move.y * moveSpeed;

        /*
        if (jumpAction.WasPressedThisFrame()&& nbJumpLeft>0)
        {
            rb.AddForceY(jumpForce);
            nbJumpLeft--;
        }
        */

        spriteRenderer.flipX = rb.linearVelocityX < 0;

        if (crouchAction.WasPressedThisFrame())
        {
            moveSpeed -= 3;
            scaleChange = new Vector2(1f, 1f);
            square.transform.localScale = scaleChange;
        }

        if (crouchAction.WasReleasedThisFrame())
        {
            moveSpeed += 3;
            scaleChange = new Vector2(1f, 1.8676f);
            square.transform.localScale = scaleChange;
        }
    }
}
