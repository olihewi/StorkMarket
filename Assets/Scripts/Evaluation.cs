using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Evaluation : MonoBehaviour
{
    public TextMeshPro textMesh;

    public void StartEvaluation(BodyPart _basePart)
    {
        Request _request = GameObject.FindObjectOfType<Request>();
        StartCoroutine(Evaluate(_request, _basePart));
    }

    IEnumerator Evaluate(Request _request, BodyPart _basePart)
    {
        BodyPart[] childParts = _basePart.GetComponentsInChildren<BodyPart>();
        textMesh.text = "EVALUATION\n*******************\n";
        yield return new WaitForSeconds(0.25F);
        // Baby Completion Score
        textMesh.text += "Body Parts: ";
        float bodyPartScore = childParts.Length / 6.0F;
        textMesh.text += childParts.Length + "/6\n";
        yield return new WaitForSeconds(0.25F);
        // Type Score
        textMesh.text += _request.requestedType.name + ": ";
        float typeScore = 0.0F;
        foreach (BodyPart part in childParts)
        {
            if (part.type == _request.requestedType)
            {
                typeScore++;
            }
        }
        typeScore /= childParts.Length;
        textMesh.text += typeScore.ToString("P") + "\n";
        yield return new WaitForSeconds(0.25F);
        // Attribute Score
        float attributeScore = 0.0F;
        foreach (Attribute attribute in _request.requestedAttributes)
        {
            textMesh.text += attribute.name + ": ";
            float thisAttributeScore = 0.0F;
            float overallScore = 0.0F;
            foreach (BodyPart part in childParts)
            {
                foreach (PartAttributes partAttribute in part.attributes)
                {
                    overallScore += partAttribute.percent;
                    if (partAttribute.attribute == attribute)
                    {
                        thisAttributeScore += partAttribute.percent;
                    }
                }
            }
            float thisAttributePercent = thisAttributeScore / overallScore;
            attributeScore += thisAttributePercent;
            textMesh.text += thisAttributePercent.ToString("P") + "\n";
            yield return new WaitForSeconds(0.25F);
        }
        attributeScore /= _request.requestedAttributes.Count;
        textMesh.text += "*******************\nPotential Income: " + _request.value.ToString("C") + "\n";
        yield return new WaitForSeconds(0.25F);
        float priceMultiplier = bodyPartScore;
        priceMultiplier *= typeScore * 0.5F + 0.5F;
        priceMultiplier *= attributeScore * 0.25F + 0.75F;
        textMesh.text += "Deductions: " + (1.0F-priceMultiplier).ToString("P") + "\n";
        yield return new WaitForSeconds(0.25F);
        float finalPrice = _request.value * priceMultiplier;
        textMesh.text += "Total Profit: " + finalPrice.ToString("C");
        MoneyManager.INSTANCE.money += finalPrice;
    }
}
