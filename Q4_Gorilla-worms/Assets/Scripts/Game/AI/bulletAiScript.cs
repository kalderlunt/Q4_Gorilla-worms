using UnityEngine;

public class bulletAiScript : MonoBehaviour
{
    private Rigidbody2D RB;
    private Vector2 _awakevel;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    public void SetAngle(Vector2 angle_vector, float force)
    {
        _awakevel = angle_vector * force;
        RB.velocity = _awakevel;
    }
}