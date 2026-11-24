using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private bool destroyOnDeath;

    public int CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead || damage <= 0)
            return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

        if (CurrentHealth == 0)
        {
            IsDead = true;

            if (destroyOnDeath)
                Destroy(gameObject);
            else
                Debug.Log($"{name} đã bị hạ.");
        }
    }
}