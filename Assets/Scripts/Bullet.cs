using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Rigidbody2D _bulletRigidbody2D;
    private Movements _player;
    private float _lastSpeed;
    [SerializeField] private AudioClip monsterDeathSound;
    [SerializeField] private float distanceRate;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(monsterDeathSound, Camera.main.transform.position + new Vector3(_lastSpeed * distanceRate, 0f, 0f), 1f);
        }
        Destroy(gameObject);
    }

    void Start()
    {
        _bulletRigidbody2D = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<Movements>();
        _lastSpeed = _player.transform.localScale.x;
    }
    void Update()
    {
        _bulletRigidbody2D.velocity = new Vector2(bulletSpeed * _lastSpeed, 0f);
    }
}
