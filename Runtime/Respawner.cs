
using UnityEngine;

/// <summary>
/// When this object touches other object with curtain layer [respawnWhenCollide]
/// for respawn time, it will be respawned to spawn position.
/// Default spawn position is at start position of object.
/// </summary>

namespace com.dgn.Respawner
{
    [AddComponentMenu("DGN/Respawn Behavior/Respawner", 1)]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class Respawner : RespawnBehavior
    {
        [SerializeField]
        public Transform respawnAt;
        [SerializeField]
        private bool disableWhenIsKinematic;

        public bool DisableWhenIsKinematic
        {
            get { return disableWhenIsKinematic; }
            set
            {
                disableWhenIsKinematic = value;
                UpdateEnableRespawn();
            }
        }

        protected override void Start()
        {
            base.Start();
            UpdateEnableRespawn();
        }

        private void Update()
        {
            UpdateEnableRespawn();
        }

        protected override void Respawn()
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            if (respawnAt)
            {
                this.transform.position = respawnAt.position;
                this.transform.eulerAngles = respawnAt.eulerAngles;
            }
            else
            {
                this.transform.position = defaultPosition;
                this.transform.eulerAngles = defaultEulerAngles;
            }
        }

        private void UpdateEnableRespawn()
        {
            if (disableWhenIsKinematic) SetEnableRespawn(rb && !rb.isKinematic);
            else SetEnableRespawn(true);
        }

        private void OnValidate()
        {
            UpdateEnableRespawn();
        }
    }
}