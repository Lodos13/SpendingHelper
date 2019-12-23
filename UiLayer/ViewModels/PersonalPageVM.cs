using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using NLog;

using BusinessLogicLayer;
using Utils;

namespace UiLayer
{
    class PersonalPageVM :ViewModelBase
    {
        Logger log = LogManager.GetCurrentClassLogger();

        private readonly String _token;

        private readonly CPerson _personInfo;
        private ObservableCollection<CCategoryNotifyObject> _categories;
        private ObservableCollection<CPaymentNotifyObject> _payments;

        public Boolean IsInFamily { get => _personInfo.Family == null; }

        public CPerson Peson { get => _personInfo; }
        public String Name { get => _personInfo.Name; }
        public String FamilyName { get => _personInfo.Family?.FamilyName ?? "No family registered"; }
        public String Budget { get => _personInfo.Budget.ToString() + " у.е."; }
        public String SpendedThisMonth { get => Categories.Select(c => c.SpendedThisMonth).Sum().ToString() + " у.е."; }
        public Boolean IsBudgetExceeded { get => _personInfo.Budget < Categories.Select(c => c.SpendedThisMonth).Sum(); }
        //Big properties
        public ObservableCollection<CCategoryNotifyObject> Categories
        {
            get
            {
                if (_categories != null)
                    return _categories;

                try
                {
                    _categories = new ObservableCollection<CCategoryNotifyObject>();
                    Dictionary<String, List<CSubCategoryNotufyObject>> CategorySubCategoriesDict = new Dictionary<String, List<CSubCategoryNotufyObject>>();

                    ICollection<CCategoryType> types = (new CBllFacadeForUIProxy()).GetAllRegisterdCategories();
                    foreach (CCategoryType tp in types)
                        CategorySubCategoriesDict.Add(tp.Title, new List<CSubCategoryNotufyObject>());

                    foreach (CSubCategory subCat in _personInfo.SubCategories)
                    {
                        CSubCategoryNotufyObject subCategoryNO = new CSubCategoryNotufyObject(subCat.SubCategoryID, subCat.Title, subCat.Description);

                        foreach (CPayment pm in subCat.Payments)
                            subCategoryNO.Payments.Add(new CPaymentNotifyObject(pm.PaymentID, pm.Date, subCat.CategoryType.Title, subCat.Title, pm.Spended));

                        CategorySubCategoriesDict[subCat.CategoryType.Title].Add(subCategoryNO);
                    }

                    foreach (KeyValuePair<string, List<CSubCategoryNotufyObject>> pair in CategorySubCategoriesDict)
                        _categories.Add(new CCategoryNotifyObject(pair.Key, CSConverterToObservableCollection.ConvertList(pair.Value)));
                }
                catch (Exception e)
                {
                    log.Error(e, "Произошло непредвиденное исключение в свойстве Categories: {0}", e.Message);
                    MessageBox.Show("Произошло непредвиденное исключение в свойстве Categories: " + e.Message);
                }
                
                return _categories;
            }

            set => OnPropertyChanged();
        }
        public ObservableCollection<CPaymentNotifyObject> Payments
        {
            get
            {
                if (_payments == null)
                {
                    List<CPaymentNotifyObject> listPayment = Categories.SelectMany(c => c.SubCategories.SelectMany(sc => sc.Payments)).ToList();
                    _payments = CSConverterToObservableCollection.ConvertList(listPayment);
                }

                return _payments;
            }

            set => OnPropertyChanged();
        }

        public PersonalPageVM(String token, CRelayCommand  logoutCommand)
        {
            _token = token;

            IBllFacadeForUI bllFacade = new CBllFacadeForUIProxy();
            _personInfo = bllFacade.GetPerson(token);

            if (_personInfo == null)
            {
                MessageBox.Show("An error occurred while loading the page. Try login again.");
                logoutCommand.Execute("MyPersonalPage");
            }
        }


