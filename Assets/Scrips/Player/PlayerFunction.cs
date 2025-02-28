using UnityEngine;
using UnityEngine.UIElements;
public class PlayerFunction : MonoBehaviour
{
    [Header("Looking")]
    public float lookDistance = 3f;
    public LayerMask interactableLayer;

    [Header("Skill A")]
    public float rangeSkill = 4f;
    public float countDown = 1.5f;
    public GameObject skillAoeFx;
    public LayerMask enemyLayer;
    public float skillDamage = 6.5f;
    [Header("Skill B")]
    public float abc;

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        CameraLooking();
        InputForSkill();
    }

    void CameraLooking()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray look = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if (Physics.Raycast(look, out hit, lookDistance, interactableLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Tuong Tac" + hit.collider.gameObject.name);
            }
        }

    }
    void InputForSkill()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SkillA();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkillB(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SkillC();
        }
    }

    private void SkillC()
    {
        
    }

    private void SkillA()
    {
    
    }

   private void SkillB(Vector3 position)
   {
        if (skillAoeFx != null)
        {
            Instantiate(skillAoeFx, position, Quaternion.identity);
        }
            // Tìm tất cả kẻ địch trong vùng ảnh hưởng
            Collider[] hitEnemies = Physics.OverlapSphere(position, rangeSkill, enemyLayer);

            foreach (Collider enemy in hitEnemies)
            {
                //EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                //if (enemyHealth != null)
                //{
                //enemyHealth.TakeDamage(skillDamage);
                //}
            }
        
   }
}