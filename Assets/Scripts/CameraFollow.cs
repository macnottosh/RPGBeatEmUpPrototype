using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.8f, -10);
    private void LateUpdate() 
    {
        transform.position = player.transform.position + offset;
    }
}
