using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerMovement playerMovement; 

    private bool isTouchingPilastra;
    private bool isClimbing;
    private float verticalInput; 

    [Header("Configurações de Velocidade")]
    public float climbSpeed = 4f;

    [Header("Configuração do Topo (Eixo Y)")]
    public float alturaDoTopo = -2.5f; 
    public float posicaoXPlataforma = -13.5f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>(); 
    }

   void Update()
{
    verticalInput = Input.GetAxisRaw("Vertical");

    
    if (isTouchingPilastra && Mathf.Abs(verticalInput) > 0.1f && !isClimbing)
    {
        isClimbing = true;
        
        if (playerMovement != null) 
        {
            playerMovement.SetClimbingState(true);
            playerMovement.SetGrounded(false);
        }
    }
    
    else if (isClimbing && !isTouchingPilastra)
    {
        DesligarEscalada();
    }

   
    if (isClimbing && verticalInput > 0 && transform.position.y >= alturaDoTopo)
    {
        SubirNaPlataforma();
        return;
    }

    if (anim != null)
    {
        anim.SetBool("IsClimbing", isClimbing);
    }
}
    
    void FixedUpdate()
    {
        if (isClimbing)
        {
           
            rb.gravityScale = 0f;

            if (verticalInput != 0)
            {
                
                rb.linearVelocity = new Vector2(0f, verticalInput * climbSpeed);
            }
            else
            {
                
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    void SubirNaPlataforma()
    {
        isClimbing = false;
        rb.gravityScale = 3f;
        rb.linearVelocity = Vector2.zero;

        transform.position = new Vector2(posicaoXPlataforma, alturaDoTopo + 0.8f);
        
        if (anim != null)
        {
            anim.SetBool("IsClimbing", false);
        }

        if (playerMovement != null)
        {
            playerMovement.SetClimbingState(false);
            playerMovement.SetGrounded(true);
        }
    }

    void DesligarEscalada()
    {
        isClimbing = false;
        rb.gravityScale = 3f;
        
        if (playerMovement != null) 
        {
            playerMovement.SetClimbingState(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pilastra"))
        {
            isTouchingPilastra = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pilastra"))
        {
            isTouchingPilastra = false;
            
           
            if (isClimbing && rb.linearVelocity.y < 0)
            {
                DesligarEscalada();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TopoPilastra"))
        {
            DesligarEscalada();
            if (playerMovement != null) playerMovement.SetGrounded(true);
        }
    }
}