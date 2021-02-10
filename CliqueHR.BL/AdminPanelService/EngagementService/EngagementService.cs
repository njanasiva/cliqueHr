using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
using System.Data;


namespace CliqueHR.BL
{
  public  class EngagementService : IEngagementService
    {
        private IEngagementRepository _iEngagementRepository;
        private ProbationValidation _modelValidation;
        private CourseTitleValidation _titleValidation;
        private MajorValidation _majorValidation;
        private UniversityValidation _universityValidation;
        private InstituteValidation _instituteValidation;

        public EngagementService()
        {
            _iEngagementRepository = new EngagementRepository();
            _modelValidation = new ProbationValidation();
            _titleValidation = new CourseTitleValidation();
            _majorValidation = new MajorValidation();
            _universityValidation = new UniversityValidation();
            _instituteValidation = new InstituteValidation();
        }

        public PaginationData<EngagementGroups> EngagementMarketPlace(UserContextModel objUser, EngagementGroups engagementGroups)
        {
            try
            {
                var data = _iEngagementRepository.EngagementMarketPlace(objUser.CompanyCode, engagementGroups);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public DataSet EngagementMaster(UserContextModel objUser, EngagementGroups engagementGroups)
        {
            try
            {
                var data = _iEngagementRepository.EngagementMaster(objUser.CompanyCode, engagementGroups);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public DataSet AddUpdateMarketPlace(UserContextModel objUser, AddUpdateMarketPlace addUpdateMarketPlace)
        {
            try
            {
                var data = _iEngagementRepository.AddUpdateMarketPlace(objUser.CompanyCode, addUpdateMarketPlace);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public DataSet AddUpdateEngagementSurvey(UserContextModel objUser, AddUpdateMarketPlace addUpdateMarketPlace)
        {
            try
            {
                var data = _iEngagementRepository.AddUpdateEngagementSurvey(objUser.CompanyCode, addUpdateMarketPlace);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<DailyContent> AddModifyDailyContent(UserContextModel objUser, DailyContent dailyContent)
        {
            try
            {
                var data = _iEngagementRepository.AddModifyDailyContent(objUser.CompanyCode, dailyContent);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }


    }
}
