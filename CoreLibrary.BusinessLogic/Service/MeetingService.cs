using System;
using System.Collections.Generic;
using CoreLibrary.Core.ViewModel;
using CoreLibrary.Cores;
using CoreLibrary.Data.EF;
using CoreLibrary.Data.Repositories;
using CoreLibrary.UnitOfWork;

namespace CoreLibrary.BusinessLogic.Service
{
    public interface IMeetingService
    {
        void Create(MeetingModel model, string username);
        void Edit(MeetingModel model);
        void Delete(int id);

        MeetingItem GetById(int id);
        MeetingItem GetDetailsById(int id);
        IEnumerable<MeetingModel> Query(int page, int count, MeetingFilter filter, string orderByExpression = null);
        IEnumerable<MeetingItem> Get();
        CounterModel<MeetingModel> GetCount(int page, int count, MeetingFilter filter, string orderByExpression = null);

        void UpdateMeeting(MeetingModel user);
        void ToggleActive(long id, string username);

        bool MeetingExists(MeetingModel model);

        //decimal? MeetingInStock(int cardId);
        int MeetingInStock(int cardId);
        List<MeetingItem> Search(string search);
    }

    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _repository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public MeetingService(IMeetingRepository repository, IUnitOfWorkAsync unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Create(MeetingModel model, string username)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                GroupMeeting meeting = new GroupMeeting
                {
                    Description = model.Description,
                    GroupMeetingDate = model.GroupMeetingDate,
                    GroupMeetingLeadName = model.GroupMeetingLeadName,
                    ProjectName = model.ProjectName,
                    TeamLeadName = model.TeamLeadName
                };
                _repository.Insert(meeting);
                _unitOfWork.Commit();
            }
            catch (System.Exception e)
            {
                _unitOfWork.Rollback();
                throw e;
            }
        }

        public void Edit(MeetingModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public MeetingItem GetById(int id)
        {
            throw new NotImplementedException();
        }

        public MeetingItem GetDetailsById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MeetingModel> Query(int page, int count, MeetingFilter filter, string orderByExpression = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MeetingItem> Get()
        {
            throw new NotImplementedException();
        }

        public CounterModel<MeetingModel> GetCount(int page, int count, MeetingFilter filter, string orderByExpression = null)
        {
            throw new NotImplementedException();
        }

        public void UpdateMeeting(MeetingModel user)
        {
            throw new NotImplementedException();
        }

        public void ToggleActive(long id, string username)
        {
            throw new NotImplementedException();
        }

        public bool MeetingExists(MeetingModel model)
        {
            throw new NotImplementedException();
        }

        public int MeetingInStock(int cardId)
        {
            throw new NotImplementedException();
        }

        public List<MeetingItem> Search(string search)
        {
            throw new NotImplementedException();
        }
    }
}