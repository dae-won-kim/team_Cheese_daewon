using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public string[] itemDB = new string[4];
    public Image[] itemImageDB = new Image[4];

    public GameObject BrownTeddyBear_Object;
    public GameObject PinkTeddyBear_Object;
    public GameObject YellowTeddyBear_Object;
    public GameObject Cake_Object;
    public GameObject NPC_Object;

    public Sprite BrownTeddyBear_Sprite;
    public Sprite PinkTeddyBear_Sprite;
    public Sprite YellowTeddyBear_Sprite;
    public Sprite Cake_Sprite;
    public Sprite NPC_Sprite;

    private DialogueManager dialogueManager;
    private UIManager uiManager;

    // ��ȭ����
    [SerializeField]
    public Dialogue d_camera;

    GameObject GetItemObject(string item_name)
    {
        switch(item_name)
        {
            case "BrownTeddyBear": return BrownTeddyBear_Object;
            case "PinkTeddyBear": return PinkTeddyBear_Object;
            case "YellowTeddyBear": return YellowTeddyBear_Object;
            case "Cake": return Cake_Object;
            case "NPC": return NPC_Object;

            default: return null;
        }
    }

    Sprite GetItemSprite(string item_name)
    {
        switch (item_name)
        {
            case "BrownTeddyBear": return BrownTeddyBear_Sprite;
            case "PinkTeddyBear": return PinkTeddyBear_Sprite;
            case "YellowTeddyBear": return YellowTeddyBear_Sprite;
            case "Cake": return Cake_Sprite;
            case "NPC": return NPC_Sprite;

            default: return null;
        }
    }

    // ������ ��ġ
    public void PlaceItem(int slotIndex)
    {
        if (itemDB[slotIndex] == null) return;

        GameObject itemObject = GetItemObject(itemDB[slotIndex]);
        Vector3 position = Player.object_collision == "��" ? Player.pos : Object.pos;

        Instantiate(itemObject, position, Quaternion.identity);
        itemDB[slotIndex] = null;
        itemImageDB[slotIndex].sprite = null;
    }

    //������ �ݱ�
    void ItemPickup(string itemName, Collider2D other)
    {
        for (int i = 0; i < itemDB.Length; i++)
        {
            if (string.IsNullOrEmpty(itemDB[i]))
            {
                itemDB[i] = itemName;
                itemImageDB[i].sprite = GetItemSprite(itemName);
                Destroy(other.gameObject);
                break;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch(other.gameObject.tag)
            {
                case "DroppedBrownTeddyBear":
                    ItemPickup("BrownTeddyBear", other);

                    dialogueManager.ShowDialogue(d_camera);
                    uiManager.CameraUI.SetActive(true);
                    break;

                case "BrownTeddyBear":
                    ItemPickup("BrownTeddyBear", other);
                    break;

                case "PinkTeddyBear":
                    ItemPickup("PinkTeddyBear", other);
                    break;

                case "YellowTeddyBear":
                    ItemPickup("YellowTeddyBear", other);
                    break;

                case "Cake":
                    ItemPickup("Cake", other);
                    break;

                case "NPC":
                    ItemPickup("NPC", other);
                    break;
            }
        }
    }

     void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) PlaceItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) PlaceItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) PlaceItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) PlaceItem(3);
    }
}