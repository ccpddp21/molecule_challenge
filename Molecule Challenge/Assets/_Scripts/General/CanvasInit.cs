using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInit : MonoBehaviour
{
    [SerializeField] private Canvas m_targetCanvas;
    [SerializeField] private string? m_targetTag;
    [SerializeField] private Camera m_targetCamera;

    // Start is called before the first frame update
    void Start()
    {
        if (m_targetCamera == null)
        {
            m_targetTag = m_targetTag == null || m_targetTag == "" ? "MainCamera" : m_targetTag;

            GameObject go = GameObject.FindGameObjectWithTag(m_targetTag);

            if (go != null)
            {
                m_targetCamera = go.GetComponent<Camera>();
            }
        }

        if (m_targetCanvas != null && m_targetCamera != null)
        {
            m_targetCanvas.worldCamera = m_targetCamera;
        }
    }
}
