using UnityEngine;

public class TeleportBehavior : MonoBehaviour
{
    public GameObject tpTo;
    public bool tpEnabled = true;
    void OnTriggerEnter(Collider other)
    {
        if (tpEnabled)
        {
            other.transform.position = tpTo.transform.position;
            tpTo.GetComponent<TeleportBehavior>().tpEnabled = false;
        }
        
    }
    void OnTriggerExit()
    {
        tpEnabled = true;
    }
}
