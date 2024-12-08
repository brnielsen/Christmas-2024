using UnityEngine;

public class Chimney : MonoBehaviour
{
    public float DunkRadius = 0.5f;
    public Transform LaunchTarget;

    public Transform DunkTarget;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(LaunchTarget.position, 0.2f);
        Gizmos.DrawWireSphere(DunkTarget.position, DunkRadius);
    }
}
