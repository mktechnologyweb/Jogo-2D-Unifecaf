using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    public GameObject prefabLaser;
    public Transform pontoDisparo;

  void Update()
{
    bool laserLiberado =
        GameData.diamantesFase1 &&
        GameData.diamantesFase2 &&
        GameData.diamantesFase3;

    if (!laserLiberado)
        return;

    if (Input.GetButtonDown("Fire1"))
    {
        DispararLaser();
    }
}

    void DispararLaser()
    {
        GameObject laser =
            Instantiate(
                prefabLaser,
                pontoDisparo.position,
                Quaternion.identity);

        float direcao =
            transform.localScale.x > 0 ? 1f : -1f;

        LaserProjetil scriptLaser =
            laser.GetComponent<LaserProjetil>();

        if (scriptLaser != null)
        {
            scriptLaser.ConfigurarDirecao(
                direcao,
                "Enemy");
        }
    }
}