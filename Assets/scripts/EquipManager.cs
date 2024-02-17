using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "New Item")]
public class EquipManager : ScriptableObject
{
    public string ItemName;

    public int iAtkStat = 0;

    public int iDefStat = 0;
}
