using System;

namespace EntrepotScreen.Tools
{
    internal interface IClosableViewModel
    {
        event EventHandler CloseWindowEvent;
    }
}