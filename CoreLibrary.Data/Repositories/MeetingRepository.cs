using CoreLibrary.Data.EF;
using CoreLibrary.DataContext;
using CoreLibrary.EntityFramework;
using CoreLibrary.Repositories;
using CoreLibrary.UnitOfWork;

namespace CoreLibrary.Data.Repositories
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
