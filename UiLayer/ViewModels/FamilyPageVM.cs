using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using BusinessLogicLayer;
using Utils;

namespace UiLayer
{
    class FamilyPageVM : ViewModelBase
    {
        private readonly CFamily _family;
        private ICollection<CCategoryNotifyObject> categories;
        private ICollection<CPaymentNotifyObject> payments;

        public String FamilyName { get => _family?.FamilyName; }
        public Int32? FamilyId { get => _family?.FamilyID; }
        public String Budget { get => _family?.Budget.ToString() + " у.е."; }
        public String MembersCounter { get => _family?.MembersCounter.ToString(); }
        public String SpendedThisMonth { get => Categories?.Select(c => c.SpendedThisMonth).Sum().ToString() + " у.е."; }
        public Boolean IsBudgetExceeded { get => _family?.Budget < Categories?.Select(c => c.SpendedThisMonth).Sum(); }
        //Big properties
        public ICollection<CCategoryNotifyObject> Categories
        {
            get
            {
                if (_family == null)
                    return null;

                if (categories != null)
                    return categories;

                categories = new List<CCategoryNotifyObject>();
                Dictionary<String, List<CSubCategoryNotufyObject>> CategorySubCategoriesDict = new Dictionary<String, List<CSubCategoryNotufyObject>>();

                ICollection<CCategoryType> types = (new CBllFacadeForUIProxy()).GetAllRegisterdCategories();
                foreach (CCategoryType tp in types)
                    CategorySubCategoriesDict.Add(tp.Title, new List<CSubCategoryNotufyObject>());

                foreach (CSubCategory subCat in _family.FamilyMembers.SelectMany(p => p.SubCategories))
                {

                    CSubCategoryNotufyObject subCategoryNO = CategorySubCategoriesDict[subCat.CategoryType.Title].FirstOrDefault(sc => sc.Title.Equals(subCat.Title));
                    if(subCategoryNO == null)
                    {
                        subCategoryNO = new CSubCategoryNotufyObject(subCat.SubCategoryID, subCat.Title, subCat.Description);
                        CategorySubCategoriesDict[subCat.CategoryType.Title].Add(subCategoryNO);
                    }

                    foreach (CPayment pm in subCat.Payments)
                        subCategoryNO.Payments.Add(new CPaymentNotifyObject(pm.PaymentID, pm.Date, subCat.CategoryType.Title, subCat.Title, pm.Spended));
                }

                foreach (KeyValuePair<string, List<CSubCategoryNotufyObject>> pair in CategorySubCategoriesDict)
                    categories.Add(new CCategoryNotifyObject(pair.Key, CSConverterToObservableCollection.ConvertList(pair.Value)));

                return categories;
            }
        }
        public ICollection<CPaymentNotifyObject> Payments
        {
            get
            {
                if (_family == null)
                    return null;

                if (payments == null)
                    payments = Categories.SelectMany(c => c.SubCategories.SelectMany(sc => sc.Payments)).ToList();

                return payments;
            }
        }

        public FamilyPageVM(String token, CRelayCommand logoutCommand, CRelayCommand redirectToMyPageCommand)
        {
            IBllFacadeForUI bllFacade = new CBllFacadeForUIProxy();
            CFamilyAccessResult res = bllFacade.GetFamily(token);

            _family = res.Family;

            if(res.IsTokenExpired)
            {
                MessageBox.Show("You have been inactive for too long. Login again!");
                logoutCommand.Execute(null);
                return;
            }
            if(res.IsNotExisted)
            {
                MessageBox.Show("Failed to acces your family data");
                redirectToMyPageCommand.Execute(null);
                return;
            }
            if(res.IsAccessDenied)
            {
                MessageBox.Show("You do not have permissions to access to this family data!");
                redirectToMyPageCommand.Execute(null);
                return;
            }
        }
    }
}
