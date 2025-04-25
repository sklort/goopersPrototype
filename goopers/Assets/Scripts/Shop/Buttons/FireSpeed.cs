using UnityEngine;

public class FireSpeed : MonoBehaviour
{
   [SerializeField] private GameBoss gameBoss;
   [SerializeField] private ShopManager shopManager;

   private float moneyToBuy;

   public void FireSpeedButton()
   {
      moneyToBuy = 50 * gameBoss.globalFireSpeed;

      if (gameBoss.playerMoney >= moneyToBuy)
      {
         gameBoss.playerMoney -= 50 * gameBoss.globalFireSpeed;
         gameBoss.globalFireSpeed++;
      }

   }
}
