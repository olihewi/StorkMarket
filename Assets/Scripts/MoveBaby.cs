using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBaby : MonoBehaviour
{
    public static bool isInEvalScene;
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
        EvalSceneManager.isInEvalScene = true;
        BodyPart[] children = newStool.GetComponentsInChildren<BodyPart>();
        EvaluateBaby(children);
        newStool.GetComponent<Collider2D>().enabled = true;
        newStool.SetActive(true);
    }

    void EvaluateBaby(BodyPart[] children)
    {
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

}
