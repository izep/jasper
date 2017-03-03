﻿using System;
using JasperBus.Model;
using JasperBus.Runtime;
using JasperBus.Runtime.Invocation;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace JasperBus.Tests.Runtime.Invocation
{
    public class ChainSuccessContinuationTester
    {
        private Envelope theEnvelope = ObjectMother.Envelope();
        private IEnvelopeContext theEnvelopeContext = Substitute.For<IEnvelopeContext>();


        public ChainSuccessContinuationTester()
        {
            theEnvelope = ObjectMother.Envelope();
            theEnvelope.Message = new object();

            ChainSuccessContinuation.Instance
                .Execute(theEnvelope, theEnvelopeContext, DateTime.UtcNow);
        }

        [Fact]
        public void should_mark_the_message_as_successful()
        {
            theEnvelope.Callback.Received().MarkSuccessful();
        }

        [Fact]
        public void should_send_off_all_queued_up_cascaded_messages()
        {
            theEnvelopeContext.Received().SendAllQueuedOutgoingMessages();
        }

    }

    public class ChainSuccessContinuation_failure_handling_Tester
    {
        private Envelope theEnvelope = ObjectMother.Envelope();
        private IEnvelopeContext theEnvelopeContext = Substitute.For<IEnvelopeContext>();
        private readonly Exception theException = new DivideByZeroException();

        public ChainSuccessContinuation_failure_handling_Tester()
        {
            theEnvelopeContext.When(x => x.SendAllQueuedOutgoingMessages())
                .Throw(theException);

            ChainSuccessContinuation.Instance
                .Execute(theEnvelope, theEnvelopeContext, DateTime.UtcNow);
        }


        [Fact]
        public void should_move_the_envelope_to_the_error_queue()
        {
            theEnvelope.Callback.Received().MoveToErrors(new ErrorReport(theEnvelope, theException));
        }

        [Fact]
        public void should_log_the_exception()
        {
            var expected = new ErrorReport(theEnvelope, theException);

            theEnvelope.Callback.Received().MoveToErrors(expected);
        }

        [Fact]
        public void should_send_a_failure_ack()
        {
            var message = "Sending cascading message failed: " + theException.Message;
            theEnvelopeContext.Received().SendFailureAcknowledgement(theEnvelope, message);

        }
    }

}