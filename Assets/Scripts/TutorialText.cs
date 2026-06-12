using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public float timeToHide = 4f;

    void Start()
    {
        Invoke("HideText", timeToHide);
    }

    void HideText()
    {
        gameObject.SetActive(false);
    }
}