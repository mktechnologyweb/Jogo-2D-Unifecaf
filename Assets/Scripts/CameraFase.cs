using UnityEngine;

public class CameraFase : MonoBehaviour
{
    private Transform alvoPlayer;
    
    [Header("Configuracoes de Seguidor")]
    public float suavidade = 0.125f; 
    public Vector3 deslocamento = new Vector3(0f, 2f, -10f); 

    void Start()
    {
        EncontrarPlayer();
    }

    void LateUpdate()
    {
        if (alvoPlayer == null)
        {
            EncontrarPlayer();
            return; 
        }

        // Aqui é aonde a camera segue o player livremente pelo mapa
        Vector3 posicaoDesejada = alvoPlayer.position + deslocamento;

        Vector3 posicaoSuavizada = Vector3.Lerp(transform.position, posicaoDesejada, suavidade);
        transform.position = posicaoSuavizada;
    }

    void EncontrarPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            alvoPlayer = playerObj.transform;
        }
    }
}