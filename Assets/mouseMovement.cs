using UnityEngine;

public class mouseMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    public float detectionDistance; // 偵測距離
    public float jumpForce = 5f; // 跳躍力
    public float bounceSpeed = 5f; // 回彈力
    public GameObject character;

    public bool isGround;
    private Rigidbody rb;
    private Collider playerCollider;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            float t = 1 / Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime * t);
            animator.SetBool("jumping", true);
        }
        SwitchAnim();
    }
    void FixedUpdate()
    {
        DetectDistance();
        // 按下空白鍵時跳躍
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            if (isGround == true)
            {
                Jump();
            }
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        // 獲取滑鼠的世界座標
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, 0);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);
        }

        return Vector3.zero;
    }
    void DetectDistance()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 objectCenter = character.transform.position;

        if (mousePosition.x < objectCenter.x && Mathf.Abs(mousePosition.x - objectCenter.x) < detectionDistance)
        {
            MoveRight();
        }
        if (mousePosition.x > objectCenter.x && Mathf.Abs(mousePosition.x - objectCenter.x) < detectionDistance)
        {
            MoveLeft();
        }
    }
    void MoveLeft()
    {
        animator.SetBool("IsRight", false);
        animator.SetBool("IsLeft", true);
        rb.MovePosition(transform.position + Vector3.left * moveSpeed * Time.deltaTime);
    }

    void MoveRight()
    {
        animator.SetBool("IsRight", true);
        animator.SetBool("IsLeft", false);
        rb.MovePosition(transform.position + Vector3.right * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // 清除垂直方向的速度
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        // 碰撞發生時，應用回彈力
        Vector3 collisionNormal = (transform.position - other.transform.position).normalized;
        Vector3 bounceDirection = Vector3.Reflect(rb.velocity.normalized, collisionNormal);
        rb.AddForce(bounceDirection * bounceSpeed, ForceMode.Impulse);
    }
    void SwitchAnim()
    {
        animator.SetBool("danceing", false);
        if (rb.velocity.y <= 0)
        {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", true);
            if (isGround == true)
            {
                animator.SetBool("falling", false);
                animator.SetBool("danceing", true);
            }
        }
    }
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }
}