using UnityEngine;

namespace ServiceLocatorPattern
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private GameObject missilePrefab;
        [SerializeField] private GameObject[] barrels;
        [SerializeField] private float rotationSpeed = 4f;
        [SerializeField] private float shotSpeed = 400;
        [SerializeField] private AudioClip shootingSound;

        private int barrel_index = 0;
        private ServiceLocator serviceLocator;
        private ISoundService soundService;
        private IDebuggerService debuggerService;

        private void Start()
        {
            serviceLocator = ServiceLocator.Instance;
            soundService = serviceLocator.GetService<ISoundService>();
            debuggerService = serviceLocator.GetService<IDebuggerService>();
        }

        private void Update()
        {
            Vector2 turretPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 direction = (Vector2)Input.mousePosition - turretPosition;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z,
                (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f, rotationSpeed * Time.deltaTime)));

            if (Input.GetMouseButtonDown(0) && barrels != null)
            {
                soundService?.PlaySound(shootingSound);
                debuggerService?.DebugMessage("We are shooting");

                GameObject bullet = Instantiate(missilePrefab, barrels[barrel_index].transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, shotSpeed));

                barrel_index++;

                if (barrel_index >= barrels.Length)
                {
                    barrel_index = 0;
                }
            }
        }
    }
}