using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public ParticleSystem deathParticles;
    public LogicScript logic;
    public bool isAlive = true;

    public SpecialAbility specialAbility; // Im Inspector setzen!

    [Header("Movement")]
    public float Speed;
    public float slowness;
    public float jumpForce;

    [Header("GroundCheck")]
    public Transform groundCheckPoint;
    public float radius = 0.1f;
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("DeathProps")]
    public bool isTouchingSpike = false;
    public bool isTouchingExplosion = false;

    [Header("PowerUp's")]
    public GameObject regenenerationPowerUp;
    public bool icRegene = false;

    [Header("KeyBinds")]
    public InputAction jumpAction;
    public InputAction runAction;
    public InputAction jumpDashAction;
    public InputAction slowAction;
    public InputAction abilityAction;
    public InputAction backJumpAction;

    private void OnEnable()
    {
        jumpAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/space");
        jumpAction.Enable();
        runAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/d");
        runAction.Enable();
        jumpDashAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/q");
        jumpDashAction.Enable();
        slowAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/a");
        slowAction.Enable();
        abilityAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        abilityAction.Enable();
        backJumpAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/rightButton");
        backJumpAction.Enable();
    }

    private void OnDisable()
    {
        jumpAction.Disable();
        runAction.Disable();
        jumpDashAction.Disable();
        slowAction.Disable();
        abilityAction.Disable();
        backJumpAction.Disable();
    }

    void Update()
    {
        HandleActions();
        HandleGroundCheck();
        HandleMovement();
        CheckDeath();
    }

    void HandleGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, radius, groundLayer);
    }

    void HandleMovement()
    {
        if (jumpAction.triggered && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
        else if (jumpAction.triggered && !isGrounded)
        {
            rb.linearVelocity = new Vector2(3, -2);
        }
        if (runAction.ReadValue<float>() > 0)
        {
            rb.linearVelocity = new Vector2(Speed, rb.linearVelocity.y);
        }

        if (slowAction.ReadValue<float>() > 0)
        {
            rb.linearVelocity = new Vector2(slowness, rb.linearVelocity.y);
        }
        if (backJumpAction.triggered && isGrounded)
        {
            rb.linearVelocity = new Vector2(-8, jumpForce);
        }

        
    }

    void HandleActions()
    {
        if (abilityAction.triggered)
        {
            specialAbility.TryActivateAbility(); // Neue saubere Methode
        }
    }

    void CheckDeath()
    {
        if (logic.lives <= 0)
        {
            KillPlayer();
            isAlive = false;
        }

        CheckPlayerPosition();
    }

    void CheckPlayerPosition()
    {
        if (transform.position.x > 9 || transform.position.x < -9.5f)
        {
            KillPlayer();
        }

        if (transform.position.y < -5.7)
        {
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        deathParticles.Play();
        Destroy(gameObject);
        isAlive = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            isTouchingSpike = true;
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Explosion"))
        {
            isTouchingExplosion = true;
        }

        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bomb"))
        {
            isTouchingExplosion = true;
        }

        if (other.CompareTag("RegenerationPowerUp"))
        {
            icRegene = true;
            Destroy(other.gameObject);
        }

    }
}
