using TMPro;
using UnityEngine;

public class TDHealth : MonoBehaviour
{
    [SerializeField] private GameBoss gameBoss;
    [SerializeField] TextMeshProUGUI message;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        message.text = string.Format("{0}{1}{2}{3}", "HP: ", gameBoss.TDHealth, " / ", gameBoss.TDHealthMax);
    }
}
