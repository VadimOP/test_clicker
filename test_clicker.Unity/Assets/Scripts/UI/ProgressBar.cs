using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider _slider;

    public void SetValue(float value)
    {
        if (_slider == null)
            _slider = transform.GetComponent<Slider>();
        _slider.value = value;
    }
}
