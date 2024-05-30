using UnityEngine;
public class Coin : MonoBehaviour
{
    [SerializeField] private int coinScore;
    private bool _isPickedUp;
    [SerializeField] private AudioClip sound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isPickedUp)
        {
            _isPickedUp = true;
            Destroy(gameObject);
            FindObjectOfType<Session>().score += coinScore;
            AudioSource.PlayClipAtPoint(sound, gameObject.transform.position);
        }
    }
}
