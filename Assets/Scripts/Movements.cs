using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Movements : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private Vector2 deathImpact;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gun;
    [SerializeField] private AudioClip bulletSound;
    [SerializeField] private AudioClip deathSound;
    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private Animator _a;
    private CapsuleCollider2D _c;
    private float _gravityScaleAtStart;
    private BoxCollider2D _bc;
    private bool _isAlive = true;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _a = GetComponent<Animator>();
        _c = GetComponent<CapsuleCollider2D>();
        _bc = GetComponent<BoxCollider2D>();
        _gravityScaleAtStart = _rb.gravityScale;
    }

    void OnFire()
    {
        if (!_isAlive)
        {
            return;
        }
        Instantiate(bullet, gun.position, transform.rotation);
        AudioSource.PlayClipAtPoint(bulletSound, gameObject.transform.position);
    }
    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(1);
        FindObjectOfType<Session>().HealthLost();
    }
    void Die()
    {
        if (_c.IsTouchingLayers(LayerMask.GetMask("Monster", "Hazard")) || _bc.IsTouchingLayers(LayerMask.GetMask("Monster", "Hazard")))
        {
            _isAlive = false;
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, 1f);
            _a.SetTrigger("isDying");
            _rb.velocity = deathImpact;
            StartCoroutine(WaitForFunction());
        }
    }
    void Update()
    {
        if(!_isAlive){return;}
        Run();
        SlipSprites();
        ClimbLadder();
        Die();
    }

    void SlipSprites()
    {
        if(!_isAlive){return;}
        bool isMov = Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon;
        if (isMov)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rb.velocity.x),1f);
        }
    }

    void OnMove(InputValue value)
    {
        if(!_isAlive){return;}
        _moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!_isAlive){return;}
        if (_bc.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            _rb.velocity += new Vector2(0f,jumpSpeed);
        }
    }

    void ClimbLadder()
    {
        if(!_isAlive){return;}
        if (_c.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            _rb.gravityScale = 0f;
            Vector2 vt = new Vector2(_rb.velocity.x, _moveInput.y * climbSpeed);
            _rb.velocity = vt;
            _a.SetBool("isClimbing", true);
            if (Mathf.Abs(_rb.velocity.y) > Mathf.Epsilon)
            {
                _a.speed = 1f;
            }
            else
            {
                _a.speed = 0f;
            }
        }
        else
        {
            _rb.gravityScale = _gravityScaleAtStart;
            _a.SetBool("isClimbing", false);
            _a.speed = 1f;
        }
    }

    void Run()
    {
        if(!_isAlive){return;}
        Vector2 vt = new Vector2(_moveInput.x * speed, _rb.velocity.y);
        _rb.velocity = vt;
        _a.SetBool("isRunning", Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon);
    }
}
