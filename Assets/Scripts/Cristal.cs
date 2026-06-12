using UnityEngine;

public class Cristal : MonoBehaviour
{
    public static int cristaisRestantes = 0;
    // Aviso ao jogo inteiro se o laser está liberado
    public static bool laserLiberado = false;

    void Start()
    {
        cristaisRestantes++;
        laserLiberado = false;    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cristaisRestantes--;

            if (cristaisRestantes <= 0)
            {
                laserLiberado = true;
            }

            Destroy(gameObject); 
        }
    }
}