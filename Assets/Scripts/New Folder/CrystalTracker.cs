using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class CrystalTracker : MonoBehaviour
{
    private int m_crystalCount = 0;
    private int m_maxCrystalCount = 15;

    public TMP_Text crystalCounterText;

    private void Start()
    {
        crystalCounterText.text = "x" + CrystalCount;
    }

    public int CrystalCount
    {
        get
        {
            return m_crystalCount;
        }
       
        set
        {
            m_crystalCount = value;
            crystalCounterText.text = "x" + CrystalCount;
        }
    }

    public int MaxCrystalCount
    {
        get
        {
            return m_maxCrystalCount;
        }

        set
        {
            m_maxCrystalCount = value;
        }
    }
}
