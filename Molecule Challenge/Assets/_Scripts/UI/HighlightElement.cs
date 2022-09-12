using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightElement : MonoBehaviour
{
    [SerializeField] private Toggle m_toggle;
    [SerializeField] private GameObject m_highlightObj;

    // Start is called before the first frame update
    void Start()
    {
        if (m_toggle != null)
        {
            m_toggle.onValueChanged.AddListener(delegate { OnValueChanged(m_toggle); });
        }
    }

    private void OnValueChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {

        }
    }
}
