using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class charactèrecontroleur : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] barre_vie barreVie;
    //[SerializeField] private float jumpForce;
    //[SerializeField] private LayerMask groundlayer;
    public bool dead;
    private Rigidbody2D rb;
    private GameObject square;
    private Vector2 scaleChange;
    private Vector2 positionChange;
    Animator animator;
    
    [SerializeField] private bool IsBreathing = true;

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
        animator = GetComponent<Animator>();

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
        if (barreVie.isDead == false)
        {
            if (IsBreathing == false)
            {
                barreVie.PrendreDegats(20 * Time.deltaTime);
            }
            else
            {
                barreVie.PrendreDegats(-20 * Time.deltaTime);
            }

            int swapX = 1;
            int swapY = 1;
            bool swapDirection = false;
            if (barreVie.GetDegat()<50)
            {
                swapX = -1;
                swapY = -1;
            }

            Vector2 move = moveAction.ReadValue<Vector2>();
            if (swapDirection)
            {
                rb.linearVelocityX = swapX * move.y * moveSpeed;
                rb.linearVelocityY = swapY * move.x * moveSpeed;
            }
            else
            {
                rb.linearVelocityX = swapX * move.x * moveSpeed;
                rb.linearVelocityY = swapY * move.y * moveSpeed;
            }

            /*
            if (jumpAction.WasPressedThisFrame()&& nbJumpLeft>0)
            {
                rb.AddForceY(jumpForce);
                nbJumpLeft--;
                animator.SetBool("jump",true);
            }
            */


            animator.SetFloat("AbsSpeedY", rb.linearVelocityY);
            animator.SetFloat("AbsSpeedX", Mathf.Abs(rb.linearVelocityX));
            //animator.SetBool("jump", true);

            spriteRenderer.flipX = rb.linearVelocityX > -0.1;

            if (crouchAction.WasPressedThisFrame() && moveSpeed == 5)
            {
                animator.SetBool("crouch", true);
                moveSpeed = 2;
                scaleChange = new Vector2(0.39f, 0.39f);
                square.transform.localScale = scaleChange;
                positionChange = new Vector2(square.transform.position.x,
                    square.transform.position.y + square.transform.position.y / 8);
                square.transform.position = positionChange;
            }

            if (crouchAction.WasReleasedThisFrame() && animator.GetBool("canUncrouch") == true && moveSpeed == 2)
            {
                animator.SetBool("crouch", false);
                moveSpeed = 5;
                scaleChange = new Vector2(0.54038f, 0.54038f);
                square.transform.localScale = scaleChange;
                positionChange = new Vector2(square.transform.position.x,
                    square.transform.position.y - square.transform.position.y / 9);
                square.transform.position = positionChange;
            }
        }
    }
    
    //  smoke conllusion
    
    void OnTriggerEnter2D( Collider2D col)
    {
        if (col.gameObject.tag == "Smoke")
        {
            IsBreathing = false;
        }
        
    }
    void OnTriggerExit2D( Collider2D col)
    {
        if (col.gameObject.tag == "Smoke")
        {
            IsBreathing = true;
        }
    }
}