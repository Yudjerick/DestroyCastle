using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardLayout : MonoBehaviour
{
    [SerializeField] private float angle = 15f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int childCount = transform.childCount;
        float currentAngle = angle;
        float angleStep = angle * 2 / (childCount - 1);
        for (int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Card>().DraggedNow)
            {
                continue;
            }
            transform.GetChild(i).rotation = Quaternion.Lerp(transform.GetChild(i).rotation, Quaternion.Euler(0, 0, currentAngle - i * angleStep), 0.5f); //rewrite?
        }
    }
}
