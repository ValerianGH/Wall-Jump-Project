using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxspeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rollForce;
    /*
    [SerializeField] private float wallJump;
    [SerializeField] private float wallJumpForce;
    [SerializeField] private float wallSlide;
    [SerializeField] private float wallSlideSpeed;
    */
    [SerializeField] private Transform RaycastStartTransform;

    public Animator animator;
    private SpriteRenderer spriterenderer;
    private Rigidbody2D rb2D;
    private Controls controls;
    private float direction;
    private bool canJump = false;
    private bool moving;
    private bool jump;
    private bool isGrounded;
    public PhysicsMaterial2D Material;
    public LayerMask Ground;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();
        controls.Main.Move.performed += MovePerformed;
        controls.Main.Move.canceled += MoveCanceled;
        controls.Main.Jump.performed += JumpOnperformed;
    }

    private void JumpOnperformed(InputAction.CallbackContext obj)
    {
        if (canJump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            canJump = false;
        }
    }

    private void MoveCanceled(InputAction.CallbackContext obj)
    {
        direction = 0;
        moving = false;
    }

    private void MovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<float>();
        if (direction > 0)
        {
            spriterenderer.flipX = false;
        }
        else
        {
            spriterenderer.flipX = true;
        }
        moving = true;
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("Space") && isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        }

        var hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), 0.001f);
        if ((hit.collider )!= null)

        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
        
        if (moving == true)
        {
            animator.SetBool("moving", true);
        }

        else
        {
            animator.SetBool("moving", false);
        }

        if (jump == true)
        {
            animator.SetBool("jump", true);
        }

        else
        {
            animator.SetBool("jump", false);
        }

        void FixedUpdate()
        {
            var horizontalSpeed = Mathf.Abs(rb2D.velocity.x);
            if (horizontalSpeed < maxspeed)
            {
                rb2D.AddForce(new Vector2(speed * direction, 0));
            }
        }
    }
}