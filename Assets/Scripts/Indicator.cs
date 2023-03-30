using UnityEngine;
public class Indicator : MonoBehaviour
{
    public Vector3 RelativeTarget {get;set;}


    void Update()
    {
        this.transform.localScale = new Vector3(RelativeTarget.magnitude/5 , RelativeTarget.magnitude,1);
        this.transform.localPosition = RelativeTarget / 2;
        var angle = Mathf.Atan2(RelativeTarget.y,RelativeTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90,Vector3.forward);
    }
}