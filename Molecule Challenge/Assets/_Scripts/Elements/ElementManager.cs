using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ElementManager : MonoBehaviour
{
    public static ElementManager instance;

    public enum ElementOption
    {
        Hydrogen,
        Carbon,
        Nitrogen,
        Oxygen,
        Fluorine,
        Neon,
        Sodium,
        Magnesium,
        Aluminum,
        Sulfur,
        Chlorine
    }

    [System.Serializable]
    public struct ElementInfo
    {
        public ElementOption Name;
        public string Symbol;
        public GameObject HighlightObject;
        public int Electrons;
    }
    [Header("Element Dispensers")]
    [SerializeField] private ElementDispenser m_dispenserOne;
    public ElementDispenser DispenserOne { get { return m_dispenserOne; } }
    [SerializeField] private ElementDispenser m_dispenserTwo;
    public ElementDispenser DispenserTwo { get { return m_dispenserTwo; } }

    [Header("UI")]
    [SerializeField] private GameObject m_togglePrefab;
    [SerializeField] private Color m_colorOne;
    [SerializeField] private GameObject m_elementPanelOne;
    [SerializeField] private Color m_colorTwo;
    [SerializeField] private GameObject m_elementPanelTwo;

    [Header("Element Info List")]
    [SerializeField] private List<ElementInfo> Elements = new List<ElementInfo>();
    public Dictionary<string, ElementInfo> ElementDict = new Dictionary<string, ElementInfo>();

    private GameObject m_prefab;
    private List<GameObject> m_elementToggles = new List<GameObject>();

    public static UnityEvent<ElementInfo?, ElementDispenser.DispenserNumber> ElementSelectedEvent = new UnityEvent<ElementInfo?, ElementDispenser.DispenserNumber>();

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
            ElementDict.Add(element.Name.ToString(), element);
        }
    }

    private void CreateElementToggles()
    {
        Toggle toggle;
        ElementToggle he;
        foreach (ElementInfo element in Elements)
        {
            // Create element toggle for Element Panel One
            m_prefab = Instantiate(m_togglePrefab, m_elementPanelOne.transform);

            toggle = m_prefab.GetComponent<Toggle>();
            toggle.group = m_elementPanelOne.GetComponent<ToggleGroup>();
            toggle.graphic.color = m_colorOne;

            he = m_prefab.GetComponent<ElementToggle>();
            if (he != null)
            {
                he.ElementInfo = element;
                he.SetSymbolText(element.Symbol);
                he.SetHighlightGameObject(element.HighlightObject);
                he.SetHighlightColor(m_colorOne);
                he.AssociatedDispenser = ElementDispenser.DispenserNumber.One;
            }

            m_elementToggles.Add(m_prefab);

            // Create element toggle for Element Panel Two
            m_prefab = Instantiate(m_togglePrefab, m_elementPanelTwo.transform);

            toggle = m_prefab.GetComponent<Toggle>();
            toggle.group = m_elementPanelTwo.GetComponent<ToggleGroup>();
            toggle.graphic.color = m_colorTwo;

            he = m_prefab.GetComponent<ElementToggle>();
            if (he != null)
            {
                he.ElementInfo = element;
                he.SetSymbolText(element.Symbol);
                he.SetHighlightGameObject(element.HighlightObject);
                he.SetHighlightColor(m_colorTwo);
                he.AssociatedDispenser = ElementDispenser.DispenserNumber.Two;
            }

            m_elementToggles.Add(m_prefab);
        }
    }
}
