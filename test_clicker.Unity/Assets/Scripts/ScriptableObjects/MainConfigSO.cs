using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MainConfig", menuName = "Main Config", order = 51)]
public class MainConfigSO : ScriptableObject
{
    [SerializeField] private BusinessConfigSO[] businessData;

    public IEnumerable<BusinessConfigSO> BusinessConfig => businessData;
}
