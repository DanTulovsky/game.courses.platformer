using UnityEngine;

public class HorizontalCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        // transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }
}
