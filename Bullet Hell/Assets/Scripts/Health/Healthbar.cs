using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] protected Slider slider;
    [SerializeField] private Color low;
    [SerializeField] private Color high;

    public virtual void Start()
    {
        slider.gameObject.SetActive(true);
    }

    public virtual void SetHealth(float health, float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = health;


        Color lerped = Color.Lerp(low, high, slider.normalizedValue);
        Vector3 colorVector = new Vector3(lerped.r, lerped.g, lerped.b);
        Color lerpedNormalized = new Color(
            Mathf.Min(colorVector.normalized.x * 2, 1), 
            Mathf.Min(colorVector.normalized.y * 2, 1), 
            Mathf.Min(colorVector.normalized.z * 2, 1), 
            1
            );
        slider.fillRect.GetComponentInChildren<Image>().color = lerpedNormalized;
    }
}
