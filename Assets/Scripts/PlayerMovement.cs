using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 12f;

    private Rigidbody2D rb;
    private Animator anim;

    private bool isGrounded;
    private bool isJumping;
    private float moveInput; 

    
    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip footstepSound;

    private bool isPlayingFootstep;
    private bool isClimbingNow = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
        moveInput = Input.GetAxisRaw("Horizontal");

        
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

       
        if (moveInput != 0 && isGrounded)
        {
            if (!isPlayingFootstep)
            {
                audioSource.clip = footstepSound;
                audioSource.loop = true;
                audioSource.Play();
                isPlayingFootstep = true;
            }
        }
        else
        {
            if (isPlayingFootstep)
            {
                audioSource.Stop();
                isPlayingFootstep = false;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            audioSource.Stop();
            isPlayingFootstep = false;

         
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            isGrounded = false;
            isJumping = true;

            
            if (jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }

        if (isJumping && isGrounded)
        {
            isJumping = false;
        }

        
        anim.SetBool("IsJumping", !isGrounded);
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    
    void FixedUpdate()
    {
        if (isClimbingNow) return;  
       
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    public void SetClimbingState(bool climbing)
{
    isClimbingNow = climbing;
    if (climbing)
    {
        rb.linearVelocity = Vector2.zero; 
    }
}

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("plataform movel"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("plataform movel"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("plataform movel"))
        {
            isGrounded = false;
        }
    }
    
    public void SetGrounded(bool state)
    {
        isGrounded = state;
        if (anim != null)
        {
            anim.SetBool("IsJumping", !state);
        }
    }
}