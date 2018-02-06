using System;
using UnityEngine;
using System.Collections.Generic;

namespace Goranee
{
    // like-stack state machine
    public abstract class baseStateMachine<TOwner, TMessage> 
    {
        protected State<TOwner, TMessage> prevState;
        protected TOwner ownerEntity;
        public State<TOwner, TMessage> BackgroundState { get; protected set; }
        public State<TOwner, TMessage> CurrentState { get; protected set; }

       
        public void SetOwner(TOwner owner)
        {
            ownerEntity = owner;
        }
        public virtual bool Change(State<TOwner, TMessage> newState)
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
            else
            {
                Debug.Log("CurrentState is NULL\n");
            }
        }
        public virtual void SetBackground(State<TOwner, TMessage> backState)
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