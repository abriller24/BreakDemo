using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerManaBar : Widget
{
    [SerializeField] private Image manaBarImage;
    [SerializeField] private TextMeshProUGUI valueText;
    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        AbilitiySystemComponent ownerASC = newOwner.GetComponent<AbilitiySystemComponent>();
        if (ownerASC)
        {
            ownerASC.onManaUpdated += UpdateMana;
            UpdateMana(ownerASC.Mana, 0, ownerASC.MaxMana);
        }
    }

    private void UpdateMana(float newMana, float delta, float maxMana)
    {
        manaBarImage.fillAmount = newMana / maxMana;
        valueText.text = $"{newMana:F0}/{maxMana:F0}";
    }
}
