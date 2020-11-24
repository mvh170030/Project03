using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioClip jump;
    AudioSource audioSource;

    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;

    public bool sliding;
    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    private bool canDoubleJump;
    private Collider2D myCollider;

    public Slider myHealthBar;
    public float Health = 100;
    private float currentHealth;
    public bool isDead;

    private IEnumerator coroutine;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();

        currentHealth = Health;
        coroutine = RuntimeHealth(2.0f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if (currentHealth <= 0)
        {
            isDead = true;
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                audioSource.PlayOneShot(jump);
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            }
            
            if (!grounded && canDoubleJump)
            {
                audioSource.PlayOneShot(jump);
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                canDoubleJump = false;
            }
        }

        // sliding
        if (Input.GetKey(KeyCode.RightShift) || Input.GetMouseButton(1))
        {
            if (grounded)
            {
                sliding = true;
            }

        } else { sliding = false; }


        if (grounded)
        {
            canDoubleJump = true;
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetBool("Sliding", sliding);
        myAnimator.SetBool("Dead", isDead);
    }

    // health decrease as time goes on
    private IEnumerator RuntimeHealth(float waitTime)
    {
        while (true)
        {
            // wait a tick, then decrease health
            currentHealth -= 4;
            myHealthBar.value = currentHealth;
            Debug.Log("Current health: " + currentHealth);
            
            // restart when die
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                myHealthBar.value = 0;
                Debug.Log("You have died!");
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene("Sandbox");
            }

            // tick
            yield return new WaitForSeconds(waitTime);
        }
    }

    // when picks up potion
    public void AddHealth(int healthToAdd)
    {
        currentHealth += healthToAdd;
        if (currentHealth >= 100)
        {
            currentHealth = 100;
        }
        myHealthBar.value = currentHealth;
        Debug.Log("Current health: " + currentHealth);
    }
}
