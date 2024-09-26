using UnityEngine;

public class player : MonoBehaviour
{
    public float player_HP = 100.0f;

    public void TakeDamage(float damage)
    {
        player_HP -= damage;
        Debug.Log("Player hit! Current HP: " + player_HP);

        if (player_HP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player is dead!");
        gameObject.SetActive(false);
    }
}
