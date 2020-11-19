using UnityEngine;

public class SnapTrapSIde2R : MonoBehaviour
{
    public bool IsTouchingPlayer;
    private int playerLayer = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
            IsTouchingPlayer = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
            IsTouchingPlayer = false;
    }
}
