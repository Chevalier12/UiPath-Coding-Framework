using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using UiPath.Core;
using UiPath.Core.Activities;

namespace Inscriber
{
    public class Job_GetValue : Activity
    {
        public IDictionary<string, object> input = new Dictionary<string, object>();
        public InArgument<string> SelectorArgument { get; set; }
        public OutArgument<string> ValueArgument { get; set; }
        public InArgument<WaitForReady> WaitForReadyArgument { get; set; }
        public InArgument<int> TimeoutArgument { get; set; }
        public InArgument<bool> ContineOnErrorArgument { get; set; }

        public IDictionary<string, object> Run(
            WaitForReady waitForReady = WaitForReady.NONE,
            string Selector = null,
            UiElement uiElement = null,
            int TimeOut = 30,
            bool ContinueOnError = false
        )
        {
            WaitForReadyArgument = waitForReady;
            SelectorArgument = Selector;
            TimeoutArgument = TimeOut;
            ValueArgument = new OutArgument<string>();
            ContineOnErrorArgument = ContinueOnError;


            if (Selector == null && uiElement != null)
                SelectorArgument = uiElement.Selector.ToString();
            else if (Selector == null && uiElement == null)
                throw new SystemException("No Selector or uiElement parameter provided.");

            Implementation = () => new Sequence
            {
                Activities =
                    {
                        new GetValue
                        {
                            Target = new Target
                            {
                                WaitForReady = new InArgument<WaitForReady>(activityContext =>
                                    WaitForReadyArgument.Get(activityContext)),
                                Selector = new InArgument<string>(activityContext =>
                                    SelectorArgument.Get(activityContext)),
                                TimeoutMS = new InArgument<int>(activityContext =>
                                    TimeoutArgument.Get(activityContext))
                            },
                            ContinueOnError = new InArgument<bool>(activityContext =>
                                ContineOnErrorArgument.Get(activityContext)),
                            Value = new OutArgument<string>(activityContext =>
                                ValueArgument.Get(activityContext))
                        }
                    }
            };

            input.Add(nameof(WaitForReadyArgument), waitForReady);
            input.Add(nameof(SelectorArgument), Selector);
            input.Add(nameof(TimeoutArgument), TimeOut);
            input.Add(nameof(ContineOnErrorArgument), ContinueOnError);

            IDictionary<string, object> output = new Dictionary<string, object>();
            try
            {
                output = WorkflowInvoker.Invoke(this, input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Press any key to continue or to exit...");
                Console.ReadLine();
            }

            return output;
        }
    }
}