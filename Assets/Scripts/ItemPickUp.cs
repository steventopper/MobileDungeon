using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemPickUp : MonoBehaviour
{
    public Text item;
    // Start is called before the first frame update
    public bool textChange = false;
    public float textChangeTimer = 0.0f;
    public float textTime = 5.0f;
    void Start()
    {
        item.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textChange == true)
        {
            textChangeTimer += Time.deltaTime;
        }
        if (textChange == true && textChangeTimer > textTime)
        {
            item.text = "";
            textChangeTimer = 0.0f;
            textChange = false;
        }
    }
    public void ChangeItemText(string text)
    {
        textChange = true;
        item.text = text;
        //Debug.Log("TEXT CHANGE");
    }
}
