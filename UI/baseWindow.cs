using UnityEngine;
using System.Collections;

namespace Goranee
{
    public enum WinType
    {
        Modeless,
        Modal,
    }
    public class baseWindow : MonoBehaviour
    {
        public virtual void ClearInfo()
        { }
        public virtual void Init()
        {
        }
        public virtual void Remove()
        {
        }
        public virtual void Show()
        {
            gameObject.SetActive(true);
            Refresh();
        }
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
        public virtual void Refresh()
        {
        }

        public virtual bool IsShow()
        {
            return gameObject.activeInHierarchy;
        }
        public virtual void Cancel()
        {
            Hide();
        }
    }
}

