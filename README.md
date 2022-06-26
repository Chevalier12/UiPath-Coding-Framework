# UiPath Coding Framework

A port of the UiPath.Core.Activities activities into C# code, this library will allow you to run any activity in the UiPath.Core.Activities packages.

# How to use?

This is an example of a GetValue implementation in C#:

```C#
var getValue = new Job_GetValue().Run
            (
            WaitForReady.COMPLETE,
            "<wnd app='notepad.exe' cls='Notepad' title='*Library.txt - Notepad' /><wnd aaname='Horizontal' cls='Edit' /><ctrl name='Text Editor' role='editable text'/>"
            );
```

Simply create the object which has the prefix **Job_<your activity name>** with the **Run** method and voila, your activity will run immediately.

A lot of classes created in this framework have already defined values for them, consider them "default" values, some of them are initialized and may be counterproductive for your workflow such as the WaitForReady value which in this framework is always set by default to NONE, always make sure your properties are properly controlled in order to have the best experience.
            
The issue of adding anchors to selectors is not resolved, unless you somehow generate a dynamic selector using strings (which is much easier to do that in Studio), if you want to add anchors, you will need to copy the finished selector from UiPath Studio for now OR, use the AnchorBase activity.

# How to contribute

Each one of the classes in UiPath.Core.Activities have properties that need to be initialized properly in an Activity class, see below for a snippet of the **Get Text** activity:

```C#
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
                Console.ReadKey();
            }

            return output;
        }
    }
```
                     

The **Get Text** activity of UiPath.Core.Activities has the following properties:

**Continue On Error** of type **InArgument of Type Boolean**

**Display Name** of type **String** - useless in the context of the framework.

**Target** of type **Target**

**Target** has the following properties:

- **Clipping Region** of type **Region** - useless in the context of the framework.
  
- **Element** of type **InArgument of Type UiElement**
  
- **Selector** of type **InArgument of Type String**
  
- **Timeout** of type **InArgument of Type Int32**
  
- **WaitForReady** of type **InArgument of Type WaitForReady**
  

**Private** of type **Boolean** - useless in the context of the framework.

**Value** of type **OutArgument**

A few of these properties you can see below in code:

```C#
public InArgument<string> SelectorArgument { get; set; }
public OutArgument<string> ValueArgument { get; set; }
public InArgument<WaitForReady> WaitForReadyArgument { get; set; }
public InArgument<int> TimeoutArgument { get; set; }
public InArgument<bool> ContineOnErrorArgument { get; set; }
```
        
To properly contribute to this project you have to create each of the activities found in UiPath.Core.Activities by using the template provided in the snippet below and by writing the class inside of the **UiPathCodeWorks.cs** file
            
# Class Template
```
public class Job_<UiPathClassName>
{
            <Argument Properties>
            
            <Run Method of IDictionary<string,object> type with parameters named after the UiPath Activity Properties>
                        
            <Assigning values to InArguments based on parameters>
                       
            <Initializing the output dictionary>
            <Adding the Arguments to the input dictionary>
                        
            <Activity Implementation Body>
                        
            <Try Catch Snippet Copy/Paste with changed Text to reflect the activity name>    

            <Return output dictionary>

}
```
   

# What are the advantages of using the coding framework over UiPath Studio traditional method?

1. The code is much more organized than just drag & dropping activities.
2. The code activities run 50% to 200% faster than the normal activities.
3. Managing the code with a team of programmers and maintaining it is much easier than in UiPath Studio.
4. You can run as many workflows as your CPU can handle because there are no dependencies on UiRobot.exe or UiPath Assistant or any other UiPath software.
5. You can create your robots in executables and deploy them on stations that do not have any UiPath software installed.
6. You have access to the entirety of .NET capabilities (limited to .NET Framework 4.6 -> 4.8).

# Is this breaking the UiPath ToS?

This is something I am still in discussion with UiPath to find out, when I find out I will edit this section with my answer, regardless if it is legal or not, these library will be open source and usable by all who desire to use it for their own personal (or business if it is ok to use) use.
