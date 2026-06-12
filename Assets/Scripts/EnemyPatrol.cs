using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    
    [Header("Vida do Inimigo")]
    public int maxHealth = 100;
    private int currentHealth;

    private bool movingRight = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        float horizontalSpeed = movingRight ? speed : -speed;
        rb.linearVelocity = new Vector2(horizontalSpeed, rb.linearVelocity.y);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
       
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " MORREU PELO ATAQUE!");
        if(GetComponent<Collider2D>() != null) GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject); 
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            movingRight = !movingRight;
            Flip();
        }
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Se encostar no corpo do Player
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAttack ataquePlayer = collision.gameObject.GetComponent<PlayerAttack>();

            if (ataquePlayer != null)
            {
               
                if (ataquePlayer.estaAtacando)
                {
                    
                    return; 
                }
            }

            
            Debug.Log("PLAYER ENCOSTOU SEM CLICAR E TOMOU DANO!");
            if (GameManager.instance != null)
            {
                PlayerDeath playerDeath =
                collision.gameObject.GetComponent<PlayerDeath>();

        if (playerDeath != null)
            {
        playerDeath.TakeDamage();
}
            }
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}