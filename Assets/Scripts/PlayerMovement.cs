using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxspeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform RaycastStartTransform;

    private Controls controls;
    private Rigidbody2D rb2D;
    private float direction;
    private bool canJump = false;
    private bool wallJump = false;
    private SpriteRenderer spriterenderer;
    private Animator animator;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();
        controls.Main.Jump.performed += JumpOnperformed;
        controls.Main.Move.performed += MoveOnperformed;
        controls.Main.Move.canceled += MoveOncanceled;
    }

    private void JumpOnperformed(InputAction.CallbackContext obj) //prend l'info si l'input espace est enfoncé
    {
        if (wallJump) //fait référence à un boolean qui est dans le code void Update()
        {
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (canJump) //fait référence à un boolean qui est dans le code void Update()
        {
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void MoveOncanceled(InputAction.CallbackContext obj)
    {
        direction = 0;
    }

    private void MoveOnperformed(InputAction.CallbackContext obj) //on récupère 1 pour la flèche droite et -1 pour la flèche enfoncée
    {
        direction = obj.ReadValue<float>();
        if (direction > 0)
        {
            spriterenderer.flipX = false;
        }
        else //(direction < 0)
        {
            spriterenderer.flipX = true;
        }
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var hit = Physics2D.Raycast(RaycastStartTransform.position, new Vector2(-1, 0), 0.001f);
        //Debug.DrawRay(RaycastStartTransform.position, new Vector2(1, 0) * 0.001f);
        if (hit.collider != null)
        {
            canJump = true; //on peut sauter
        }
        else
        {
            canJump = false; //on ne peut pas sauter
        }
    }

    private void FixedUpdate()
    {
        var horizontalSpeed = Mathf.Abs(rb2D.velocity.x);
        if (horizontalSpeed < maxspeed)
        {
            rb2D.AddForce(new Vector2(speed * direction, 0));
        }

        float characterVelocity = Mathf.Abs(rb2D.velocity.x);
        animator.SetFloat("speed", characterVelocity);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y == 0) // plafond ( 0 , -1) sol ( 0 , 1 )
        {
            wallJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        wallJump = false;
    }
}