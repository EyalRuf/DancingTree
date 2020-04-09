using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour
{
    public TouchIndicator touchIndicator;

    // Update is called once per frame
    void Update()
    {
        Touch[] touches = Input.touches;
        foreach (Touch touch in touches)
        {
            if (touch.phase.Equals(TouchPhase.Began))
            {
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(touch.position);
                Instantiate(touchIndicator, worldPos, Quaternion.identity);
            }
        }
    }
}
