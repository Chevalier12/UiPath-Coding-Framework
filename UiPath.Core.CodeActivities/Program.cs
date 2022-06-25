using System;
using System.Linq;
using Inscriber;
using UiPath.Core.Activities;

namespace UiPath.Core.CodeActivities
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var getValue = new Job_GetValue().Run
            (
                WaitForReady.COMPLETE,
                "<wnd app='notepad.exe' cls='Notepad' title='*Library.txt - Notepad' /><wnd aaname='Horizontal' cls='Edit' /><ctrl name='Text Editor' role='editable text' />"
            );

        }
    }
}