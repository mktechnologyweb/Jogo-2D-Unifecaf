using UnityEngine;

public class LancaPlayer : MonoBehaviour
{
    public GameObject prefabLaserLanca;  
    public Transform pontoDisparoLanca;  
    public float tempoEntreAtaques = 0.5f;
    private float proximoAtaque;

    void Update()
    {
        
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl)) && Time.time >= proximoAtaque)
        {
           
            if (Cristal.laserLiberado)
            {
                AtirarLaserLanca();
            }
        }
    }

    void AtirarLaserLanca()
    {
        proximoAtaque = Time.time + tempoEntreAtaques;

        if (prefabLaserLanca != null && pontoDisparoLanca != null)
        {
            GameObject laser = Instantiate(prefabLaserLanca, pontoDisparoLanca.position, Quaternion.identity);
            laser.transform.SetParent(null);

            float direcaoPlayer = transform.root.localScale.x > 0 ? 1f : -1f;
            laser.GetComponent<LaserProjetil>().ConfigurarDirecao(direcaoPlayer, "Enemy");
        }
    }
}