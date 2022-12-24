using UnityEngine;

public class MainConfig : MonoBehaviour
{
    [SerializeField] private MainConfigSO mainConfigSO;

    public MainConfigSO SO => mainConfigSO;
}
