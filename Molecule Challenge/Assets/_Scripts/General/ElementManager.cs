using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementManager : MonoBehaviour
{
    public static ElementManager instance;

    [System.Serializable]
    public struct ElementInfo
    {
        public string Name;
        public string Symbol;
        public GameObject HighlightObject;
        public GameObject ElementPrefab;
        public int Electrons;
    }

    [Header("UI")]
    [SerializeField] private GameObject m_togglePrefab;
    [SerializeField] private Color m_colorOne;
    [SerializeField] private GameObject m_elementPanelOne;
    [SerializeField] private Color m_colorTwo;
    [SerializeField] private GameObject m_elementPanelTwo;

    [Header("Element List")]
    [SerializeField] private List<ElementInfo> Elements = new List<ElementInfo>();
    public Dictionary<string, ElementInfo> ElementDict = new Dictionary<string, ElementInfo>();

    private GameObject m_prefab;
    private List<GameObject> m_elementToggles = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateElementDictionary();
        CreateElementToggles();
    }

    private void CreateElementDictionary()
    {
        foreach (ElementInfo element in Elements)
        {
            ElementDict.Add(element.Name, element);
        }
    }

    private void CreateElementToggles()
    {
        ElementToggle he;
        foreach (ElementInfo element in Elements)
        {
            // Create element toggle for Element Panel One
            m_prefab = Instantiate(m_togglePrefab, m_elementPanelOne.transform);

            m_prefab.GetComponent<Toggle>().group = m_elementPanelOne.GetComponent<ToggleGroup>();

            he = m_prefab.GetComponent<ElementToggle>();
            if (he != null)
            {
                he.SetSymbolText(element.Symbol);
                he.SetHighlightGameObject(element.HighlightObject);
                he.SetHighlightColor(m_colorOne);
            }

            m_elementToggles.Add(m_prefab);

            // Create element toggle for Element Panel Two
            m_prefab = Instantiate(m_togglePrefab, m_elementPanelTwo.transform);

            m_prefab.GetComponent<Toggle>().group = m_elementPanelTwo.GetComponent<ToggleGroup>();

            he = m_prefab.GetComponent<ElementToggle>();
            if (he != null)
            {
                he.SetSymbolText(element.Symbol);
                he.SetHighlightGameObject(element.HighlightObject);
                he.SetHighlightColor(m_colorTwo);
            }

            m_elementToggles.Add(m_prefab);
        }
    }
}
