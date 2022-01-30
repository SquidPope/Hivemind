using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    [SerializeField] Text tooltipText;

    //check for mouseing over a beeButton, get text based on type

    void Update()
    {
        //ToDo: account for the canvas scaling to fit the screen size.
        transform.position = Input.mousePosition;
    }
}
