using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script to add new conveyor belt when certain conditions are met
public class UpgradeFactory : MonoBehaviour
{
    public ConveyorScript2 conveyorBelt;
    //public Button button;
    public GameObject partGenerator;
    public float distanceBetweenConveyers;
    int upgradeNum;
    public int maxUpgrades;
    public Button addConveyorButton;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(upgradeNum == maxUpgrades && button.IsInteractable())
        {
            button.interactable = false;
            button.gameObject.SetActive(false);
        }*/
    }

    [ContextMenu("Create Conveyor")]
    public void CreateConveyer()
    {
        if(upgradeNum < maxUpgrades)
        {
            upgradeNum++;
            //Instantiate(partGenerator, new Vector2(partGenerator.transform.position.x, partGenerator.transform.position.y - upgradeNum * distanceBetweenConveyers), Quaternion.identity);
            ConveyorScript2 newCon = Instantiate(conveyorBelt, new Vector2(conveyorBelt.transform.position.x, conveyorBelt.transform.position.y - upgradeNum * distanceBetweenConveyers), Quaternion.identity);
            newCon.speed = 2.0F + upgradeNum * 0.5F;
            addConveyorButton.transform.Translate(0.0F,-distanceBetweenConveyers,0.0F);
            if (upgradeNum >= maxUpgrades)
            {
                addConveyorButton.gameObject.SetActive(false);
            }
            /*newCon.GetComponent<ConveyorScript>().speed = conveyorBelt.GetComponent<ConveyorScript>().speed;
            newCon.GetComponent<ConveyorScript>().direction = conveyorBelt.GetComponent<ConveyorScript>().direction;*/
        }

    }

}
