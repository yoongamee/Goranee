using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Goranee
{
    public class MessageAction : baseMessage
    {
        private Action                      execute;
        private Action                      success;
        private Action                      failed;
        private MessageExtraInfo            extraInfo;

        private MessageAction()
        {
            SwallowMSG = false;
        }

        public override void Execute()
        {
            if ( execute != null)
            {
                execute();
            }
        }

        public MessageAction(int messageID, float dispatchTime,
            bool swallow, Action execution, Action resSuccess, Action resFailed, 
            MessageExtraInfo extra)
        {
            Set(null, null, messageID, dispatchTime, swallow);
            execute = execution;
            success = resSuccess;
            failed = resFailed;
            extraInfo = extra;
        }

        public void             DoExecute()
        {
            if (execute != null)
            {
                execute();
            }
        }
        public void             DoSuccess() 
        {
            if ( success != null)
            {
                success();
            }
        }
        public void             DoFailed()
        {
            if ( failed != null)
            {
                failed();
            }
        }
        public void             AddExecute(Action addAction)
        {
            if (addAction == null) return;

            if ( execute == null)
            {
                execute = addAction;
            }
            else
            {
                execute += addAction;
            }
        }
        public void             AddSuccess(Action addAction)
        {
            if (addAction == null) return;

            if (success == null)
            {
                success = addAction;
            }
            else
            {
                success += addAction;
            }
        }
        public void             AddFailed(Action addAction)
        {
            if (addAction == null) return;

            if (failed == null)
            {
                failed = addAction;
            }
            else
            {
                failed += addAction;
            }
        }

        public Action           GetExecute()  { return execute; }
        public Action           GetSuccess()  { return success; }
        public Action           GetFailed()   { return failed; }
        public MessageExtraInfo GetExtraInfo() { return extraInfo; }

    }
}