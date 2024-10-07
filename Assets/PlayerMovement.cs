using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    void Update()
    {
        // 獲取水平輸入 (A, D, 左箭頭, 右箭頭)
        float moveDirection = Input.GetAxis("Horizontal");

        // 計算移動量
        Vector3 move = new Vector3(moveDirection * moveSpeed * Time.deltaTime, 0, 0);

        // 檢查是否允許移動
        if ((moveDirection < 0 && canMoveLeft) || (moveDirection > 0 && canMoveRight))
        {
            // 應用移動量到角色
            transform.Translate(move);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 檢查碰撞標籤
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
        // 當離開碰撞時恢復移動
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
