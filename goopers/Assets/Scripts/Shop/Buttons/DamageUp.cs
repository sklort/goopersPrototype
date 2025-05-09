using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameBoss gameBoss;

    private int timesBought = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void DamageUp()
    {
        if (gameBoss.playerMoney >= (50 * timesBought))
        {
            gameBoss.pistolDamage += 0.5f;
            gameBoss.playerMoney -= 50 * timesBought;
        }
    }
}
