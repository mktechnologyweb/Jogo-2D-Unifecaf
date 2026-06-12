using UnityEngine;

public class CollectCrystal : MonoBehaviour
{
    public AudioClip collectSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            GameManager.instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}


