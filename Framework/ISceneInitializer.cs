using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goranee
{
    public interface ISceneInitializer 
    {
        void Init();
        void ReInit();

        void Clear();
        void Active();
        void Deactive();

        void Run();

        void Close();
        void Destroy();
    }
}


