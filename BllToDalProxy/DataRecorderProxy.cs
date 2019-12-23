using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace BlltoDalTransportContract
{
    class CDataRecorderProxy : IDataRecorder
    {
        static private Logger log = LogManager.GetCurrentClassLogger();

        IDataRecorder _recorder;

        public CDataRecorderProxy(String source = "DAL")
        {
            if (source.Equals("DAL"))
                _recorder = new CDataRecorderDB();
            else
            {
                log.Fatal("Data supplier are not defined");
                throw new ArgumentException();
            }
        }

        public void RegisterPerson(String name, String username, String email, String salt, String passwordHash)
        {
            _recorder.RegisterPerson(name, username, email, salt, passwordHash);
        }

        public Boolean AddPayment(Int32 personId, DateTime date, String category, String subCategory, Decimal sum)
        {
            return _recorder.AddPayment(personId,  date,  category,  subCategory, sum);
        }

        public Boolean EditPayment(Int32 personId, Int32 paymentId, DateTime newDate, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum)
        {
            return _recorder.EditPayment(personId, paymentId, newDate, newCategoryTitle, newSubCategoryTitle, newSum);
        }

        public Boolean DeletePayment(Int32 personId, Int32 paymentId)
        {
            return _recorder.DeletePayment(personId, paymentId);
        }

        public Boolean CreateFamily(Int32 personId, String familyName, Int32 budget)
        {
            return _recorder.CreateFamily(personId, familyName, budget);
        }

        public Boolean JoinFamily(Int32 personId, Int32 familyId)
        {
            return _recorder.JoinFamily(personId, familyId);
        }

        public Boolean LeaveFamily(Int32 personId)
        {
            return _recorder.LeaveFamily(personId);
        }
    }
}
