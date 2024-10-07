using UnityEngine;
using System.Collections.Generic;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // 子彈的預製件
    public float bulletSpeed = 20f; // 子彈速度
    public GameObject targetObject;
    public float pushSpeed = 10f;
    private Queue<GameObject> bullets = new Queue<GameObject>(); // 子彈隊列
    private int maxBullets = 10; // 最大子彈數量
    public Animator animator;
    private Rigidbody rb;
    private Collider playerCollider;
    public bool isGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }
    void Update()
    {
        Vector3 mPosition = GetMouseWorldPosition();
        if (Input.GetKeyDown(KeyCode.Mouse0)) // 按下滑鼠左鍵
        {
            // 設置 firePoint 為滑鼠點擊位置
            Vector3 firePoint = new Vector3(mPosition.x, mPosition.y, Camera.main.transform.position.z);

            if (insideCollider(mPosition, targetObject.GetComponent<Collider>()))
            {
                push(targetObject, mPosition);
            }
            else
            {
                Shoot(firePoint);
            }
        }
    }

    void Shoot(Vector3 firePoint)
    {
        if (bullets.Count >= maxBullets)
        {
            // 刪除最舊的子彈
            GameObject oldestBullet = bullets.Dequeue();
            Destroy(oldestBullet);
        }
        // 計算子彈的方向
        Vector3 direction = Vector3.back; // 固定方向為 -Z 軸

        // 創建子彈
        GameObject bullet = Instantiate(bulletPrefab, firePoint, Quaternion.Euler(0,-90,0));

        bullets.Enqueue(bullet);

        // 設置子彈的速度和方向
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        // 獲取滑鼠的世界座標
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.forward, 0); // Z 軸平面
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);
        }

        return Vector3.zero;
    }

    bool insideCollider(Vector3 mPosition, Collider collider)
    {
        return collider.bounds.Contains(mPosition);
    }

    void push(GameObject obj, Vector3 position)
    {
        //Vector3 pushDirection = (obj.transform.position - position).normalized;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if ((rb != null) || isGround == true)
        {
            rb.AddForce(Vector3.up * pushSpeed, ForceMode.Impulse);
            animator.SetBool("jumping", true);

        }
    }
    void SwitchAnim()
    {
        animator.SetBool("danceing", false);
        if (rb.velocity.y <= 0)
        {
            animator.SetBool("jumping", false);
            animator.SetBool("danceing", true);
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
