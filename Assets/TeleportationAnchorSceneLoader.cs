using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityEngine.XR.Interaction.Toolkit
{
    [AddComponentMenu("XR/Teleportation Anchor (Scene Loader)", 11)]
    public class TeleportationAnchorSceneLoader : BaseTeleportationInteractable
    {
        [SerializeField]
        [Tooltip("Nom de la scène à charger lors de l'activation.")]
        private string sceneToLoad;

        [SerializeField]
        [Tooltip("Le Transform où le joueur sera téléporté s'il reste dans la même scène.")]
        private Transform m_TeleportAnchorTransform;

        public Transform teleportAnchorTransform
        {
            get => m_TeleportAnchorTransform;
            set => m_TeleportAnchorTransform = value;
        }

        protected void OnValidate()
        {
            if (m_TeleportAnchorTransform == null)
                m_TeleportAnchorTransform = transform;
        }

        protected override void Reset()
        {
            base.Reset();
            m_TeleportAnchorTransform = transform;
        }

        public void RequestSceneLoad()
        {
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                // Vérifie si la scène est déjà chargée
                if (SceneManager.GetActiveScene().name != sceneToLoad)
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
                else
                {
                    Debug.LogWarning("La scène est déjà active. Téléportation au point d'ancrage.");
                    RequestTeleport(); // Fallback à une téléportation locale
                }
            }
            else
            {
                Debug.LogWarning("Aucune scène spécifiée. Téléportation au point d'ancrage.");
                RequestTeleport();
            }
        }

        public void RequestTeleport()
        {
            if (m_TeleportAnchorTransform != null)
            {
                var teleportRequest = new TeleportRequest
                {
                    destinationPosition = m_TeleportAnchorTransform.position,
                    destinationRotation = m_TeleportAnchorTransform.rotation
                };

                if (teleportationProvider != null)
                {
                    teleportationProvider.QueueTeleportRequest(teleportRequest);
                }
                else
                {
                    Debug.LogWarning("Aucun TeleportationProvider assigné.");
                }
            }
        }

        protected override bool GenerateTeleportRequest(IXRInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
        {
            // Appelé quand l'utilisateur sélectionne l'ancre
            RequestSceneLoad();
            return false; // Ne pas effectuer de téléportation standard
        }
    }
}
