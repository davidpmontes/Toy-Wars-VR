using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    List<GameObject> enemyPos;
    private bool onScreen;
    bool offScreenLeft;
    bool offScreenRight;

    [SerializeField] private GameObject LeftEnemyIndicator = default;
    [SerializeField] private GameObject RightEnemyIndicator = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        UpdateIndicators(Camera.main.transform.position);
    }

    public void UpdateIndicators(Vector3 playerPosition)
    {
        enemyPos = EnemyManager.Instance.GetAllEnemyPositions();
        offScreenLeft = false;
        offScreenRight = false;
        
        for (int i = 0; i < EnemyManager.Instance.GetAllEnemyPositions().Count; i++)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(enemyPos[i].transform.position);
            onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

            if(!offScreenLeft)
            {
                offScreenLeft = !onScreen && screenPoint.x < .5;
            }
            
            if(!offScreenRight)
            {
                offScreenRight = !onScreen && screenPoint.x > .5;
            }
           
        }
        
        LeftEnemyIndicator.SetActive(offScreenLeft);
        RightEnemyIndicator.SetActive(offScreenRight);



    }

}
