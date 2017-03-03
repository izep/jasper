﻿using System;

namespace JasperBus.Runtime.Invocation
{
    public class ChainSuccessContinuation : IContinuation
    {
        public static readonly ChainSuccessContinuation Instance = new ChainSuccessContinuation();

        private ChainSuccessContinuation()
        {

        }

        public void Execute(Envelope envelope, IEnvelopeContext context, DateTime utcNow)
        {
            try
            {
                context.SendAllQueuedOutgoingMessages();

                envelope.Callback.MarkSuccessful();
            }
            catch (Exception ex)
            {
                context.SendFailureAcknowledgement(envelope, "Sending cascading message failed: " + ex.Message);
                context.Error(envelope.CorrelationId, ex.Message, ex);

                envelope.Callback.MoveToErrors(new ErrorReport(envelope, ex));
            }
        }

    }
}