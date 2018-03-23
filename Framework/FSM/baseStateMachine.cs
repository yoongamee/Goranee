using System;
using UnityEngine;
using System.Collections.Generic;

namespace Goranee
{
    // like-stack state machine
    public abstract class baseStateMachine<TOwner> 
    {
        protected State<TOwner> prevState;
        protected TOwner ownerEntity;
        public State<TOwner> BackgroundState { get; protected set; }
        public State<TOwner> CurrentState { get; protected set; }

       
        public void SetOwner(TOwner owner)
        {
            ownerEntity = owner;
        }
        public virtual bool Change(State<TOwner> newState)
        {
            prevState = CurrentState;

            if (prevState != null)
            {
                prevState.Out(ownerEntity);
            }

            CurrentState = newState;


            if (CurrentState != null)
            {
                CurrentState.In(ownerEntity);
            }

            return true;
        }

        public virtual void Update()
        {
            if (BackgroundState != null)
            {
                BackgroundState.Execute(ownerEntity);
            }
            
            if (CurrentState != null)
            {
                CurrentState.Execute(ownerEntity);
            }
            /*else
            {
                Debug.Log("CurrentState is NULL\n");
            }*/
        }
        public virtual void SetBackground(State<TOwner> backState)
        {
            if (BackgroundState != null)
            {
                BackgroundState.Out(ownerEntity);
            }


            BackgroundState = backState;

            if (BackgroundState != null)
            {
                BackgroundState.In(ownerEntity);
            }
        }
        public virtual void GoPreview()
        {
            Change(prevState);
        }
    }


}