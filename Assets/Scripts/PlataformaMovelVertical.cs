using UnityEngine;

public class PlataformaMovelVertical : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 2f;
    
    public Transform pontoA; 
    public Transform pontoB; 

    private Vector2 destinoAtual;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;
        }

        if (pontoA != null)
        {
            transform.position = pontoA.position;
            destinoAtual = pontoB.position;
        }
    }

    void FixedUpdate()
    {
        if (pontoA == null || pontoB == null || rb == null) return;

        
        Vector2 posicaoAtual = rb.position;
        Vector2 novaPosicao = Vector2.MoveTowards(posicaoAtual, destinoAtual, velocidade * Time.fixedDeltaTime);

        
        rb.MovePosition(novaPosicao);

        
        if (Vector2.Distance(novaPosicao, destinoAtual) < 0.05f)
        {
            destinoAtual = destinoAtual == (Vector2)pontoA.position ? (Vector2)pontoB.position : (Vector2)pontoA.position;
        }
    }
}