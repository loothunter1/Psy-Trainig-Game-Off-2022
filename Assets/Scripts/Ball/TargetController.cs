using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public int currentScore = 0;
    public GameObject player;
    public BallTimerController ballTimerController;

    public Transform gridTopLeft;
    public Transform gridBottomRight;
    public int gridX;
    public int gridY;

    public float minRange;
    public float minProbability;

    float stepX;
    float stepY;
    // Start is called before the first frame update
    void Start()
    {
        if (gridX > 0)
        {
            stepX = (gridBottomRight.transform.position.x - gridTopLeft.transform.position.x) / gridX;
        }
        if (gridY > 0)
        {
            stepY = (gridBottomRight.transform.position.z - gridTopLeft.transform.position.z) / gridY;
        }
        Debug.Log(stepX+" "+stepY);
        Relocate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentScore += 1;
            ballTimerController.Score = currentScore;
            Relocate();
        }
    }

    void Relocate()
    {
        Vector3 playerPosition = player.transform.position;
        int posX = Random.Range(0, gridX + 1);
        int posY = Random.Range(0, gridY + 1);
        Vector3 newPosition = GridToVectorCoords(posX, posY);
        if ((newPosition - playerPosition).magnitude > minRange)
        {
            Debug.Log("Lucky! New postion: x="+posX+" y="+posY);
            transform.position = newPosition;
        }
        else
        {
            int positionsAvailable = 0;
            for (int i = 0; i < gridX+1; i++)
            {
                for (int j = 0; j < gridY+1; j++)
                {
                    posX = i;
                    posY = j;

                    newPosition = GridToVectorCoords(posX, posY);
                    if ((newPosition - playerPosition).magnitude > minRange)
                    {
                        positionsAvailable++;
                    }
                }

            }
            if (positionsAvailable == 0)
            {
                Debug.Log("No positions for relocation available!");
            }
            else if (positionsAvailable / gridX * gridY < minProbability)
            {
                Debug.Log("Probability is too low!");
            }
            else
            {
                posX = Random.Range(0, gridX + 1);
                posY = Random.Range(0, gridY + 1);
                newPosition = GridToVectorCoords(posX, posY);
                while ((newPosition - playerPosition).magnitude < minRange)
                {
                    posX = Random.Range(0, gridX + 1);
                    posY = Random.Range(0, gridY + 1);
                    newPosition = GridToVectorCoords(posX, posY);
                }
                Debug.Log("New postion: x=" + posX + " y=" + posY);
                transform.position = newPosition;
            }
        }
    }

    Vector3 GridToVectorCoords(int posX,int posY)
    {
        return new Vector3(gridTopLeft.transform.position.x + stepX * posX, transform.position.y, gridTopLeft.transform.position.z + stepY * posY);
    }
}
