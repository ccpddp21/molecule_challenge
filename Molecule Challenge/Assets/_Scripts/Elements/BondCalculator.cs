using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondCalculator : MonoBehaviour
{
    private bool elemOneSelected = false;
    private ElementManager.ElementInfo? elemOneInfo;
    private bool elemTwoSelected = false;
    private ElementManager.ElementInfo? elemTwoInfo;

    // Start is called before the first frame update
    void Start()
    {
        ElementManager.ElementSelectedEvent.AddListener(OnElementSelected);
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
    }

    private void ResetActive(ElementDispenser.DispenserNumber dispenserNumber)
    {
        throw new NotImplementedException();
    }

    private void ShowPossibleBonds()
    {
        Debug.Log(elemOneInfo.Value.Name + " and " + elemTwoInfo.Value.Name + " can make stuff!!");
    }
}
