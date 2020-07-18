using BRM;
namespace Photon
{
    using UnityEngine;

    public class MonoBehaviour : UnityEngine.MonoBehaviour
    {
        internal Pool pool;
        internal Pool homePool;
        internal bool isBackground;
        PhotonView view;

        private void Awake()
        {
            if (this is PhotonView)
            {
                if ((this.view = (PhotonView)this) && !this.isBackground)
                {
                    this.isBackground = this.view.isSceneView;
                    return;
                }
            }
            else if ((this.view = base.GetComponent<PhotonView>()) && !this.isBackground)
            {
                this.isBackground = this.view.isSceneView;
            }
        }

        public PhotonView networkView
        {
            get
            {
                Debug.LogWarning("Why are you still using networkView? should be PhotonView?");
                return PhotonView.Get(this);
            }
        }

        public PhotonView photonView
        {
            get
            {
                if (this.view != null)
                {
                    return this.view;
                }
                return this.view = base.GetComponent<PhotonView>();
            }
        }

        public bool isLocal
        {
            get
            {
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    return true;
                }
                PhotonView photonView = this.photonView;
                return photonView != null && photonView.isMine;
            }
        }

        private void OnDestroy()
        {
            if (this.pool != null)
            {
                this.pool.Clear();
            }
        }
    }
}

