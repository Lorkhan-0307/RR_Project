
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;


    [Header("enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    [Header("Attackpoint")]
    [SerializeField] public Transform attackpoint;





    private void Awake()
    {
        initScale = enemy.localScale;
        
    }

    private void OnDisable()
    {
        anim.SetBool("walking", false);
    }


    private void Update()
    {
        
        if(movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveinDirection(-1);
            }
            else
            {
                //change direction
                DirectionChange();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveinDirection(1);
            }
            else
            {
                //change direction
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("walking", false);

        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;

            //Make attackpoint rotate
            attackpoint.transform.Rotate(0f, 180f, 0f);
        }

        
    }


    private void MoveinDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("walking", true);
        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
}
