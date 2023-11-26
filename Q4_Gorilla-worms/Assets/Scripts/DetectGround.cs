using UnityEngine;

public class DetectGround : MonoBehaviour
{
    private bool _onGround;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _onGround = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _onGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _onGround = false;
    }

    public bool OnGround()
    {
        return _onGround;
    }
}