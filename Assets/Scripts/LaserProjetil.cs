using UnityEngine;

public class LaserProjetil : MonoBehaviour
{
    public float velocidade = 15f;
    private float direcao = 1f;
    private string tagAlvo = "Player"; 

    void Start() => Destroy(gameObject, 2f);

    void Update()
    {
        transform.Translate(Vector3.right * direcao * velocidade * Time.deltaTime);
    }

    public void ConfigurarDirecao(float d, string alvo)
    {
        direcao = d;
        tagAlvo = alvo;
        transform.localScale = new Vector3(d, 1f, 1f);
    }

   void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag(tagAlvo))
    {
        if (tagAlvo == "Enemy")
        {
            DinoIA dino = other.GetComponent<DinoIA>();

            if (dino != null)
                dino.TomarDano(1);
        }
        else if (tagAlvo == "Player")
        {
            PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();

            if (playerDeath != null)
            {
                playerDeath.TakeDamage();
            }
        }

        Destroy(gameObject);
    }

    if (other.CompareTag("Ground"))
    {
        Destroy(gameObject);
    }
}
}