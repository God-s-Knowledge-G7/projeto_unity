using UnityEngine;

public class DogEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private bool isWalking;
    public bool facingRigth;
    public bool previousFacingRigth;

    private Transform target;

    private float dogEnemySpeed = 0.5f;
    private float currentSpeed;

    private float horizontalForce;
    private float verticalForce;

    private float walkTimer;

    public bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GetComponent<Player_Base_Controller>().transform;

        currentSpeed = dogEnemySpeed;

    }
    void Update()
    {
        if (target.position.x < this.transform.position.x) 
        {
            facingRigth = true;
        } else
        {
            facingRigth = false;
        }

        if (facingRigth && !previousFacingRigth)
        {
            this.transform.Rotate(0, 180, 0);
            previousFacingRigth = true;
        }

        if (!facingRigth && previousFacingRigth)
        {
            this.transform.Rotate(0, -180, 0);
            previousFacingRigth = false;
        }

        walkTimer += Time.deltaTime;

        if (horizontalForce == 0 && verticalForce == 0)
        {
            isWalking = false;
        }
        else
        {
             isWalking = true;
        }

        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Vector3 targetDistance = target.position - this.transform.position;

            horizontalForce = target.position.x / Mathf.Abs(targetDistance.x);

            if (walkTimer >= Random.Range(1f, 2f))
            {
                verticalForce = Random.Range(-1, 2);

                walkTimer = 0;
            }
        }
    }

    void UpdateAnimator()
    {
        animator.SetBool("isWalking", isWalking);
    }
}
