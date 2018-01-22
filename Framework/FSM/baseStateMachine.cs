using System;
using UnityEngine;
using System.Collections.Generic;

namespace Goranee
{
    // like-stack state machine
    public abstract class baseStateMachine<TOwner, TMessage> :  IMessageProc<TMessage>
    {
        protected State<TOwner, TMessage> backgroundState;
        protected State<TOwner, TMessage> prevState;
        protected TOwner ownerEntity;
        public State<TOwner, TMessage> CurrentState { get; protected set; }

        public abstract void ReceiveMessage(TMessage message);

        public void SetOwner(TOwner owner)
        {
            ownerEntity = owner;
        }
        public virtual bool ChangeState(State<TOwner, TMessage> newState)
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
            if (backgroundState != null)
            {
                backgroundState.Execute(ownerEntity);
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
        public virtual void SetBackgroundState(State<TOwner, TMessage> backState)
        {
            if (backgroundState != null)
            {
                backgroundState.Out(ownerEntity);
            }


            backgroundState = backState;

            if (backgroundState != null)
            {
                backgroundState.In(ownerEntity);
            }
        }
        public virtual void GoPreviewState()
        {
            ChangeState(prevState);
        }
    }


}