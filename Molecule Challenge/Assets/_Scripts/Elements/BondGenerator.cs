using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BondGenerator : MonoBehaviour
{
    public enum BondType
    {
        NA,
        Ionic,
        Convalent
    }

    [System.Serializable]
    public struct BondInfo
    {
        public string Name;
        public List<ElementManager.ElementOption> Elements;
        public string Formula;
        public BondType Type;
    }

    const string SELECT_ELEMENTS = "Select <b><color=#048C00>Two</color></b> elements to bond";
    const string AVAILABLE_COMPOUNDS = "Available compounds: ";

    [Header("Info")]
    [SerializeField] private List<BondInfo> m_bondInfoList = new List<BondInfo>();

    [Header("UI")]
    [SerializeField] private GameObject m_possibleBondPanel;
    [SerializeField] private TextMeshProUGUI m_possibleBondsText;
    [SerializeField] private GameObject m_bondQuestionPanel;
    [SerializeField] private TextMeshProUGUI m_instructionText;

    private bool elemOneSelected = false;
    private ElementManager.ElementInfo? elemOneInfo;
    private bool elemTwoSelected = false;
    private ElementManager.ElementInfo? elemTwoInfo;

    private List<BondInfo> m_foundInfoList = new List<BondInfo>();

    // Start is called before the first frame update
    void Start()
    {
        ElementManager.ElementSelectedEvent.AddListener(OnElementSelected);

        HidePossibleBonds();

        UpdateInstructionText(SELECT_ELEMENTS);
    }

    private void OnDestroy()
    {
        ElementManager.ElementSelectedEvent.RemoveListener(OnElementSelected);
    }

    private void OnElementSelected(ElementManager.ElementInfo? elementInfo, ElementDispenser.DispenserNumber dispenserNumber)
    {
        if (elementInfo == null)
        {
            if (dispenserNumber == ElementDispenser.DispenserNumber.One)
            {
                elemOneSelected = false;
                elemOneInfo = elementInfo;
            }
            else if (dispenserNumber == ElementDispenser.DispenserNumber.Two)
            {
                elemTwoSelected = false;
                elemTwoInfo = elementInfo;
            }
        }
        else
        {
            switch (dispenserNumber)
            {
                case ElementDispenser.DispenserNumber.One:
                    elemOneSelected = true;
                    elemOneInfo = elementInfo;
                    break;
                case ElementDispenser.DispenserNumber.Two:
                    elemTwoSelected = true;
                    elemTwoInfo = elementInfo;
                    break;
                default:
                    elemOneSelected = elemTwoSelected = false;
                    elemOneInfo = null;
                    elemTwoInfo = null;
                    break;
            }
        }

        if (elemOneSelected && elemTwoSelected)
        {
            ShowPossibleBonds();
        }
        else
        {
            HidePossibleBonds();
        }
    }

    private void ShowPossibleBonds()
    {
        
        Debug.Log(elemOneInfo.Value.Name + " and " + elemTwoInfo.Value.Name + " can make");

        m_foundInfoList.Clear();
        m_foundInfoList.AddRange(m_bondInfoList.FindAll(bondInfo =>
            {
                return bondInfo.Elements.Contains(elemOneInfo.Value.Name) && bondInfo.Elements.Contains(elemTwoInfo.Value.Name);
            }));

        string fullName;
        foreach (BondInfo info in m_foundInfoList)
        {
            Debug.Log(info.Name);
            fullName = "<size=8>" + info.Name + "</size>\n";
            m_possibleBondsText.SetText(m_possibleBondsText.text + (fullName + info.Formula + "\n"));
        }

        int foundCount = m_foundInfoList.Count;

        UpdateInstructionText(AVAILABLE_COMPOUNDS + foundCount.ToString());

        if (foundCount > 0)
        {
            m_possibleBondPanel.SetActive(true);
            m_bondQuestionPanel.SetActive(true);
        }
    }

    private void HidePossibleBonds()
    {
        m_possibleBondPanel.SetActive(false);
        m_possibleBondsText.SetText("");
        m_bondQuestionPanel.SetActive(false);
        UpdateInstructionText(SELECT_ELEMENTS);
    }

    private void UpdateInstructionText(string text)
    {
        m_instructionText.SetText(text);
    }
}
