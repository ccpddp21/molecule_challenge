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

    [System.Serializable]
    public struct PoolItem
    {
        public ElementManager.ElementOption Name;
        public GameObject ElementObject;
    }

    [Header("Info")]
    [SerializeField] private DispenserNumber dispenserNumber;

    [SerializeField] private Transform m_spawnPointTransform;
    public Transform SpawnPointTransform { get { return m_spawnPointTransform; } }

    [SerializeField] private GameObject m_activeElementObject;
    public GameObject ActiveElementObject { get { return m_activeElementObject; } }

    [SerializeField] private ElementManager.ElementInfo activeElementInfo;
    public ElementManager.ElementInfo ActiveElementInfo { get { return activeElementInfo; } }

    [Header("UI")]
    [SerializeField] private Canvas m_elementNameCanvas;
    [SerializeField] private TextMeshProUGUI m_elementNameText;

    [Header("Pool")]
    [SerializeField] List<PoolItem> m_poolItems = new List<PoolItem>();
    private Dictionary<ElementManager.ElementOption?, PoolItem> m_poolItemDic = new Dictionary<ElementManager.ElementOption?, PoolItem>();

    // Start is called before the first frame update
    void Start()
    {
        ElementManager.ElementSelectedEvent.AddListener(OnElementSelected);

        LoadPoolItemDict();
    }

    private void OnDestroy()
    {
        ElementManager.ElementSelectedEvent.RemoveListener(OnElementSelected);
    }

    /// <summary>
    /// Populate the dictionary
    /// </summary>
    private void LoadPoolItemDict()
    {
        foreach (PoolItem poolItem in m_poolItems)
        {
            m_poolItemDic.Add(poolItem.Name, poolItem);
        }
    }

    /// <summary>
    /// Enables the corresponding Element gameObject based on the triggered Element toggle
    /// </summary>
    /// <param name="elementInfo"></param>
    /// <param name="number"></param>
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
                ElementManager.ElementOption? option = elementInfo?.Name;
                m_activeElementObject = m_poolItemDic[option].ElementObject;
            }

            m_activeElementObject.SetActive(true);

            m_activeElementObject.transform.position = m_spawnPointTransform.position;

            m_elementNameCanvas.enabled = true;
            m_elementNameText.SetText(elementInfo.Value.Name.ToString());
        }
    }

    /// <summary>
    /// Reset the Dispenser attributes
    /// </summary>
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
