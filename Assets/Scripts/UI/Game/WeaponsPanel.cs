using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponsPanel : MonoBehaviour
{
    public Text m_AmmoLabelText;

    public void UpdateAmmoCount(int a_Current, int a_Max)
    {
        m_AmmoLabelText.text = string.Format("{0} / {1}", a_Current, a_Max);
    }
}
