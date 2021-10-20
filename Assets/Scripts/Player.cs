using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] ApplePooler pooler = null;
    [SerializeField] ParticleSystem pickUpParticles = null;
    [SerializeField] AudioSource audioSource = null;
    [SerializeField] AudioClip pickUpClip = null;
    [SerializeField] ScoreUpdater scoreUpdater = null;

    bool facingLeft;
    float xAxis;
    Animator anm;
    Rigidbody2D rg2D;

    private void Awake()
    {
        Time.timeScale = 1;

        anm = GetComponent<Animator>();
        rg2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0)) { return; }

        xAxis = Input.GetAxisRaw("Horizontal");
        anm.SetBool("IsWalking", Mathf.Abs(xAxis) > 0);

        CheckIfFlip();
    }

    private void FixedUpdate()
    {
        if (Mathf.Approximately(Time.timeScale, 0)) { return; }

        rg2D.MovePosition(transform.position + Vector3.right * xAxis * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Apple"))
        {
            pooler.EnqueueApple(collision.gameObject);
            pickUpParticles.Play();
            audioSource.PlayOneShot(pickUpClip);
            scoreUpdater.UpdateScore();
        }
    }

    private void CheckIfFlip()
    {
        if (xAxis > 0 && facingLeft)
        {
            FlipSprite();
        }
        else if (xAxis < 0 && !facingLeft)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        facingLeft = !facingLeft; 
        Vector3 flipScale = transform.localScale;
        flipScale.x *= -1;
        transform.localScale = flipScale;
    }
}
