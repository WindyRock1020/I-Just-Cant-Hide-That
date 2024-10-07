using UnityEngine;
using System.Collections.Generic;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // �l�u���w�s��
    public float bulletSpeed = 20f; // �l�u�t��
    public GameObject targetObject;
    public float pushSpeed = 10f;
    private Queue<GameObject> bullets = new Queue<GameObject>(); // �l�u���C
    private int maxBullets = 10; // �̤j�l�u�ƶq
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
        if (Input.GetKeyDown(KeyCode.Mouse0)) // ���U�ƹ�����
        {
            // �]�m firePoint ���ƹ��I����m
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
            // �R�����ª��l�u
            GameObject oldestBullet = bullets.Dequeue();
            Destroy(oldestBullet);
        }
        // �p��l�u����V
        Vector3 direction = Vector3.back; // �T�w��V�� -Z �b

        // �Ыؤl�u
        GameObject bullet = Instantiate(bulletPrefab, firePoint, Quaternion.Euler(0,-90,0));

        bullets.Enqueue(bullet);

        // �]�m�l�u���t�שM��V
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        // ����ƹ����@�ɮy��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.forward, 0); // Z �b����
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
