using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BondGenerator : MonoBehaviour
{
    public enum BondType
    {
        NA = 0,
        Ionic = 1,
        Convalent = 2
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
    [SerializeField] private Image m_ionicButtonBorder;
    [SerializeField] private Image m_covalentButtonBorder;

    private bool elemOneSelected = false;
    private ElementManager.ElementInfo? elemOneInfo;
    private bool elemTwoSelected = false;
    private ElementManager.ElementInfo? elemTwoInfo;

    private List<BondInfo> m_foundInfoList = new List<BondInfo>();

    private BondType m_expectedBondTypeAnswer;

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
        m_foundInfoList.Clear();
        
        if (elemOneInfo.Value.Name == elemTwoInfo.Value.Name)
        {
            m_foundInfoList.AddRange(m_bondInfoList.FindAll(bondInfo =>
            {
                return bondInfo.Elements.Contains(elemOneInfo.Value.Name) && bondInfo.Elements.Contains(elemTwoInfo.Value.Name);
            }));

            m_foundInfoList.RemoveAll(bondInfo =>
            {
                return bondInfo.Elements[0] != elemOneInfo.Value.Name || bondInfo.Elements[1] != elemTwoInfo.Value.Name;
            });
        }
        else
        {
            m_foundInfoList.AddRange(m_bondInfoList.FindAll(bondInfo =>
            {
                return bondInfo.Elements.Contains(elemOneInfo.Value.Name) && bondInfo.Elements.Contains(elemTwoInfo.Value.Name);
            }));
        }

        string fullName;
        foreach (BondInfo info in m_foundInfoList)
        {
            fullName = "<size=6>" + info.Name + "</size>\n";
            m_possibleBondsText.SetText(m_possibleBondsText.text + (fullName + "<cspace=1>" + info.Formula + "</cspace>\n"));
        }

        int foundCount = m_foundInfoList.Count;

        UpdateInstructionText(AVAILABLE_COMPOUNDS + foundCount.ToString());

        if (foundCount > 0)
        {
            m_expectedBondTypeAnswer = m_foundInfoList[0].Type;
            m_possibleBondPanel.SetActive(true);
            m_bondQuestionPanel.SetActive(true);
        }
    }

    private void HidePossibleBonds()
    {
        m_expectedBondTypeAnswer = BondType.NA;
        m_possibleBondPanel.SetActive(false);
        m_possibleBondsText.SetText("");
        m_bondQuestionPanel.SetActive(false);
        UpdateInstructionText(SELECT_ELEMENTS);
        m_ionicButtonBorder.gameObject.SetActive(false);
        m_ionicButtonBorder.color = Color.white;
        m_covalentButtonBorder.gameObject.SetActive(false);
        m_covalentButtonBorder.color = Color.white;
    }

    private void UpdateInstructionText(string text)
    {
        m_instructionText.SetText(text);
    }

    public void CheckBondAnswer(int answer)
    {
        if (m_expectedBondTypeAnswer == BondType.Ionic)
        {
            m_ionicButtonBorder.gameObject.SetActive(true);
            m_ionicButtonBorder.color = Color.green;
        }
        else if (m_expectedBondTypeAnswer == BondType.Convalent)
        {
            m_covalentButtonBorder.gameObject.SetActive(true);
            m_covalentButtonBorder.color = Color.green;
        }

        if (answer != (int) m_expectedBondTypeAnswer)
        {
            if (answer == (int)BondType.Ionic)
            {
                m_ionicButtonBorder.gameObject.SetActive(true);
                m_ionicButtonBorder.color = Color.red;
            }
            else if (answer == (int)BondType.Convalent)
            {
                m_covalentButtonBorder.gameObject.SetActive(true);
                m_covalentButtonBorder.color = Color.red;
            }
        }
    }
}
