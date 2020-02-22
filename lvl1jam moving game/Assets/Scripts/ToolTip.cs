using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{

    [SerializeField]
    private GameObject tooltipPrefab;

    private GameObject tooltipGo;
    private bool shouldShowTooltip = false;

    [TextArea(3, 5)]
    public string Tip;
    public string Header;
    public string Subtext;
    // Update is called once per frame
    void Update()
    {
        tooltipGo.SetActive(shouldShowTooltip);
    }
    void Start()
    {
        tooltipGo = Instantiate(tooltipPrefab, this.transform);
        tooltipGo.SetActive(false);
        TextMeshProUGUI[] texts = tooltipGo.GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = Tip;
        texts[1].text = Header;
    }
    public void ShowTooltip()
    {
        shouldShowTooltip = true;
    }
    public void HideTooltip()
    {
        shouldShowTooltip = false;
    }

}
