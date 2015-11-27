using Foundation;
using UIKit;

namespace DynamicTableCellHeight.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds) {RootViewController = new MyRootViewController()};

            // make the window visible
            Window.MakeKeyAndVisible();

            return true;
        }
    }
}


