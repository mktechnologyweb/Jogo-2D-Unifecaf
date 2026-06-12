using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI; 

public class PlayerDeath : MonoBehaviour
{
    public Vector3 spawnPoint;

    public int maxLives = 3;
    public int currentLives;

    private Animator anim;
    private PlayerMovement movement;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    public AudioClip damageSound;
    public AudioClip deathSound;
    public AudioClip gameOverSound;

    public GameObject gameOverText;

    private bool isDead = false;

    void Start()
    {
        spawnPoint = transform.position;
        currentLives = maxLives;

        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (gameOverText != null)
            gameOverText.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death") && !isDead)
        {
            TakeDamage();
        }
    }

   public void TakeDamage()
    {
        currentLives--;

        Debug.Log("Vidas: " + currentLives);
        if (GameManager.instance != null)
{
    GameManager.instance.UpdateLifeUI();
}
        anim.SetTrigger("Hurt");

        if (damageSound != null)
            audioSource.PlayOneShot(damageSound);

        movement.enabled = false;

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        if (currentLives > 0)
        {
            StartCoroutine(RespawnDelay());
        }
        else
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(0.6f);

        transform.position = spawnPoint;

        rb.bodyType = RigidbodyType2D.Dynamic;

        movement.enabled = true;
    }

    IEnumerator Die()
    {
        isDead = true;

        anim.SetTrigger("Hurt");

       
        if (deathSound != null)
            audioSource.PlayOneShot(deathSound);

        yield return new WaitForSeconds(0.11f);

        
        if (gameOverText != null)
            gameOverText.SetActive(true);

        
        if (gameOverSound != null)
            audioSource.PlayOneShot(gameOverSound);


if (SceneManager.GetActiveScene().name == "Level3")
{
    GameData.diamantesFase3 = false;

    
}

yield return new WaitForSeconds(2f);

SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
}