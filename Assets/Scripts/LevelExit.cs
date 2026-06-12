using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelExit : MonoBehaviour
{
    public AudioClip levelCompleteSound;
    private bool isLoading = false;
    private bool portalAtivo = false; 

    private SpriteRenderer spriteRenderer;
    private Collider2D meuCollider;

    void Awake()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        meuCollider = GetComponent<Collider2D>();
    }

    void Start()
    {
       
        string nomeFaseAtual = SceneManager.GetActiveScene().name;

       
        if (nomeFaseAtual == "Level3")
        {
           
            portalAtivo = false; 
        }
        else
        {
           
            portalAtivo = true;
            if (spriteRenderer != null) spriteRenderer.enabled = true;
            if (meuCollider != null) meuCollider.enabled = true;
        }
    }

    
    public void EsconderPortal()
    {
       
        {
            portalAtivo = false;
            if (spriteRenderer != null) spriteRenderer.enabled = false; 
            if (meuCollider != null) meuCollider.enabled = false;       
        }
    }

   
    public void AparecerPortal()
    {
        portalAtivo = true;
        if (spriteRenderer != null) spriteRenderer.enabled = true;  
        if (meuCollider != null) meuCollider.enabled = true;        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (portalAtivo && other.CompareTag("Player") && !isLoading)
        {
            isLoading = true;

           
            if (levelCompleteSound != null)
            {
                GameObject tempAudio = new GameObject("TempAudio");
                AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
                audioSource.clip = levelCompleteSound;
                audioSource.Play();
                Destroy(tempAudio, levelCompleteSound.length);
            }

            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1.2f);

       
        if (SceneManager.GetActiveScene().name == "Level3" && GameManager.instance != null)
        {
            GameManager.instance.VencerJogo();
        }
        else
        {
            
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            string nomeFase = SceneManager.GetActiveScene().name;

if (GameManager.instance != null)
{
    if (nomeFase == "Level1")
    {
        if (GameManager.instance.score >= GameManager.instance.totalDiamantesNaFase)
        {
            GameData.diamantesFase1 = true;
            
        }
    }

    if (nomeFase == "Level2")
    {
        if (GameManager.instance.score >= GameManager.instance.totalDiamantesNaFase)
        {
            GameData.diamantesFase2 = true;
            
        }
    }

    if (nomeFase == "Level3")
    {
        if (GameManager.instance.score >= GameManager.instance.totalDiamantesNaFase)
        {
            GameData.diamantesFase3 = true;
           
        }
    }
}
            SceneManager.LoadScene(currentIndex + 1);
        }
    }
}