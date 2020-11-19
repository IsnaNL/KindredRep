using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseTest : MonoBehaviour
{
    private RectTransform thisRectTrans;
    // Start is called before the first frame update
    void Start()
    {
        thisRectTrans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        thisRectTrans.position = Input.mousePosition;
    }
}
