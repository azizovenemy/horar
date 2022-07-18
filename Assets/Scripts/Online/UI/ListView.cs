using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListView : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private GameObject elementPrefab;
    [SerializeField] private float offset;
    [SerializeField] private List<GameObject> elements;

    public GameObject Add()
    {
        GameObject newElement = Instantiate(elementPrefab, content);

        if (elements.Count == 0)
        {
            elements.Add(newElement);
            return newElement;
        }

        GameObject lastElement = elements[elements.Count - 1];

        Vector3 lastElementPosition = lastElement.transform.localPosition;

        Vector3 newPosition = new Vector3
        {
            x = lastElementPosition.x,
            y = lastElementPosition.y - elementPrefab.GetComponent<RectTransform>().rect.height - offset,
            z = lastElementPosition.z
        };

        newElement.transform.localPosition = newPosition;

        elements.Add(newElement);

        float contentHeight = contentRect.rect.height;
        contentHeight += elementPrefab.GetComponent<RectTransform>().rect.height - offset;

        contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHeight);

        return newElement;
    }
}