        private CRelayCommand _addNewPaymentCommand;
        public CRelayCommand AddNewPaymentCommand
        {
            get
            {
                return _addNewPaymentCommand ?? (_addNewPaymentCommand = new CRelayCommand((o) =>
                {
                    try
                    {
                        PaymentEditForm form = new PaymentEditForm(_categories);
                        if (form.ShowDialog() == true)
                        {
                            DateTime date = (DateTime)form.Date;
                            CCategoryNotifyObject category = form.Category;
                            CSubCategoryNotufyObject subCategory = form.SubCategory;
                            Decimal sum = form.Sum;

                            if(date == null || category == null || subCategory == null || sum <= 0)
                            {
                                MessageBox.Show("При создании платежа необходимо заполнить все поля. Сумма не может быть меньше или равна 0");
                                return;
                            }

                            //Add to DB
                            IBllFacadeForUI bllFacade = new CBllFacadeForUIProxy();
                            Int32 paymentId = bllFacade.AddPayment(_token, date, category.Title, subCategory.Title, sum);
                            if (paymentId == 0)
                            {
                                MessageBox.Show("Звезды сказали что вы внесли неверные данные, регистрация платежа откланена, попробуйте снова");
                                return;
                            }

                            //Add to view
                            CPaymentNotifyObject payment = new CPaymentNotifyObject(paymentId, date, category.Title, subCategory.Title, sum);

                            CCategoryNotifyObject selectedCategory = Categories.Where(c => c.Title == category.Title).FirstOrDefault();

                            CSubCategoryNotufyObject selectedSubCategory = selectedCategory.SubCategories.Where(s => s.Title == subCategory.Title).FirstOrDefault();
                            selectedSubCategory.Payments.Add(payment);

                            Payments.Add(payment);

                            selectedCategory.Update();
                            selectedSubCategory.Update();
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(e, "Произошло непредвиденное исключение при поптыке добавить платеж: {0}", e.Message);
                        MessageBox.Show("Произошло непредвиденное исключение при поптыке добавить платеж: " + e.Message);
                    }
                }));
            }
        }

        private CRelayCommand _editPaymentCommand;
        public CRelayCommand EditPaymentCommand
        {
            get
            {
                return _editPaymentCommand ?? (_editPaymentCommand = new CRelayCommand((o) =>
                {
                    try
                    {
                        CPaymentNotifyObject payment = o as CPaymentNotifyObject;
                        if(payment == null)
                        {
                            MessageBox.Show("Платеж для редактирования не был выбран, убедитесь что вы выбрали платеж");
                            return;
                        }
                        PaymentEditForm form = new PaymentEditForm(_categories, payment);
                        if (form.ShowDialog() == true)
                        {
                            CPaymentNotifyObject paymentInCollection = _payments.Where(p => p.Category == payment.Category && p.Date == payment.Date && p.SubCategory == payment.SubCategory).FirstOrDefault();
                            if(paymentInCollection == null)
                            {
                                log.Warn("При попытке редактировать платеж программа не смогла найти платеж пользователя");
                                MessageBox.Show("При попытке редактировать платеж программа не смогла найти платеж пользователя");
                                return;
                            }

                            DateTime newDate = (DateTime)form.Date;
                            CCategoryNotifyObject newCategory = form.Category;
                            CSubCategoryNotufyObject newSubCategory = form.SubCategory;
                            Decimal newSum = form.Sum;

                            if (newDate == null || newCategory == null || newSubCategory == null || newSum <= 0)
                            {
                                MessageBox.Show("При редактировании платежа необходимо заполнить все поля. Сумма не может быть меньше ил равна 0");
                                return;
                            }

                            //Edit in DB
                            IBllFacadeForUI bllFacade = new CBllFacadeForUIProxy();
                            if(bllFacade.EditPayment(_token, paymentInCollection.PaymentId, newDate, newCategory.Title, newSubCategory.Title, newSum))
                                MessageBox.Show("Платеж отредактирован! Будте внимательны, не только вы любите редактировать суммы в платежах =)");
                            else
                                MessageBox.Show("Редактирование платежа закончилось неудачей. Причина? А кто его знает, может у удаляльщика сегодня выходной?");


                            //Edit in View
                            CCategoryNotifyObject oldCategory = Categories.Where(c => c.Title == payment.Category).FirstOrDefault();
                            CSubCategoryNotufyObject oldSubCategory = oldCategory.SubCategories.Where(s => s.Title == payment.SubCategory).FirstOrDefault();

                            oldSubCategory.Payments.Remove(payment);

                            payment.Date = newDate;
                            payment.Category = newCategory.Title;
                            payment.SubCategory = newSubCategory.Title;
                            payment.Spended = newSum;

                            newSubCategory.Payments.Add(payment);

                            oldCategory.Update();
                            oldSubCategory.Update();
                            newCategory.Update();
                            newSubCategory.Update();
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(e, "Произошло непредвиденное исключение при поптыке редактировать платеж: {0}", e.Message);
                        MessageBox.Show("Произошло непредвиденное исключение при поптыке добавить платеж: " + e.Message);
                    }
                }));
        }
        }

        private CRelayCommand _deletePaymentCommand;
        public CRelayCommand DeletePaymentCommand
        {
            get
            {
                return _deletePaymentCommand ?? (_deletePaymentCommand = new CRelayCommand((o) =>
                {
                    try
                    {
                        CPaymentNotifyObject payment = o as CPaymentNotifyObject;
                        if (payment == null)
                        {
                            MessageBox.Show("Платеж для удаления не был выбран, убедитесь что вы выбрали платеж");
                            return;
                        }
                        if(MessageBox.Show($"Вы уверены что хотите удалить выбранный платеж от {payment.Date}?", "Подтвердите удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            //Edit in DB
                            IBllFacadeForUI bllFacade = new CBllFacadeForUIProxy();
                            if(bllFacade.DeletePayment(_token, payment.PaymentId))
                                MessageBox.Show("Платеж удален! Сделаем вид что мы этого не видели.");
                            else
                                MessageBox.Show("К сожалению во время попытки удаления платежа нас засек инспектор ведомства \"Открытые платежи\" и заставил вернуть все как былою");

                            
                            //Edit in View
                            CCategoryNotifyObject selectedCategory = Categories.Where(c => c.Title == payment.Category).FirstOrDefault();
                            if (selectedCategory == null)
                                return;

                            CSubCategoryNotufyObject selectedSubCategory = selectedCategory.SubCategories.Where(s => s.Title == payment.SubCategory).FirstOrDefault();
                            if (selectedSubCategory == null)
                                return;

                            selectedSubCategory.Payments.Remove(payment);
                            Payments.Remove(payment);

                            selectedCategory.Update();
                            selectedSubCategory.Update();
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(e, "Произошло непредвиденное исключение при поптыке удалить платеж: {0}", e.Message);
                        MessageBox.Show("Произошло непредвиденное исключение при поптыке удалить платеж: " + e.Message);
                    }
                }));
            }
        }


        private CRelayCommand _createFamilyCommand;
        public CRelayCommand CreateFamilyCommand
        {
            get
            {
                return _createFamilyCommand ?? (_createFamilyCommand = new CRelayCommand((o) =>
                {
                    try
                    {
                        FamilyEditForm form = new FamilyEditForm();
                        if(form.ShowDialog() == true)
                        {
                            String familyName = form.FamilyName;
                            Int32 budget = form.Budget;

                            if(budget < 0)
                            {
                                MessageBox.Show("Бюджет семьи не может быть меньше 0. Семья не создана");
                                return;
                            }

                            IBllFacadeForUI bllFacade = new CBllFacadeForUIProxy();

                            if (!bllFacade.CreateFamily(_token, familyName, budget))
                            {
                                MessageBox.Show("Создание семьи закончилось неудачей");
                                return;
                            }

                            MessageBox.Show("Семья успешно создана");

                            CFamilyAccessResult familyAccessResult = bllFacade.GetFamily(_token);
                            _personInfo.Family = familyAccessResult.Family;
                            OnPropertyChanged("FamilyName");
                            OnPropertyChanged("IsInFamily");
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(e, "Произошло непредвиденное исключение при попытке создать новую семью: {0}", e.Message);
                        MessageBox.Show("Произошло непредвиденное исключение при попытке удалить платеж: " + e.Message);
                    }
                }));
            }
        }

        private CRelayCommand _joinFamilyCommand;
        public CRelayCommand JoinFamilyCommand
        {
            get
            {
                return _joinFamilyCommand ?? (_joinFamilyCommand = new CRelayCommand((o) =>
                {
                    try
                    {
                        SingleIntForm form = new SingleIntForm("введите id семьи");
                        if (form.ShowDialog() == true)
                        {
                            Int32 familyId = form.Result;
                            if (familyId < 0)
                            {
                                MessageBox.Show("Вы ввели неверные данные");
                                return;
                            }

                            IBllFacadeForUI bllFacade = new CBllFacadeForUIProxy();
                            if (!bllFacade.JoinFamily(_token, familyId))
                            {
                                MessageBox.Show("Вас не приняли в семью. Это ведь не воображаемая семья, верно?");
                                return;
                            }

                            MessageBox.Show("Вы вошли в семью, это ведь не мафиозная семья, да?");

                            CFamilyAccessResult familyAccessResult = bllFacade.GetFamily(_token);
                            _personInfo.Family = familyAccessResult.Family;
                            OnPropertyChanged("FamilyName");
                            OnPropertyChanged("IsInFamily");
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(e, "Произошло непредвиденное исключение при поптыке присоединиться к семье: {0}", e.Message);
                        MessageBox.Show("Произошло непредвиденное исключение при поптыке присоединиться к семье: " + e.Message);
                    }
                }));
            }
        }

        private CRelayCommand _leaveFamilyCommand;
        public CRelayCommand LeaveFamilyCommand
        {
            get
            {
                return _leaveFamilyCommand ?? (_leaveFamilyCommand = new CRelayCommand((o) =>
                {
                    try
                    {
                        if (MessageBox.Show($"Вы уверены что хотите покинуть семью?", "Подтвердите...", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            IBllFacadeForUI bllFacade = new CBllFacadeForUIProxy();

                            if(bllFacade.LeaveFamily(_token))
                                MessageBox.Show("Вы покинули свою семью");
                            else
                                MessageBox.Show("Попытка покинуть семью закончилась неудачей. Не так, то это просто, а?");

                            _personInfo.Family = null;
                            OnPropertyChanged("FamilyName");
                            OnPropertyChanged("IsInFamily");
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(e, "Произошло непредвиденное исключение при поптыке покинуть семью. (Не над так делать...) {0}", e.Message);
                        MessageBox.Show("Произошло непредвиденное исключение при поптыке покинуть семью. (Не над так делать...) " + e.Message);
                    }
                }));
            }
        }


        private CRelayCommand _addNewSubCategory;
        public CRelayCommand AddNewSubCategory
        {
            get
            {
                return _addNewSubCategory ?? (_addNewSubCategory = new CRelayCommand((o) =>
                {
                    try
                    {
                        SubCategoryEditForm form = new SubCategoryEditForm(_categories);
                        if (form.ShowDialog() == true)
                        {
                            String categoryTitle = form.CategoryTitle;
                            String subCategoryTitle = form.SubCategoryTitle;
                            String subCategoryDescription = form.Description;

                            if (String.IsNullOrEmpty(subCategoryTitle) || String.IsNullOrEmpty(subCategoryDescription) || String.IsNullOrEmpty(categoryTitle))
                            {
                                MessageBox.Show("При создании новой подкатегории необходимо заполнить/выбрать все поля.");
                                return;
                            }

                            //Add to DB
                            IBllFacadeForUI bllFacade = new CBllFacadeForUIProxy();
                            Int32 subCategoryId = bllFacade.AddSubCategory(_token, categoryTitle, subCategoryTitle, subCategoryDescription);
                            if (subCategoryId == 0)
                            {
                                MessageBox.Show("Добавление подкатегории закончилось неудачей!");
                                return;
                            }

                            //Add to page
                            CSubCategoryNotufyObject subCategory = new CSubCategoryNotufyObject(subCategoryId, subCategoryTitle, subCategoryDescription);
                            CCategoryNotifyObject category = _categories.Where(c => c.Title == categoryTitle).FirstOrDefault();
                            category.SubCategories.Add(subCategory);

                            category.Update();
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(e, "Произошло непредвиденное исключение при поптыке добавить новую подкатегорию: {0}", e.Message);
                        MessageBox.Show("Произошло непредвиденное исключение при поптыке добавить новую подкатегорию: " + e.Message);
                    }
                }));
            }
        }

        private CRelayCommand _editSubCategory;
        public CRelayCommand EditSubCategory
        {
            get
            {
                return _editSubCategory ?? (_editSubCategory = new CRelayCommand((o) =>
                {
                    try
                    {

                        CSubCategoryNotufyObject selectedSubCategory = o as CSubCategoryNotufyObject;
                        if (selectedSubCategory == null)
                        {
                            MessageBox.Show("Подкатегория для редактирования не была выбрана, убедитесь что вы выбрали подкатегорию");
                            return;
                        }

                        CCategoryNotifyObject oldCategory = Categories.Where(c => c.SubCategories.Contains(selectedSubCategory)).FirstOrDefault();
                        
                        
                        SubCategoryEditForm form = new SubCategoryEditForm(_categories, oldCategory, selectedSubCategory);
                        if(form.ShowDialog() == true)
                        {
                            String newCategoryTitle = form.CategoryTitle;
                            String newSubCategoryTitle = form.SubCategoryTitle;
                            String newSubCategoryDescription = form.Description;

                            if (String.IsNullOrEmpty(newSubCategoryTitle) || String.IsNullOrEmpty(newSubCategoryDescription) || String.IsNullOrEmpty(newCategoryTitle))
                            {
                                MessageBox.Show("При создании новой подкатегории необходимо заполнить/выбрать все поля.");
                                return;
                            }

                            //Add to DB
                            IBllFacadeForUI bllFacade = new CBllFacadeForUIProxy();
                            if (!bllFacade.EditSubCategory(_token, selectedSubCategory.SubCategoryId, newCategoryTitle, newSubCategoryTitle, newSubCategoryDescription))
                            {
                                MessageBox.Show("Редактирование категории закончилось неудачей.");
                                return;
                            }

                            //Add to page
                            selectedSubCategory.Title = newSubCategoryTitle;
                            selectedSubCategory.Description = newSubCategoryDescription;

                            if(oldCategory.Title != newCategoryTitle)
                            {
                                CCategoryNotifyObject newCategory = Categories.Where(c => c.Title == newCategoryTitle).FirstOrDefault();

                                oldCategory.SubCategories.Remove(selectedSubCategory);
                                newCategory.SubCategories.Add(selectedSubCategory);

                                oldCategory.Update();
                                newCategory.Update();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(e, "Произошло непредвиденное исключение при поптыке редактировать платеж: {0}", e.Message);
                        MessageBox.Show("Произошло непредвиденное исключение при поптыке добавить платеж: " + e.Message);
                    }
                }));
            }
        }

        private CRelayCommand _deleteSubCategory;
        public CRelayCommand DeleteSubCategory
        {
            get
            {
                return _deleteSubCategory ?? (_deleteSubCategory = new CRelayCommand((o) =>
                {
                    try
                    {
                        CSubCategoryNotufyObject selectedSubCategory = o as CSubCategoryNotufyObject;
                        if (selectedSubCategory == null)
                        {
                            MessageBox.Show("Подкатегория для редактирования не была выбрана, убедитесь что вы выбрали подкатегорию");
                            return;
                        }

                        CCategoryNotifyObject selectedCategory = Categories.Where(c => c.SubCategories.Contains(selectedSubCategory)).FirstOrDefault();

                        if (MessageBox.Show($"Вы уверены что хотите удалить выбранную подкатегорию: {selectedSubCategory.Title}?", "Подтвердите удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            //Edit in DB
                            IBllFacadeForUI bllFacade = new CBllFacadeForUIProxy();
                            if (bllFacade.DeleteSubCategory(_token, selectedSubCategory.SubCategoryId))
                                MessageBox.Show("Подкатегория удалена.");
                            else
                                MessageBox.Show("К сожалению удаление подкатегории прошло неудачно");

                            //Edit in View
                            selectedCategory.SubCategories.Remove(selectedSubCategory);
                            selectedCategory.Update();
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(e, "Произошло непредвиденное исключение при поптыке удалить платеж: {0}", e.Message);
                        MessageBox.Show("Произошло непредвиденное исключение при поптыке удалить платеж: " + e.Message);
                    }
                }));
            }
        }

    }
}
