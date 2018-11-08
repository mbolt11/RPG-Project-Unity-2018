using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float StartingHealth = 100f;
    public Slider HealthSlider;
    public GameObject FillArea;
    public Color FullHealthColor = Color.green;
    public Color ZeroHealthColor = Color.red;
    public bool Dead;

    private float CurrentHealth;
    private Image FillImage;

    private void OnEnable()
    {
        CurrentHealth = StartingHealth;
        Dead = false;

        FillImage = FillArea.GetComponentInChildren<Image>();

        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
        // Adjust the current health
        CurrentHealth -= amount;

        //Update UI based on new health
        SetHealthUI();

        //Check if player is dead
        if (CurrentHealth <= 0f && !Dead)
        {
            Dead = true;
        }
    }


    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        HealthSlider.value = CurrentHealth;

        FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, CurrentHealth / StartingHealth);
    }
}