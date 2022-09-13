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

    private void OnDestroy()
    {
        if (m_toggle != null)
        {
            m_toggle.onValueChanged.RemoveAllListeners();
        }
    }

    /// <summary>
    /// Set symbol text on label
    /// </summary>
    /// <param name="symbol"></param>
    public void SetSymbolText(string symbol)
    {
        m_label.SetText(symbol);
    }

    /// <summary>
    /// Set reference to highlight gameObject
    /// </summary>
    /// <param name="highlightObj"></param>
    public void SetHighlightGameObject(GameObject highlightObj)
    {
        m_highlightGameObject = highlightObj;
    }

    /// <summary>
    /// Set the highlight color
    /// </summary>
    /// <param name="color"></param>
    public void SetHighlightColor(Color color)
    {
        m_highlightColor = color;
    }

    /// <summary>
    /// Invokes the ElementSelectedEvent on value change
    /// </summary>
    /// <param name="toggle"></param>
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
