using Inscriber;

namespace UiPath.Core.CodeActivities
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var getValue = new Job_GetValue().Run
            //(
            //    WaitForReady.COMPLETE,
            //    "<wnd app='notepad.exe' cls='Notepad' title='*Library.txt - Notepad' /><wnd aaname='Horizontal' cls='Edit' /><ctrl name='Text Editor' role='editable text' />"
            //);

            var click = new Job_Click().Run
            (
                Selector:
                "<html app='chrome.exe' title='Chevalier12/UiPath-Coding-Framework' /><webctrl aaname='                   Star           1    ' parentid='repository-container-header' tag='BUTTON' type='submit' />",
                WaitForReady: WaitForReady.COMPLETE
                );

        }
    }
}