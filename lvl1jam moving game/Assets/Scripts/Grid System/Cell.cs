using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool inUse;
    SpriteRenderer renderer;
    Color startColor;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    public void SetColor(Color c){
        renderer.color = c;
    }
    public void SetAlpha(float a){
        Color c = renderer.color;
        c.a = a;
        renderer.color = c;
    }
    public void ResetColor(){
        renderer.color = startColor;
    }
}
