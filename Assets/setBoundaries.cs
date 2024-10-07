using UnityEngine;

public class SetupBoundaries : MonoBehaviour
{
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject topWall;
    public GameObject bottomWall;
    public float boundaryThickness = 1f;
    public float boundaryLength = 30f;
    public float boundaryWidth = 15f;
    public float boundaryHeight = 10f;

    void Start()
    {
        // 設置左邊界
        leftWall.transform.position = new Vector3(-boundaryLength / 2 - boundaryThickness / 2, boundaryHeight / 2, 0);
        leftWall.transform.localScale = new Vector3(boundaryThickness, boundaryHeight, boundaryWidth);

        // 設置右邊界
        rightWall.transform.position = new Vector3(boundaryLength / 2 + boundaryThickness / 2, boundaryHeight / 2, 0);
        rightWall.transform.localScale = new Vector3(boundaryThickness, boundaryHeight, boundaryWidth);

        // 設置上邊界
        topWall.transform.position = new Vector3(0, boundaryHeight / 2, boundaryWidth / 2 + boundaryThickness / 2);
        topWall.transform.localScale = new Vector3(boundaryLength, boundaryHeight, boundaryThickness);

        // 設置下邊界
        bottomWall.transform.position = new Vector3(0, boundaryHeight / 2, -boundaryWidth / 2 - boundaryThickness / 2);
        bottomWall.transform.localScale = new Vector3(boundaryLength, boundaryHeight, boundaryThickness);
    }
}
