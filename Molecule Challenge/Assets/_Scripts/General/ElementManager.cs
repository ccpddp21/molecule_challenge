using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private List<ElementInfo> Elements = new List<ElementInfo>();
    public Dictionary<string, ElementInfo> ElementDict = new Dictionary<string, ElementInfo>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (ElementInfo element in Elements)
        {
            ElementDict.Add(element.Name, element);
        }
    }
}
