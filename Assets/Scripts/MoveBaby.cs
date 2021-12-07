using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class MoveBaby : MonoBehaviour
{
    public static bool isInEvalScene;
    [HideInInspector] public GameObject stool;
    public GameObject stoolPrefab;
    public Transform stoolParent;
    public Transform stoolPos;
    GameObject newStool;
    Vector3 originalStoolPos;
    public GameObject hopper;
    public TextMeshProUGUI hopperText;
    public Evaluation evaluation;

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
        stool = stoolParent.GetChild(0).gameObject;
        obj = stoolParent.GetChild(0).gameObject;
        originalStoolPos = obj.transform.position;
        BodyPart[] oldParts = obj.transform.GetChild(0).GetComponentsInChildren<BodyPart>();
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
        newStool.transform.SetParent(stoolParent);
        evaluation.StartEvaluation(newStool.GetComponentsInChildren<BodyPart>()[1]);
        //EvalSceneManager.isInEvalScene = true;
        //BodyPart[] children = newStool.GetComponentsInChildren<BodyPart>();
        //EvaluateBaby(children);
        newStool.GetComponent<Collider2D>().enabled = true;
        newStool.SetActive(true);
    }

    void EvaluateBaby(BodyPart[] children)
    {
        Destroy(stoolParent.GetChild(0).gameObject);
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
        GameObject newStool = Instantiate(stoolPrefab, originalStoolPos, Quaternion.identity);
        newStool.transform.SetParent(stoolParent);
        EvalSceneManager.isInEvalScene = false;
        Destroy(stoolParent.GetChild(0).gameObject);
        hopper.GetComponent<Hopper>().resetText();
    }

}
