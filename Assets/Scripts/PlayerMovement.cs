using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //On créé des variables sérialisées afin de pouvoir les changer à notre grés de manière plus simple
    [SerializeField] private float speed;
    [SerializeField] private float maxspeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rollForce;
    [SerializeField] private Transform RaycastStartTransform;

    private SpriteRenderer spriterenderer;
    private Animator animator;
    private Rigidbody2D rb2D;
    private Controls controls;
    private float direction;
    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();
        controls.Movement.LeftRight.performed += LeftRight;
        controls.Movement.Space.performed += Space;
        controls.Movement.LeftRight.canceled += LeftRightCanceled;
    }

    private void LeftRightCanceled(InputAction.CallbackContext obj)
    {
        direction = 0;
    }

    private void LeftRight(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<float>();
        if (direction > 0)
        {
            spriterenderer.flipX = true;
            //ChangeAnimationState(RUN_RIGHT);
        }
        else //(direction<0)
        {
            spriterenderer.flipX = false;
            //ChangeAnimationState(RUN_LEFT);
        }
    }

    void FixedUpdate()
    {
        var horizontalSpeed = Mathf.Abs(rb2D.velocity.x);
        if (horizontalSpeed < maxspeed)
        {
            rb2D.AddForce(new Vector2(speed * direction, 0));
        }
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();



    }

    private void Space(InputAction.CallbackContext obj)
    {
        rb2D.gravityScale *= -1;
        if (rb2D.gravityScale < 0)
        {
            spriterenderer.flipY = true;
        }
        else
        {
            spriterenderer.flipY = false;
        }
    }
}