using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementToggle : MonoBehaviour
{
    [SerializeField] private Toggle m_toggle;
    [SerializeField] private Color m_highlightColor;
    [SerializeField] private GameObject m_highlightGameObject;
    [SerializeField] private TextMeshProUGUI m_label;

    public ElementManager.ElementInfo ElementInfo { get; set; }
    public ElementDispenser.DispenserNumber AssociatedDispenser { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (m_toggle != null)
        {
            m_toggle.onValueChanged.AddListener(delegate { OnValueChanged(m_toggle); });
        }
    }

    public void SetSymbolText(string symbol)
    {
        m_label.SetText(symbol);
    }

    public void SetHighlightGameObject(GameObject highlightObj)
    {
        m_highlightGameObject = highlightObj;
    }

    public void SetHighlightColor(Color color)
    {
        m_highlightColor = color;
    }

    private void OnValueChanged(Toggle toggle)
    {
        m_highlightGameObject.SetActive(toggle.isOn);
        if (toggle.isOn)
        {
            m_highlightGameObject.GetComponent<Image>().color = m_highlightColor;
            ElementManager.ElementSelectedEvent.Invoke(ElementInfo, AssociatedDispenser);
        }
        else
        {
            ElementManager.ElementSelectedEvent.Invoke(null, AssociatedDispenser);
        }
    }
}
