using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ���ʳt��
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    void Update()
    {
        // ���������J (A, D, ���b�Y, �k�b�Y)
        float moveDirection = Input.GetAxis("Horizontal");

        // �p�Ⲿ�ʶq
        Vector3 move = new Vector3(moveDirection * moveSpeed * Time.deltaTime, 0, 0);

        // �ˬd�O�_���\����
        if ((moveDirection < 0 && canMoveLeft) || (moveDirection > 0 && canMoveRight))
        {
            // ���β��ʶq�쨤��
            transform.Translate(move);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �ˬd�I������
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            canMoveLeft = false;
        }
        else if (collision.gameObject.CompareTag("RightWall"))
        {
            canMoveRight = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // �����}�I���ɫ�_����
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            canMoveLeft = true;
        }
        else if (collision.gameObject.CompareTag("RightWall"))
        {
            canMoveRight = true;
        }
    }
}
