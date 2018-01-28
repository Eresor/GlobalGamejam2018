using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewController : MonoBehaviour
{

    public Text ItemName;
    public Text ItemInfo;
    public PlayerPickingScript PlayerPick;

    void Start()
    {
        ClearInfo();
    }

    void ClearInfo()
    {
        ItemName.text = "Current item: None";
        ItemInfo.text = "";
    }

    void Update()
    {
        var item = PlayerPick.holdingObject;
        if (!item)
        {
            ClearInfo();
            return;
        }

        var itemType = item.GetComponent<PickableObject>().objectType;
        ItemName.text = "Current item: " + (PlayerPick.holdingObject
            ? itemType.ToString()
            : "None");

        StringBuilder infoText = new StringBuilder();
        if (itemType == PickableObject.ObjectType.sword)
        {
            infoText.Append("Durability: ");
            infoText.Append(PlayerPick.holdingObject.GetComponentInChildren<SwordCollisionAttack>().Durability);
        }
        ItemInfo.text = infoText.ToString();
    }

}
