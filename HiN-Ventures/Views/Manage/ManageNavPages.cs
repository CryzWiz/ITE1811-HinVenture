using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HiN_Ventures.Views.Manage
{
    public static class ManageNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Index => "Index";

        public static string BitCoin => "BitCoin";

        public static string ChangePassword => "ChangePassword";

        public static string ExternalLogins => "ExternalLogins";

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string ManageKlient => "ManageKlient";

        public static string ManageFreelance => "ManageFreelance";

        public static string ManagePayments => "ManagePayments";

        public static string ManageProjects => "ManageProjects";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string BitCoinNavClass(ViewContext viewContext) => PageNavClass(viewContext, BitCoin);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        public static string ManageKlientNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageKlient);

        public static string ManageFreelanceNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageFreelance);
        
        public static string ManagePaymentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManagePayments);

        public static string ManageProjectsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageProjects);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
