using UnityEngine;
public class Entity : MonoBehaviour
{
 //   public float Health = 100f;
    public float Resistance;
    public float MagResistance;
    public int experienceReward = 25;

    private ExperienceManager experienceManager;

    void Start()
    {
        
        experienceManager = FindObjectOfType<ExperienceManager>();
    }

    void Update()
    {
        if (Health <= 0)
        {
            GrantExperience();  
            Destroy(gameObject);
        }
    }

    public void TakePhysicalDmg(float Damage)
    {
        float takenDamge = Damage - (0.75f * Resistance);
     //   Health -= takenDamge;
    }

    public void TakeMagicDmg(float Damage)
    {
        float takenDamge = Damage - (0.75f * MagResistance);
      //  Health -= takenDamge;
    }

    private void GrantExperience()
    {
        if (experienceManager != null)
        {
            experienceManager.AddExperience(experienceReward); 
        }
    }
}