using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Objetivos de Coleta")]
    public int score = 0; 
    public int totalDiamantesNaFase = 5; 

    [Header("Objetivos de Combate")]
    public int dinossaurosVivos = 0; 

    [Header("Referencia do Portal")]
    public LevelExit portalFimDeFase; 

    [Header("Configuracao de Fim de Jogo")]
    public string nomeCenaVitoria = "TelaVitoria"; 
    

    public int diamantesColetados => score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public Vector3 checkpointPosition;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        checkpointPosition = Vector3.zero;
        
        
        dinossaurosVivos = GameObject.FindGameObjectsWithTag("Enemy").Length;

        
        if (portalFimDeFase != null)
        {
            portalFimDeFase.EsconderPortal();
        }

        UpdateUI();
        UpdateLifeUI();
    }

  public void AddScore(int value)
{
    score += value;

    string faseAtual =
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

    if (score >= totalDiamantesNaFase)
    {
        if (faseAtual == "Level1")
        {
            GameData.diamantesFase1 = true;
            Debug.Log("Diamantes da Fase 1 completos!");
        }

        if (faseAtual == "Level2")
        {
            GameData.diamantesFase2 = true;
            Debug.Log("Diamantes da Fase 2 completos!");
        }

        if (faseAtual == "Level3")
        {
            GameData.diamantesFase3 = true;
           

            
        }
    }

    UpdateUI();
    VerificarObjetivos();
}
    public void DinoMorreu()
    {
        dinossaurosVivos--;
        VerificarObjetivos(); 
    }

    
    void VerificarObjetivos()
    {
     
        if (score >= totalDiamantesNaFase && dinossaurosVivos <= 0)
        {
            if (portalFimDeFase != null)
            {
                
                portalFimDeFase.AparecerPortal(); 
            }
        }
    }

    public void VencerJogo()
    {
        SceneManager.LoadScene(nomeCenaVitoria);
    }


   
    public void SetCheckpoint(Vector3 pos)
    {
        checkpointPosition = pos;
    }

    void UpdateUI()
{
    if (scoreText != null)
        scoreText.text = "Cristais: " + score;
        UpdateLifeUI();
    if (lifeText != null)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            PlayerDeath pd = player.GetComponent<PlayerDeath>();

            if (pd != null)
                lifeText.text = "Vidas: " + pd.currentLives;
        }
    }

}
public void UpdateLifeUI()
{
    if (lifeText != null)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            PlayerDeath pd = player.GetComponent<PlayerDeath>();

            if (pd != null)
            {
                lifeText.text = "Vidas: " + pd.currentLives;
            }
        }
    }

}
}