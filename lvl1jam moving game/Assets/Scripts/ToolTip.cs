using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{

    [SerializeField]
    private GameObject tooltipPrefab;

    private GameObject tooltipGo;

    [TextArea(3, 5)]
    public string Tip;
    public string Header;
    public string Subtext;
    // Update is called once per frame
    void Update()
    {

    }
    void Start()
    {
        tooltipGo = Instantiate(tooltipPrefab, this.transform);
        tooltipGo.SetActive(false);
    }


}
