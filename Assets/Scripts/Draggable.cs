using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool dragging = false;
    public int cardIndex;
    private bool isOverGrid = false;
    private GameObject lastGridCell;
    public bool mode;
    private Vector3 offset;
    private Vector3 startPos;
    GameManager gameManager;

    void Start()
    {
        startPos = transform.position;
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }

        // if (Input.GetMouseButtonDown(1) && Time.time > lastClickTime + clickCooldown)
        // {
        //     Debug.Log("Toggle");
        //     gameManager.toggleMode();
        //     lastClickTime = Time.time;
        // }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
        transform.position = startPos;
        if (gameManager != null && isOverGrid)
        {
            gameManager.commit();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grid"))
        {
            isOverGrid = true;
            lastGridCell = collision.gameObject;

            if (gameManager != null)
            {
                gameManager.PreviewCard(cardIndex);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grid"))
        {
            isOverGrid = false;
            lastGridCell = null;
            gameManager.ShowStatus();
        }
    }
}
