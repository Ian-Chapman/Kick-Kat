using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{

    public GameObject Steak;
    public GameObject Fishbone;
    public GameObject Fabric;

    public float rotateSpeed;
    public float floatFrequency, floatAmplitude;
    public Vector3 startPos;
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    // to uncomment all of this, need a way to set up a meshrenderer that has the correct mesh/ shapes for each item

    private Item item;
    void Awake()
    {
        startPos = transform.position;
    }

    void Update()
    {
        //Make sure you are using the right parameters here
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        Vector3 tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;

        transform.position = tempPos;
    }

    public void SetItem(Item item)
    {
        this.item = item;
        Debug.Log(item.itemType);
        switch(item.itemType)
        {
            case Item.ItemType.PowerUp:
                Fishbone.SetActive(true);
                break;
            case Item.ItemType.Points:
                Steak.SetActive(true);
                break;
            case Item.ItemType.tempPup:
                Fabric.SetActive(true);
                break;

        }
            
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
