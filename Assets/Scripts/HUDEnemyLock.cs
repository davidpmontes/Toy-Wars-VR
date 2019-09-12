using UnityEngine;
using UnityEngine.UI;

public class HUDEnemyLock : MonoBehaviour
{
    public Camera cam;

    public GameObject Targets;
    public GameObject[] enemyLock;

    public RectTransform rtReticle;
    public RectTransform rtRectangle;
    public GameObject TrueAimingLocation;

    public Texture texture;

    public Vector3 IdealAimingLocation;

    private RectTransform[] rt;
    private Image[] image;
    private Animator[] animator;

    private Vector3 IdealReticleLocation;

    private float distanceToTarget;
    private float distanceToClosestEnemy;
    private Vector3 screenPosition;
    private Vector3 viewportPosition;
    private Vector3 sweetSpot;

    private readonly float downMargin = 0.05f;
    private readonly float upMargin = 0.95f;
    private readonly float leftMargin = 0.05f;
    private readonly float rightMargin = 0.95f;

    private readonly float SCREEN_WIDTH_HALF = Screen.width / 2;
    private readonly float SCREEN_HEIGHT_HALF = Screen.height / 2;

    private readonly float DEFAULT_AIMING_DISTANCE = 5f;
    private readonly int NUM_ICONS = 4;
    private readonly float RETICLE_SPEED = 350;
    private readonly int TARGETING_RECTANGLE_RADIUS = 300;

    void Start()
    {
        rt = new RectTransform[NUM_ICONS];
        image = new Image[NUM_ICONS];
        animator = new Animator[NUM_ICONS];

        for (int i = 0; i < NUM_ICONS; i++)
        {
            animator[i] = enemyLock[i].GetComponent<Animator>();
            rt[i] = enemyLock[i].GetComponent<RectTransform>();
            image[i] = enemyLock[i].GetComponent<Image>();
        }
    }

    void Update()
    {
        DrawEnemyTargetingIcons();
        MoveReticleAndTrueAimingLocation();
    }

    private void MoveReticleAndTrueAimingLocation()
    {
        rtReticle.localPosition = Vector2.MoveTowards(rtReticle.localPosition,
                                                      IdealReticleLocation,
                                                      Time.deltaTime * RETICLE_SPEED);

        TrueAimingLocation.transform.position = cam.ScreenToWorldPoint(new Vector3(SCREEN_WIDTH_HALF + rtReticle.localPosition.x,
                                                                       SCREEN_HEIGHT_HALF + rtReticle.localPosition.y,
                                                                       IdealAimingLocation.z));
    }

    private void DrawEnemyTargetingIcons()
    {
        InitializeTargetingValues();
        LoopThroughTargets();
    }

    private void InitializeTargetingValues()
    {
        distanceToClosestEnemy = float.MaxValue;
        IdealReticleLocation = rtRectangle.localPosition;
        IdealAimingLocation = new Vector3(IdealAimingLocation.x,
                                          IdealAimingLocation.y,
                                          DEFAULT_AIMING_DISTANCE);
    }


    private void LoopThroughTargets()
    {
        var iconNumber = 0;

        foreach (Transform target in Targets.transform)
        {
            CalculateDistanceToSweetSpot(target);
            CalculateViewportPosition();
            CalculateScreenPosition();
            SelectEnemyTargetingIcon(iconNumber);
            DrawTargetingIcons(iconNumber);
            MoveCrossHairsToClosestEnemy();


            iconNumber++;
            if (iconNumber >= NUM_ICONS)
                break;
        }

        for (int i = iconNumber; i < NUM_ICONS; i++)
        {
            animator[i].Play("None");
        }
    }

    private void CalculateDistanceToSweetSpot(Transform target)
    {
        sweetSpot = target.Find("SweetSpot").gameObject.transform.position;
        distanceToTarget = Vector3.Distance(sweetSpot, cam.transform.position);
    }

    private void CalculateViewportPosition()
    {
        viewportPosition = cam.WorldToViewportPoint(sweetSpot);
    }

    private void SelectEnemyTargetingIcon(int iconNumber)
    {
        if (viewportPosition.z < 0)
        {
            animator[iconNumber].Play("None");
            return;
        }

        if (viewportPosition.x < leftMargin)
        {
            animator[iconNumber].Play("Left");
        }
        else if (viewportPosition.x > rightMargin)
        {
            animator[iconNumber].Play("Right");
        }
        else if (viewportPosition.y < downMargin)
        {
            animator[iconNumber].Play("Down");
        }
        else if (viewportPosition.y > upMargin)
        {
            animator[iconNumber].Play("Up");
        }
        else
        {
            animator[iconNumber].Play("Rotate");
        }
    }

    private void CalculateScreenPosition()
    {
        Vector3 viewportPositionClamped = new Vector3(Mathf.Clamp(viewportPosition.x, leftMargin, rightMargin),
                                                      Mathf.Clamp(viewportPosition.y, downMargin, upMargin),
                                                      viewportPosition.z);

        screenPosition = new Vector3(-SCREEN_WIDTH_HALF + (Screen.width * viewportPositionClamped.x),
                                     -SCREEN_HEIGHT_HALF + (Screen.height * viewportPositionClamped.y),
                                     viewportPosition.z);
    }

    private void DrawTargetingIcons(int iconNumber)
    {
        rt[iconNumber].localPosition = new Vector2(screenPosition.x,
                                                   screenPosition.y);
    }

    private void MoveCrossHairsToClosestEnemy()
    {
        if (ScreenPositionIsWithinTargetingRectangle())
        {
            CheckIfClosestEnemy();
        }
    }

    private bool ScreenPositionIsWithinTargetingRectangle()
    {
        return screenPosition.x > rtRectangle.localPosition.x - TARGETING_RECTANGLE_RADIUS &&
               screenPosition.x < rtRectangle.localPosition.x + TARGETING_RECTANGLE_RADIUS &&
               screenPosition.y > rtRectangle.localPosition.y - TARGETING_RECTANGLE_RADIUS &&
               screenPosition.y < rtRectangle.localPosition.y + TARGETING_RECTANGLE_RADIUS;
    }

    private void CheckIfClosestEnemy()
    {
        if (viewportPosition.z < distanceToClosestEnemy)
        {
            distanceToClosestEnemy = viewportPosition.z;
            IdealReticleLocation = new Vector3(screenPosition.x,
                                               screenPosition.y);

            IdealAimingLocation = new Vector3(IdealAimingLocation.x,
                                              IdealAimingLocation.y,
                                              distanceToTarget);
        }
    }
}
