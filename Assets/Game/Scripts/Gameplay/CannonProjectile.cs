using UnityEngine;

namespace Gameplay
{
    public class CannonProjectile : MonoBehaviour
    {
        [SerializeField]
        private float m_speed = 0.2f;

        [SerializeField]
        private int m_damage = 10;

        void Update()
        {
            var translation = transform.forward * m_speed;
            transform.Translate(translation);
        }

        void OnTriggerEnter(Collider other)
        {
            var monster = other.gameObject.GetComponent<Monster>();
            if (monster == null)
                return;

            monster.m_hp -= m_damage;
            if (monster.m_hp <= 0)
            {
                Destroy(monster.gameObject);
            }

            Destroy(gameObject);
        }
    }
}