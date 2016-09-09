using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace OrganizerTask1.VMUnitTest.Base
{
    public abstract class WindowObject
    {
        protected Window window;

        protected WindowObject(Window window)
        {
            this.window = window;
        }

        public Button GetButton(string name)
        {
            return window.Get<Button>(name);
        }

        public Button GetButtonByText(string text)
        {
            return window.Get<Button>(SearchCriteria.ByText(text));
        }

        public TextBox GetTextBlock(string title)
        {
            return window.Get<TextBox>(title);
        }

    }
}
