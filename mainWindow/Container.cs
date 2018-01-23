using System.Windows.Controls;

namespace mainWindow
{
    public class Container : StackPanel
    {
        public Container()
        {
            Grid.SetIsSharedSizeScope(this, true);
        }
    }
}
