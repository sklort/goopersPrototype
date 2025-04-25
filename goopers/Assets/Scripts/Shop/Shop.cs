using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameBoss gameBoss;
    [SerializeField] private GameObject pressE;
    [SerializeField] private GameObject shopInterface;
    [SerializeField] private GameObject crosshair;
    
    public bool canShop;
    private bool shopOpen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canShop = gameBoss.canShop;
    }

    // Update is called once per frame
    void Update()
    {
        canShop = gameBoss.canShop;
        
        if (canShop == true)
        {
            pressE.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                shopOpen = !shopOpen;
                shopInterface.SetActive(shopOpen);
            }
        }
        else
        {
            pressE.SetActive(false);
        }

        if (shopOpen && gameBoss.newGame)
        {
            crosshair.SetActive(false);
            gameBoss.canPlay = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else if (!shopOpen && gameBoss.newGame)
        {
            crosshair.SetActive(true);
            gameBoss.canPlay = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
