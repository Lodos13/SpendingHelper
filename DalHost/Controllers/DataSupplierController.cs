using System;
using System.Collections.Generic;
using System.Web.Http;

using NLog;

using BlltoDalTransport;
using BusinessLogicLayer;

namespace DalHost
{
    public class DataSupplierController : ApiController, IDataSupplier
    {
        static private Logger log = LogManager.GetCurrentClassLogger();

        CDataSupplierDB _supplier = new CDataSupplierDB();

        [HttpGet]
        public List<CCategoryType> GetCategoryTypes()
        {
            try
            {
                return _supplier.GetCategoryTypes();
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataSupplierController.GetCategoryTypes. Message: {0}", ex.Message);
                return null;
            }
        }

        [HttpGet]
        public CPerson GetPersonById(Int32 personId)
        {
            try
            {
                return _supplier.GetPersonById(personId);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataSupplierController.GetPersonById (personId = {1}). Message: {0}", ex.Message, personId);
                return null;
            }
        }

        [HttpGet]
        public CFamily GetFamilyByPersonId(Int32 personId)
        {
            try
            {
                return _supplier.GetFamilyByPersonId(personId);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataSupplierController.GetFamilyByPersonId (personId = {1}). Message: {0}", ex.Message, personId);
                return null;
            }
        }



        [HttpGet]
        public CLoginData FindLoginByUsernameOrEmail(String username)
        {
            try
            {
                log.Trace($"Entered DataSupplierController.FindLoginByUsernameOrEmail. username = {username}");

                return _supplier.FindLoginByUsernameOrEmail(username);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataSupplierController.FindLoginByUsernameOrEmail (username = {1}). Message: {0}", ex.Message, username);
                return null;
            }
        }

        [HttpGet]
        public bool IsUsernameExisted(String username)
        {
            try
            {
                return _supplier.IsUsernameExisted(username);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataSupplierController.IsUsernameExisted (username = {1}). Message: {0}", ex.Message, username);
                return false;
            }
        }

        [HttpGet]
        public bool IsEmailExisted(String email)
        {
            try
            {
                return _supplier.IsEmailExisted(email);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataSupplierController.IsEmailExisted (email = {1}). Message: {0}", ex.Message, email);
                return false;
            }
        }



        [HttpGet]
        public String Ping()
        {
            return "Pong";
        }
    }
}
