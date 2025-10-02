using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Slider slider;
    public Image damageImage;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public float flashSpeed = 5f;
    private bool damaged;

    void Update()
    {
        if (damageImage == null) return;
        damageImage.color = damaged ? flashColour : Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        damaged = false;
    }

    public void OnHealthChanged(int current, int max)
    {
        if (slider != null)
        {
            slider.maxValue = max;
            slider.value = current;
        }
        damaged = true;
    }
}