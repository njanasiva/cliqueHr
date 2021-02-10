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
        public class HelpDeskService : IHelpDeskService 
    {
            private IHelpDeskRepository _iHelpDeskRepository;
            private ProbationValidation _modelValidation;
            private CourseTitleValidation _titleValidation;
            private MajorValidation _majorValidation;
            private UniversityValidation _universityValidation;
            private InstituteValidation _instituteValidation;

            public HelpDeskService()
            {
                _iHelpDeskRepository = new HelpDeskRepository();
                _modelValidation = new ProbationValidation();
                _titleValidation = new CourseTitleValidation();
                _majorValidation = new MajorValidation();
                _universityValidation = new UniversityValidation();
                _instituteValidation = new InstituteValidation();
            }

        public DataSet GetHelpDiskMaster(UserContextModel objUser, GetHelpDiskMaster getHelpDiskMaster)
        {
            try
            {
                var data = _iHelpDeskRepository.GetHelpDiskMaster(objUser.CompanyCode, getHelpDiskMaster);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<AddModifyCategory> AddModifyCategory(UserContextModel objUser, AddModifyCategory addModifyCategory)
        {
            try
            {
                var data = _iHelpDeskRepository.AddModifyCategory(objUser.CompanyCode, addModifyCategory);
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
