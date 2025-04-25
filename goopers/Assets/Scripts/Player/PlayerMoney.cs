using System.Resources;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private GameBoss gameBoss;
    private float playerMoney;
    [SerializeField] TextMeshProUGUI message;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMoney = gameBoss.playerMoney;
        message.text = string.Format("{0}{1}", "$", playerMoney);
    }
}
