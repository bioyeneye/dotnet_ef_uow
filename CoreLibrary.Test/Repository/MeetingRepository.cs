using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.DataContext;
using CoreLibrary.EntityFramework;
using CoreLibrary.Repositories;
using CoreLibrary.Test.EF;
using CoreLibrary.UnitOfWork;

namespace CoreLibrary.Test.Repository
{
    public interface IMeetingRepository : IRepositoryAsync<GroupMeeting>
    {
        
    }


    public class MeetingRepository : Repository<GroupMeeting>, IMeetingRepository
    {
        public MeetingRepository(IDataContextAsync context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
