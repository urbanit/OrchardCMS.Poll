using Orchard;
using Orchard.UI.Navigation;

namespace Urbanit.Polls
{
    public class AdminMenu : Component, INavigationProvider
    {
        public string MenuName { get { return "admin"; } }


        public void GetNavigation(NavigationBuilder builder)
        {
            builder
                .AddImageSet("Polls")
                .Add(
                    T("Polls Dashboard"),
                    "7",
                    menu => menu.Add(
                        T("Polls Dashboard"),
                        "0",
                        item => item.Action(
                            "Index",
                            "Admin",
                            new { area = "Urbanit.Polls" }).Permission(Permissions.ManagePollsPermission)));
        }
    }
}