using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipCanvas : MonoBehaviour
{
    private static Vector2 offset = new Vector2(2.3f, -1.2f);
    private RectTransform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf = (RectTransform)transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse pos
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Translate to transform coords
        Vector3 mouseLocalPos = new Vector3(mousePos.x, mousePos.y, 0);
        //Vector3 mouseLocalPos = GetComponentInParent<Transform>().InverseTransformPoint(mousePos.x, mousePos.y, 0.0f);

        // Move there
        tf.position = mousePos + offset;
        //tf.localPosition = new Vector2(mousePos.x, mousePos.y);
        //transform.L
    }
}
