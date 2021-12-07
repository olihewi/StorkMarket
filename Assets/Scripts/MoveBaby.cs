using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBaby : MonoBehaviour
{
    public static bool isInEvalScene;
    [HideInInspector] public GameObject stool;
    public GameObject stoolPrefab;
    public Transform stoolPos;
    GameObject newStool;
    Vector3 originalStoolPos;

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
        originalStoolPos = obj.transform.position;
        BodyPart[] oldParts = obj.transform.GetChild(0).GetChild(0).GetComponentsInChildren<BodyPart>();
        foreach (var item in oldParts)
        {
            Debug.Log(item.name);
            if(item != obj)
            {
                Destroy(item.gameObject);
            }
            
        }
        stool = obj;

        newStool = Instantiate(stool, stoolPos.position, Quaternion.identity);
        //EvalSceneManager.isInEvalScene = true;
        BodyPart[] children = newStool.GetComponentsInChildren<BodyPart>();
        EvaluateBaby(children);
        newStool.GetComponent<Collider2D>().enabled = true;
        newStool.SetActive(true);
    }

    void EvaluateBaby(BodyPart[] children)
    {
        Destroy(stool);
        List<PartAttributes> score = new List<PartAttributes>();
        for (int i = 0; i < children.Length; i++)
        {
            for (int j = 0; j < children[i].attributes.Capacity; j++)
            {
                score.Add(children[i].attributes[j]);
            }

        }
        foreach (PartAttributes attribute in score)
        {
            Debug.Log(attribute.attribute.name + ": " + attribute.percent);
        }
    }

    public void DetroyNewStool()
    {
        Instantiate(stoolPrefab, originalStoolPos, Quaternion.identity);
        EvalSceneManager.isInEvalScene = false;
        if(newStool != null)
        {
            Destroy(newStool);
        }
    }

}
