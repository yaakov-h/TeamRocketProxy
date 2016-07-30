using System;
using System.Windows.Forms;

namespace TeamRocketProxy
{
    static class FormExtensions
    {
        // It's kind of pathetic that his extension method is needed, but it coerces the compiler
        // into casting Action (from a lambda expression) into a Delegate.
        public static IAsyncResult BeginInvoke(this Form form, Action action)
            => form.BeginInvoke(action);
    }
}
