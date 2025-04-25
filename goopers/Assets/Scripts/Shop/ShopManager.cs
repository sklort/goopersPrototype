using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameBoss gameBoss;
    
    //Weapon trees
    [SerializeField] private GameObject shootyTree;
    [SerializeField] private GameObject autoShootyTree;
    [SerializeField] private GameObject swingyTree;
    [SerializeField] private GameObject bigSwingyTree;
    
    //Weapon upgrades
    [SerializeField] private GameObject shootyUpgrades;
    [SerializeField] private GameObject autoShootyUpgrades;
    [SerializeField] private GameObject swingyUpgrades;
    [SerializeField] private GameObject bigSwingyUpgrades;
    
    //Has weapon
    public bool hasShooty;
    public bool hasAutoShooty;
    public bool hasSwingy;
    public bool hasBigSwingy;
    
    //Is holding weapon
    public bool holdingShooty = true;
    public bool holdingAutoShooty;
    public bool holdingSwingy;
    public bool holdingBigSwingy;
    
    //General Upgrades
    public int fireSpeedUpgrade;
    public int damageUpgrade;
    public int tweetyBirdUpgrade;
    public int explodeUpgrade;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameBoss.canShop)
        {
            if (hasShooty)
            {
                shootyTree.SetActive(true);
                shootyUpgrades.SetActive(true);
            }
            else
            {
                shootyUpgrades.SetActive(false);
            }
        }
    }
}
