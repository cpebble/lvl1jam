using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurserSystem : MonoBehaviour
{
    [SerializeField]
    LayerMask gridLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit;
        // hit = Physics2D.OverlapPoint(mousePos,gridLayer,-100f,100f);
    }
}
