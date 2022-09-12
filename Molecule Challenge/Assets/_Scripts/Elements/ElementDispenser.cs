using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElementDispenser : MonoBehaviour
{
    public enum DispenserNumber
    {
        None = 0,
        One = 1,
        Two = 2
    }

    [Header("Info")]
    [SerializeField] private DispenserNumber dispenserNumber;

    [SerializeField] private Transform m_spawnPointTransform;
    public Transform SpawnPointTransform { get { return m_spawnPointTransform; } }

    [SerializeField] private GameObject m_activeElementObject;
    public GameObject ActiveElementObject { get { return m_activeElementObject; } }

    [Header("UI")]
    [SerializeField] private Canvas m_elementNameCanvas;
    [SerializeField] private TextMeshProUGUI m_elementNameText;

    // Start is called before the first frame update
    void Start()
    {
        ElementManager.ElementSelectedEvent.AddListener(OnElementSelected);
    }

    private void OnDestroy()
    {
        ElementManager.ElementSelectedEvent.RemoveListener(OnElementSelected);
    }

    private void OnElementSelected(ElementManager.ElementInfo? elementInfo, DispenserNumber number)
    {
        if (number == dispenserNumber)
        {
            ResetDispenser();

            if (elementInfo == null)
            {
                return;
            }

            if (elementInfo != null)
            {
                m_activeElementObject = elementInfo?.ElementObject;
            }

            m_activeElementObject.SetActive(true);

            m_activeElementObject.transform.position = m_spawnPointTransform.position;

            m_elementNameCanvas.enabled = true;
            m_elementNameText.SetText(elementInfo.Value.Name);
        }
    }

    private void ResetDispenser()
    {
        if (m_activeElementObject != null)
        {
            m_activeElementObject.SetActive(false);
            m_activeElementObject = null;
            m_elementNameCanvas.enabled = false;
            m_elementNameText.SetText("");
        }
    }
}
