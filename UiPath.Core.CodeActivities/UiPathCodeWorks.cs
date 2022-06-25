using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using UiPath.Core;
using UiPath.Core.Activities;
using UiPath.UIAutomationNext.Enums;

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
            UiElement UiElement = null,
            int TimeOut = 30,
            bool ContinueOnError = false
        )
        {

            //Assigning values and initializing dictionary
            WaitForReadyArgument = waitForReady;
            SelectorArgument = Selector;
            TimeoutArgument = TimeOut;
            ValueArgument = new OutArgument<string>();
            ContineOnErrorArgument = ContinueOnError;

            IDictionary<string, object> output = new Dictionary<string, object>();
            input.Add(nameof(WaitForReadyArgument), waitForReady);
            input.Add(nameof(SelectorArgument), Selector);
            input.Add(nameof(TimeoutArgument), TimeOut);
            input.Add(nameof(ContineOnErrorArgument), ContinueOnError);

            if (Selector == null && UiElement != null)
                SelectorArgument = UiElement.Selector.ToString();
            else if (Selector == null && UiElement == null)
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


            try
            {
                Console.WriteLine("");
                Console.WriteLine("Starting Get Text activity");
                output = WorkflowInvoker.Invoke(this, input);
                Console.WriteLine("");
                Console.WriteLine("Finished Get Text activity.");
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

    public class Job_Click : Activity
    {
        public IDictionary<string, object> input = new Dictionary<string, object>();
        public InArgument<Boolean> ContinueOnErrorArgument { get; set; }
        public InArgument<int> DelayAfterArgument { get; set; }
        public InArgument<int> DelayBeforeArgument { get; set; }
        public InArgument<ClickType> ClickTypeArgument { get; set; }
        public InArgument<MouseButton> MouseButtonArgument { get; set; }
        public InArgument<UiElement> UiElementArgument { get; set; }
        public InArgument<String> SelectorArgument { get; set; }
        public InArgument<int> TimeoutMSArgument { get; set; }
        public InArgument<WaitForReady> WaitForReadyArgument { get; set; }
        public InArgument<CursorMotionType> CursorMotionTypeArgument { get; set; }
        public InArgument<KeyModifiers> KeyModifiersArgument { get; set; }
        public InArgument<bool> SendWindowMessagesArgument { get; set; }
        public InArgument<bool> SimulateClickArgument { get; set; }

        public IDictionary<string, object> Run(
            bool ContinueOnError = false,
            int DelayAfter = 300,
            int DelayBefore = 200,
            ClickType ClickType = ClickType.CLICK_SINGLE,
            MouseButton MouseButton = MouseButton.BTN_LEFT,
            String Selector = null,
            UiElement UiElement = null,
            int TimeOutMS = 3000,
            WaitForReady WaitForReady = WaitForReady.NONE,
            CursorMotionType CursorMotionType = CursorMotionType.Instant,
            KeyModifiers KeyModifiers = KeyModifiers.None,
            bool SendWindowMessages = false,
            bool SimulateClick = false
        )
        {
            //Assigning values and initializing dictionary
            ContinueOnErrorArgument = ContinueOnError;
            DelayAfterArgument = DelayAfter;
            DelayBeforeArgument = DelayBefore;
            ClickTypeArgument = ClickType;
            MouseButtonArgument = MouseButton;
            SelectorArgument = Selector;
            TimeoutMSArgument = TimeOutMS;
            WaitForReadyArgument = WaitForReady;
            CursorMotionTypeArgument = CursorMotionType;
            KeyModifiersArgument = KeyModifiers;
            SendWindowMessagesArgument = SendWindowMessages;
            SimulateClickArgument = SimulateClick;

            IDictionary<string, object> output = new Dictionary<string, object>();
            input.Add(nameof(ContinueOnErrorArgument), ContinueOnError);
            input.Add(nameof(DelayAfterArgument), DelayAfter);
            input.Add(nameof(DelayBeforeArgument), DelayBefore);
            input.Add(nameof(ClickTypeArgument), ClickType);
            input.Add(nameof(MouseButtonArgument), MouseButton);
            input.Add(nameof(SelectorArgument), Selector);
            input.Add(nameof(TimeoutMSArgument), TimeOutMS);
            input.Add(nameof(WaitForReadyArgument), WaitForReady);
            input.Add(nameof(CursorMotionTypeArgument), CursorMotionType);
            input.Add(nameof(KeyModifiersArgument), KeyModifiers);
            input.Add(nameof(SendWindowMessagesArgument), SendWindowMessages);
            input.Add(nameof(SimulateClickArgument), SimulateClick);


            if (Selector == null && UiElement != null)
                SelectorArgument = UiElement.Selector.ToString();
            else if (Selector == null && UiElement == null)
                throw new SystemException("No Selector or uiElement parameter provided.");

            Implementation = () => new Sequence
            {
                Activities =
                {
                    new Click()
                    {
                        ContinueOnError = new InArgument<bool>((activityContext)=> ContinueOnErrorArgument.Get(activityContext)),
                        DelayMS = new InArgument<int>((activityContext)=> DelayAfterArgument.Get(activityContext)),
                        DelayBefore = new InArgument<int>((activityContext)=> DelayBeforeArgument.Get(activityContext)),
                        ClickType = new InArgument<ClickType>((activityContext) => ClickTypeArgument.Get(activityContext)),
                        MouseButton = new InArgument<MouseButton>((activityContext)=> MouseButtonArgument.Get(activityContext)),
                        Target = new Target
                        {
                            Selector = new InArgument<string>((activityContext)=> SelectorArgument.Get(activityContext)),
                            TimeoutMS = new InArgument<int>((activityContext)=> TimeoutMSArgument.Get(activityContext)),
                            WaitForReady = new InArgument<WaitForReady>((activityContext)=> WaitForReadyArgument.Get(activityContext))
                        },
                        CursorMotionType = new InArgument<CursorMotionType>((activityContext)=> CursorMotionTypeArgument.Get(activityContext)),
                        KeyModifiers = new InArgument<KeyModifiers>((activityContext)=> KeyModifiersArgument.Get(activityContext)),
                        SendWindowMessages = new InArgument<bool>((activityContext)=> SendWindowMessagesArgument.Get(activityContext)),
                        SimulateClick = new InArgument<bool>((activityContext)=> SimulateClickArgument.Get(activityContext))
                        
                        
                    }
                }
            };

            
            try
            {
                Console.WriteLine("");
                Console.WriteLine("Starting Click activity");
                output = WorkflowInvoker.Invoke(this, input);
                Console.WriteLine("");
                Console.WriteLine("Finished Click activity.");
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