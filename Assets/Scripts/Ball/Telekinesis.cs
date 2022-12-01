using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    public float tkPower;
    public float tkMaxDistance;
    public float tkMaxTime;
    public float tkInterfaceMaxRange;

    bool tkPush;
    bool tkPull;
    Vector3 tkPoint;
    Vector3 glanceDirection;
    float tkDistance;
    Collider tkLock;
    float tkLockTime;

    public Transform tkRangeMarker;
    public VolumetricLines.VolumetricLineBehavior tkCommandLine;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 scaleVector = new Vector3(tkInterfaceMaxRange, tkInterfaceMaxRange, tkInterfaceMaxRange);
        tkRangeMarker.localScale = scaleVector;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TKInitiate(true);
            if (tkPush)
            {
                tkRangeMarker.position = tkPoint;
                tkRangeMarker.gameObject.SetActive(true);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            TKInitiate(false);
            if (tkPull)
            {
                tkRangeMarker.position = tkPoint;
                tkRangeMarker.gameObject.SetActive(true);
            }
        }

        if (tkPush||tkPull)
        {
            Ray tkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 tkCommandPoint = tkRay.GetPoint(tkDistance);
            tkCommandLine.EndPos=tkCommandPoint-tkPoint;
            if (Input.GetMouseButtonUp(0) && tkPush)
            {
                TKRelease(true);
                tkRangeMarker.gameObject.SetActive(false);
            }
            else if (Input.GetMouseButtonUp(1) && tkPull)
            {
                TKRelease(false);
                tkRangeMarker.gameObject.SetActive(false);
            }

        }

    }

    void TKInitiate(bool push)
    {
        Ray tkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("TK ray");
        if (Physics.Raycast(tkRay, out hit, tkMaxDistance) && hit.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("TK lock");
            tkPoint = hit.point;
            tkDistance = hit.distance;
            tkLock = hit.collider;
            tkLockTime = Time.time;
            glanceDirection = tkRay.direction;
            if (push)
                tkPush = true;
            else
                tkPull = true;
        }
    }

    void TKRelease(bool push)
    {
        Ray tkRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 tkCommandPoint = tkRay.GetPoint(tkDistance);
        float tkTime = Time.time - tkLockTime;
        Vector3 tkVector = tkCommandPoint - tkPoint;
        float nTime = tkTime / tkMaxTime;
        float nRange = tkVector.magnitude / tkInterfaceMaxRange;
        float nMagnitude = Mathf.Sqrt(nTime * nTime + nRange * nRange);
        Vector3 commandVector = tkVector.normalized * nRange / nMagnitude;
        if (push)
            commandVector += glanceDirection.normalized * nTime / nMagnitude;
        else
            commandVector -= glanceDirection.normalized * nTime / nMagnitude;
        commandVector.Normalize();
        commandVector *= tkPower;
        Debug.Log("TK vector: " + (commandVector).ToString());
        tkLock.GetComponent<Rigidbody>().AddForceAtPosition(commandVector, tkPoint, ForceMode.Impulse);
        if (push)
            tkPush = false;
        else
            tkPull = false;
    }
}
