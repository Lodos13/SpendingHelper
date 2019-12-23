using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

using BusinessLogicLayer;
using DataAccessLayer;

namespace BlltoDalTransport
{
    internal class CDalToBllConverter
    {
        static Logger log = LogManager.GetCurrentClassLogger();

        private CAllCategoryTypes _types = new CAllCategoryTypes();

        private CPerson ConvertPerson(CPersonDto personDal, CFamily knownFamilyBll = null)
        {
            CPerson personBll;
            try
            {
                if (personDal == null)
                    return null;

                Int32 id = personDal.PersonID;
                String name = personDal.Name;
                Int32 budget = personDal.Budget ?? 0;

                personBll = new CPerson(id, name, budget);
                CFamily familyBll;
                if (knownFamilyBll == null && personDal.FamilyID != null)
                {
                    CFamilyDto familyDto = personDal.Family;
                    familyBll = ConvertFamily(familyDto, personBll);
                }
                else
                    familyBll = knownFamilyBll;
                personBll.Family = familyBll;

                foreach (CSubCategoryDto subCategory in personDal.SubCategories)
                    personBll.SubCategories.Add(ConvertSubCategory(subCategory));

                foreach (CSpendingHistoryDto spendingHistory in personDal.SpendingHistories)
                    personBll.Histories.Add(ConvertSpendingHistory(spendingHistory));

                foreach (CPersonalTargetDto pTarget in personDal.PersonalTargets)
                    personBll.PersonalTargets.Add(ConvertPersonalTarget(pTarget));

                foreach (CReminderDto reminder in personDal.Reminders)
                    personBll.Reminders.Add(ConvertReminders(reminder));
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to convert CPersonDto (id = {1}) to CPerson. Message: {0}", ex.Message, personDal.PersonID);
                throw;
            }

            return personBll;
        }
        public CPerson ConvertPerson(CPersonDto personDal)
        {
            return ConvertPerson(personDal, null);
        }

        public CLoginData ConvertLogin(CLoginDataDto loginDataDto)
        {
            Int32 personID = loginDataDto.PersonID;
            String email = loginDataDto.email;
            String username = loginDataDto.Username;
            String hash = loginDataDto.PasswordHash;
            String salt = loginDataDto.Salt;

            return new CLoginData(personID, email, username, hash, salt);
        }

        private CFamily ConvertFamily(CFamilyDto familyDal, CPerson knownMember = null)
        {
            if (familyDal == null)
                return null;

            Int32 id = familyDal.FamilyID;
            String fName = familyDal.FamilyName;
            Int32 budget = familyDal.Budget ?? 0;
            Int32 membersCounter = familyDal.MembersCounter ?? 0;
            CFamily familyBll = new CFamily(id, fName, budget, membersCounter);

            foreach (CFamilyTargetDto fTarget in familyDal.FamilyTargets)
                familyBll.FamilyTargets.Add(ConvertFamilyTarget(fTarget));

            foreach (CPersonDto member in familyDal.People)
            {
                if (knownMember != null && knownMember.PersonID == member.PersonID)
                    familyBll.FamilyMembers.Add(knownMember);
                else
                    familyBll.FamilyMembers.Add(ConvertPerson(member, familyBll));
            }

            return familyBll;
        }
        public CFamily ConvertFamily(CFamilyDto familyDal)
        {
            return ConvertFamily(familyDal, null);
        }

        private CPayment ConvertPayment(CPaymentDto paymentDal)
        {
            Int32 id = paymentDal.PaymentID;
            DateTime date = paymentDal.Date;
            Decimal spended = paymentDal.Spended;
            return new CPayment(id, date, spended); ;
        }

        private CTarget ConvertPersonalTarget(CPersonalTargetDto pTargetDal)
        {
            DateTime month = pTargetDal.Month;
            String type = pTargetDal.Type;
            Decimal amount = pTargetDal.Amount;
            CCategoryType categoryType = _types.GetByCategoryId(pTargetDal.CategoryID);

            return new CTarget(month, type, amount, categoryType);
        }

        private CTarget ConvertFamilyTarget(CFamilyTargetDto fTargetDal)
        {
            DateTime month = fTargetDal.Month;
            String type = fTargetDal.Type;
            Decimal amount = fTargetDal.Amount;
            CCategoryType categoryType = _types.GetByCategoryId(fTargetDal.CategoryID);

            return new CTarget(month, type, amount, categoryType);
        }

        private CReminder ConvertReminders(CReminderDto reminderDal)
        {
            String title = reminderDal.Title;
            String description = reminderDal.Description;
            Int32 dayOfMonth = reminderDal.DayOfMonth;

            return new CReminder(title, description, dayOfMonth);
        }

        private CSpendingHistory ConvertSpendingHistory(CSpendingHistoryDto historyDal)
        {
            DateTime month = historyDal.Month;
            Decimal spended = historyDal.Spended;
            return new CSpendingHistory(month, spended);
        }

        private CSubCategory ConvertSubCategory(CSubCategoryDto subCategoryDal)
        {
            Int32 id = subCategoryDal.SubCategoryID;
            String title = subCategoryDal.Title;
            String description = subCategoryDal.Description;
            CCategoryType categoryType = _types.GetByCategoryId(subCategoryDal.ParentCategoryID);

            CSubCategory subCategoryBll = new CSubCategory(id, title, description, categoryType);

            foreach (CPaymentDto payment in subCategoryDal.Payments)
                subCategoryBll.Payments.Add(ConvertPayment(payment));

            return subCategoryBll;
        }
    }
}
