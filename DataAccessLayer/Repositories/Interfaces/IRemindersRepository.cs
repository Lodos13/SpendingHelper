﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRemindersRepository : IRepository<CReminderDto, CReminderKey>
    {
        ICollection<CReminderDto> FindByPersonId(Int32 personId);
    }
}