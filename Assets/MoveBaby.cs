using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBaby : MonoBehaviour
{
    
    [HideInInspector] public GameObject stool;
    public Transform stoolPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetStool(GameObject obj)
    {
        //obj.SetActive(false);
        for (int i = 0; i < obj.transform.GetChild(0).childCount; i++)
        {
            Destroy(obj.transform.GetChild(0).GetChild(i).gameObject);
        }
        stool = obj;

        GameObject newStool = Instantiate(stool, stoolPos.position, Quaternion.identity);
        newStool.GetComponent<Collider2D>().enabled = true;
        newStool.SetActive(true);
    }
}
