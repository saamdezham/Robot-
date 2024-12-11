using UnityEngine;

public class RayCast : MonoBehaviour
{
    public float raycast_length = 5f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * raycast_length);
    }
}
