using UnityEngine;

public class DinoIA : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform playerTransform;

    [Header("Configuracoes de Vida")]
    public int vidaMaxima = 3;
    private int vidaAtual;

    [Header("Configuracoes de Movimento")]
    public float velocidadePatrulha = 2f;
    public float velocidadePerseguicao = 4.5f;
    private float velocidadeAtual;
    
    private bool patrulhaIndoParaDireita = true;

    
    private bool tocandoParedeEsquerda = false;
    private bool tocandoParedeDireita = false;

    [Header("Laser do Dino")]
    public GameObject prefabLaser;      
    public Transform pontoDisparoLaser; 
    public float tempoEntreLasers = 1.5f; 
    private float proximoLaser;

    private int estadoAtual = 0; 

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();

    Debug.Log("VIDA MAXIMA = " + vidaMaxima);

    vidaAtual = vidaMaxima;

    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
        playerTransform = player.transform;
}

    void Update()
    {
        if (playerTransform == null) return;
        float dist = Vector2.Distance(transform.position, playerTransform.position);

        if (dist <= 5f) estadoAtual = 2; 
        else if (dist <= 15f) estadoAtual = 1; 
        else estadoAtual = 0;

        if (estadoAtual == 2)
        {
            velocidadeAtual = 0f;
            OlharParaAlvo(playerTransform.position.x > transform.position.x);
            if (Time.time >= proximoLaser) DispararLaser();
        }
        else if (estadoAtual == 1) Perseguir();
        else Patrulhar();
    }

    void DispararLaser()
    {
        proximoLaser = Time.time + tempoEntreLasers;
        if (anim) anim.SetTrigger("Attack");

        if (prefabLaser && pontoDisparoLaser)
        {
            GameObject laser = Instantiate(prefabLaser, pontoDisparoLaser.position, Quaternion.identity);
            laser.transform.SetParent(null); 
            
            float dir = (playerTransform.position.x > transform.position.x) ? 1f : -1f;
            laser.GetComponent<LaserProjetil>().ConfigurarDirecao(dir, "Player");
        }
    }

    void Patrulhar()
    {
       
        if (tocandoParedeDireita) patrulhaIndoParaDireita = false;
       
        if (tocandoParedeEsquerda) patrulhaIndoParaDireita = true;

        if (patrulhaIndoParaDireita) 
        { 
            velocidadeAtual = velocidadePatrulha; 
            OlharParaAlvo(true); 
        }
        else 
        { 
            velocidadeAtual = -velocidadePatrulha; 
            OlharParaAlvo(false); 
        }
        rb.linearVelocity = new Vector2(velocidadeAtual, rb.linearVelocity.y);
    }

    void Perseguir()
    {
        bool irParaDireita = playerTransform.position.x > transform.position.x;
        OlharParaAlvo(irParaDireita);

       
        if (irParaDireita && tocandoParedeDireita)
        {
            velocidadeAtual = 0f;
        }
        
        else if (!irParaDireita && tocandoParedeEsquerda)
        {
            velocidadeAtual = 0f;
        }
        else
        {
            velocidadeAtual = irParaDireita ? velocidadePerseguicao : -velocidadePerseguicao;
        }

        rb.linearVelocity = new Vector2(velocidadeAtual, rb.linearVelocity.y);
    }

    void OlharParaAlvo(bool direita) 
    {
        transform.localScale = new Vector3(direita ? 1f : -1f, 1f, 1f);
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            if (other.transform.position.x > transform.position.x)
                tocandoParedeDireita = true;
            else
                tocandoParedeEsquerda = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            tocandoParedeDireita = false;
            tocandoParedeEsquerda = false;
        }
    }

  public void TomarDano(int dano)
{
    Debug.Log("DINO TOMOU DANO: " + dano);

    vidaAtual -= dano;

    if (vidaAtual <= 0)
    {
        if (GameManager.instance != null)
            GameManager.instance.DinoMorreu();

        Destroy(gameObject);
    }

}


    
}