using UnityEngine;
public class MonsterMov : MonoBehaviour
{
    [SerializeField] private float monsterSpeed;
    private Rigidbody2D _monsterRigidbody2D;
    private BoxCollider2D _monsterBoxCollider2D;
    private CapsuleCollider2D _monsterCapsuleCollider2D;
    private Transform _monsterTransform;
    void Start()
    {
        _monsterRigidbody2D = GetComponent<Rigidbody2D>();
        _monsterTransform = GetComponent<Transform>();
    }
    void Update()
    {
        MonsterMove();
    }

    void MonsterMove()
    {
        _monsterRigidbody2D.velocity = new Vector2(monsterSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other is CompositeCollider2D)
        {
            monsterSpeed *= -1;
            FaceFlip();
        }
    }

    void FaceFlip()
    {
        _monsterTransform.localScale = new Vector3(-Mathf.Sign(_monsterRigidbody2D.velocity.x), 1f, 1f);
    }
}
