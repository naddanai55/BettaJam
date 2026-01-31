using UnityEngine;
using System; // Required for Action

public class ClickCallback : MonoBehaviour
{
    // 1. Define a variable to hold the function
    public Action OnClickAction;

    // 2. Detect the click (Requires a Collider on the GameObject)
    private void OnMouseDown()
    {
        // 3. Execute the function if it exists
        OnClickAction?.Invoke();
    }
}