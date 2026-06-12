using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smooth = 5f;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 target = new Vector3(player.position.x, player.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, target, smooth * Time.deltaTime);
    }
}