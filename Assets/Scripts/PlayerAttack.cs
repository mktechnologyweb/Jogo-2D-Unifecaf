using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("Configurações de Dano")]
    public int danoNormal = 20;      
    public int danoSuperLanca = 100; 

    [Header("Área de Impacto")]
    public Transform pontoDeAtaque;  
    public float raioDoAtaqueConfigurado = 1.4f; 
    public float distanciaParaFrente = 1.2f; 
    public LayerMask layerInimigos;  

    private Animator anim;
    private float raioAtualDoAtaque = 0f; 
    [HideInInspector] public bool estaAtacando = false; 

    void Start()
    {
        anim = GetComponent<Animator>();
        raioAtualDoAtaque = 0f; 
        estaAtacando = false;
    }

  void Update()
{
    if (Input.GetButtonDown("Fire1") && !estaAtacando)
    {
        StartCoroutine(ExecutarSequenciaAtaque());
    }

    AjustarPosicaoDoPonto();
}

    void AjustarPosicaoDoPonto()
    {
        if (pontoDeAtaque != null)
        {
            float direcaoOlhar = transform.localScale.x > 0 ? 1f : -1f;
            pontoDeAtaque.localPosition = new Vector3(distanciaParaFrente * direcaoOlhar, -0.2f, 0f);
        }
    }

    IEnumerator ExecutarSequenciaAtaque()
    {
        estaAtacando = true; 

        if (anim != null)
        {
            anim.SetTrigger("attack"); 
        }

        
        yield return new WaitForSeconds(0.1f);

       
        raioAtualDoAtaque = raioDoAtaqueConfigurado;
        RealizarDano(); 

        
        raioAtualDoAtaque = 0f;

        yield return new WaitForSeconds(0.15f); 
        estaAtacando = false; 
    }
public void RealizarDano()
{
    string faseAtual =
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

    
    if (faseAtual == "Level3")
    {
        return;
    }

    int danoAtual = danoNormal;

    if (faseAtual == "Level2" && GameData.diamantesFase1)
    {
        danoAtual = danoSuperLanca;
    }

    Collider2D[] inimigosAtingidos =
        Physics2D.OverlapCircleAll(
            pontoDeAtaque.position,
            raioAtualDoAtaque,
            layerInimigos);

    foreach (Collider2D enemy in inimigosAtingidos)
    {
        EnemyPatrol scriptInimigo = enemy.GetComponent<EnemyPatrol>();

        if (scriptInimigo != null)
            scriptInimigo.TakeDamage(danoAtual);

        DinoIA dinoBoss = enemy.GetComponent<DinoIA>();

        if (dinoBoss != null)
            dinoBoss.TomarDano(danoAtual);
    }
}
 public bool LaserLiberado()
{
    return
        GameData.diamantesFase1 &&
        GameData.diamantesFase2 &&
        GameData.diamantesFase3;
}
    void OnDrawGizmos()
    {
        if (pontoDeAtaque == null) return;
        Gizmos.color = Color.cyan; 
        Gizmos.DrawWireSphere(pontoDeAtaque.position, estaAtacando ? raioDoAtaqueConfigurado : 0.2f);
    }
}