using IterationWebApp.ViewModels;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IterationWebApp.Models
{
    public class IterationRepository : IIterationRepository
    {
        private IterationContext _context;
        private IHostingEnvironment _environment;
        private IdentityDbContext _identityContext;
        

        //Constructor
        public IterationRepository(IterationContext context, IHostingEnvironment environment, IdentityDbContext identityContext)
        {
            _context = context;
            _environment = environment;
            _identityContext = identityContext;
            
        }

        //Get all the users
        public List<DisplayUserViewModel> GetAllUsers()
        {
            List<DisplayUserViewModel> vmUsers = new List<DisplayUserViewModel>();

            var users = _identityContext.Users.ToList();
            foreach (var u in users)
            {
                vmUsers.Add(new DisplayUserViewModel
                {
                    Id = u.Id,
                    Email = u.Email,

                });
            }
            return vmUsers;
        }
        ////Get User By ID
        //public DisplayUserViewModel GetUserById(string id)
        //{
        //    DisplayUserViewModel vmUser = new DisplayUserViewModel();
        //    var user = _identityContext.Users.Where(u => u.Id == id).SingleOrDefault();
        //    user.Email = vmUser.Email;
        //    return vmUser;
        //}

        //Delete user
        public IdentityUser GetUser(string id)
        {
           return _identityContext.Users.Where(u=>u.Id == id).FirstOrDefault();
            
        }

        //Get Company by ID
        public Company GetCompanyById(long id)
        {
            return _context.Companies.FirstOrDefault(c => c.Company_Id == id);
        }

        #region GET Method for Company
        //Get list of companies to the view
        public IEnumerable<Company> GetAllCompany()
        {
            var companies = _context.Companies.ToList();

            return companies;
        }

        #endregion

        #region GET Method for Questions
        //Get list of questions to the view
        public IEnumerable<Question> GetAllQuestion()
        {
            var questions = _context.Questions.ToList();

            return questions;
        }

        public IEnumerable<Question> GetAllQuestionWithQuestionTypeAndProcurement()
        {
            var questions = _context.Questions.Include(q => q.QuestionType).Include(p => p.Procurement).ToList();

            return questions;
        }

        #endregion

        #region Adding Companies method
        //add company
        public void AddCompany(CreateCompanyViewModel vmCompany)
        {
            var company = new Company();
            company.Company_Name = vmCompany.CompanyName;
            company.Corporate_Number = vmCompany.CorporateNumber;
            _context.Companies.Add(company);
            _context.SaveChanges();
        }
        #endregion

        #region GET Method for procurements
        //Get all procurements
        public IEnumerable<Procurement> GetAllProcurement()
        {
            //var pageSize = 4;
            //var skip = page * pageSize;

            return _context.Procurements.OrderByDescending(p=>p.PublishedDate).Include(c => c.Company).Include(q => q.Questions).ToList();
        }

        //Get all With Questions procurements
        public IEnumerable<Procurement> GetAllProcurementWithQuestions()
        {
            return _context.Procurements.Include(q => q.Questions).OrderBy(p => p.Service_Procured).ToList();
        }

        //Get total number of procurements with company
        public double GetTotalProcurements()
        {
            return _context.Procurements.Count();
        }


        #region Display Procurement Method
        //Display Procurements to View
        public List<DisplayProcurementViewModel> DisplayAllProcurements()
        {
            List<DisplayProcurementViewModel> items = new List<DisplayProcurementViewModel>();
            List<Procurement> procurements = _context.Procurements.Include(c => c.Company).Include(q => q.Questions).OrderByDescending(d => d.Date_Of_Submission).ToList();
            foreach (Procurement p in procurements)
            {
                items.Add(new DisplayProcurementViewModel
                {
                    Procurement_Id = p.Procurement_Id,
                    Service_Procured = p.Service_Procured,
                    Response_Evaluation = p.Response_Evaluation,
                    Date_Of_Submission = p.Date_Of_Submission,
                    CompanyName = p.Company.Company_Name,
                    CorporateNumber = p.Company.Corporate_Number,
                    Note = p.Note,
                    Questions = p.Questions


                });
            }
            return items;
        }
        #endregion

        #endregion

        #region Sorting Procurements by Titles
        //sorted by company name Asc
        public IEnumerable<Procurement> GetAllProcurementOrderByCompanyName()
        {
            //return DisplayAllProcurements().OrderBy(p => p.CompanyName).ToList();
            return _context.Procurements.OrderBy(p => p.Company.Company_Name).ToList();
        }
        //Desc
        public IEnumerable<Procurement> GetAllProcurementOrderByDesc()
        {
            //return DisplayAllProcurements().OrderByDescending(p => p.CompanyName).ToList();
            return _context.Procurements.OrderByDescending(p => p.Company.Company_Name).ToList();
        }

        //sorted by service procured Asc
        public IEnumerable<Procurement> GetAllProcurementOrderByServiceProcured()
        {
            //return DisplayAllProcurements().OrderBy(p => p.Service_Procured).ToList();

            return _context.Procurements.OrderBy(p => p.Service_Procured).ToList();
        }
        //sorted by service procured Desc
        public IEnumerable<Procurement> GetAllProcurementDescByServiceProcured()
        {
            //return DisplayAllProcurements().OrderByDescending(p => p.Service_Procured).ToList();
            return _context.Procurements.OrderByDescending(p => p.Service_Procured).ToList();
        }

        //sorted by Response Evaluation Asc
        public IEnumerable<Procurement> GetAllProcurementOrderByResEval()
        {
            //return DisplayAllProcurements().OrderBy(p => p.Response_Evaluation).ToList();
            return _context.Procurements.OrderBy(p => p.Response_Evaluation).ToList();
        }
        //sorted by Response Evaluation Desc
        public IEnumerable<Procurement> GetAllProcurementDescByResEval()
        {
            //return DisplayAllProcurements().OrderByDescending(p => p.Response_Evaluation).ToList();
            return _context.Procurements.OrderByDescending(p => p.Response_Evaluation).ToList();
        }

        //sorted by Date and Time Asc
        public IEnumerable<Procurement> GetAllProcurementOrderByDateOfSub()
        {
            //return DisplayAllProcurements().OrderBy(p => p.Date_Of_Submission).ToList();
            return _context.Procurements.OrderBy(p => p.Date_Of_Submission).ToList();
        }
        //sorted by Date and Time Desc
        public IEnumerable<Procurement> GetAllProcurementDescByDateOfSub()
        {
            //return DisplayAllProcurements().OrderByDescending(p => p.Date_Of_Submission).ToList();
            return _context.Procurements.OrderByDescending(p => p.Date_Of_Submission).ToList();
        }

        #endregion


        #region Search Function
        //filtered result for procurements
        public IEnumerable<Procurement> GetSpecificProcurement(string keyword)
        {
            return _context.Procurements.Where(p => p.Service_Procured.StartsWith(keyword) || p.Service_Procured.Contains(keyword));

            //return _context.Procurements.Where(p => p.Service_Procured.Contains(keyword));                    
        }

        public IEnumerable<Procurement> GetSpecificProcurementByCompany(string searchString)
        {
            return _context.Procurements.Where(p => p.Company.Company_Name.StartsWith(searchString) || p.Company.Company_Name.Contains(searchString));
        }

        public IEnumerable<Procurement> GetSpecificProcurementByResponse(string searchString)
        {
            return _context.Procurements.Where(p => p.Response_Evaluation.StartsWith(searchString) || p.Response_Evaluation.Contains(searchString));
        }

        public IEnumerable<Procurement> GetSpecificProcurementByDate(DateTime? searchString)
        {
            return _context.Procurements.Where(p => p.Date_Of_Submission.HasValue == searchString.HasValue);
        }
        #endregion

        #region Searching questions by User Keywords on the table
        //filtered result for questions
        public IEnumerable<Question> GetSpecificQuestion(string keyword)
        {
           // var newKeyword = keyword.Split(' ');

            return _context.Questions.Where(p => p.question.Contains(keyword) || p.question.StartsWith(keyword));
        }

        public IEnumerable<Question> GetSpecificQuestionsByProcurement(string searchString)
        {
            return _context.Questions.Where(p => p.Procurement.Service_Procured.Contains(searchString) || p.Procurement.Service_Procured.StartsWith(searchString));
        }

        public IEnumerable<Question> GetSpecificQuestionByWinningResponse(string searchString)
        {
            return _context.Questions.Where(p => p.Winning_Response.Contains(searchString) || p.Winning_Response.StartsWith(searchString));
        }

        public IEnumerable<Question> GetSpecificQuestionByReviewScore(string searchString)
        {
            return _context.Questions.Where(p => p.Review_Score_Iteration.Contains(searchString) || p.Review_Score_Iteration.StartsWith(searchString));
        }

        #endregion

        #region Add Procurement Method
        //add Procurement
        public void AddProcurement(CreateProcurementViewModel vmProcurement)
        {
            var procurement = new Procurement();
            var company = new Company();
            procurement.Service_Procured = vmProcurement.Service_Procured;
            procurement.Date_Of_Submission = DateTime.UtcNow.ToLocalTime();
            procurement.PublishedDate = procurement.Date_Of_Submission.ToString();
            procurement.Response_Evaluation = vmProcurement.Response_Evaluation;
            procurement.Note = vmProcurement.Note;

            if (vmProcurement.SelectedCompanyID != 0)
            {
                procurement.CompanyID = vmProcurement.SelectedCompanyID;
            }
            if (vmProcurement.SelectedCompanyID == 0)
            {
                company.Company_Name = vmProcurement.Company.Company_Name;
                company.Company_Id = vmProcurement.Company.Company_Id;
            }


            _context.Procurements.Add(procurement);
            _context.SaveChanges();
        }
        #endregion

        #region Load Companies on create procurement form 
        //First Load 
        public CreateProcurementViewModel LoadCompanies(CreateProcurementViewModel vmProcurement)
        {
            vmProcurement.Companies = _context.Companies.Select(c =>
            new SelectListItem
            {
                Value = c.Company_Id.ToString(),
                Text = c.Company_Name,


            }).ToList();


            return vmProcurement;
        }
        #endregion

        #region Edit Form Load Companies on create procurement form 
        //First Load 

        public EditProcurementViewModel LoadCompaniesToEdit(EditProcurementViewModel vmEditProcurement)
        {
            vmEditProcurement.Companies = _context.Companies.Select(c =>
            new SelectListItem
            {
                Value = c.Company_Id.ToString(),
                Text = c.Company_Name

            }).ToList();

            return vmEditProcurement;
        }
        #endregion

        #region Get Method for Question
        //GetById
        public Question GetQuestionById(long? id)
        {
            return _context.Questions.FirstOrDefault(q => q.Question_Id == id);
        }

        #endregion

        #region Adding Questions
        //Add Questions
        public void AddQuestions(CreateQuestionViewModel vmQuestion, ICollection<IFormFile> Files)
        {
            var question = new Question();
            question.Question_Number = vmQuestion.Question_Number;
            question.question = vmQuestion.question;
            question.Iteration_Response = vmQuestion.Iteration_Response;
            question.Review_Score_Iteration = vmQuestion.Review_Score_Iteration;
            question.Winning_Response = vmQuestion.Winning_Response;
            question.Review_Score_Winning_Company = vmQuestion.Review_Score_Winning_Company;
            question.Note = vmQuestion.Note;
            question.ProcurementID = vmQuestion.SelectedProcurementID;
            question.QuestionTypeID = vmQuestion.SelectedQuestionTypeID;

            //File uploading
            var uploads = Path.Combine(_environment.WebRootPath, "Documents");
            var stringUrls = new List<string>();
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Replace("#", "_").Trim('"');
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Replace('#', '_');
                    file.SaveAs(Path.Combine(uploads, fileName));
                    var relPath = "~/Documents/" + fileName;
                    stringUrls.Add(relPath);
                }

            }
            vmQuestion.FileUrl = string.Join(",", stringUrls);
            question.Documents = vmQuestion.FileUrl;
            _context.Questions.Add(question);
            _context.SaveChanges();
        }
        #endregion

        #region Loading Procurements and question list-On Creating Form for questions
        //Load Procurements and QuestionTypes
        public CreateQuestionViewModel LoadProcurementsAndQuestionTypes(CreateQuestionViewModel viewModel)
        {
            viewModel.Procurements = _context.Procurements.Select(c =>
            new SelectListItem
            {
                Value = c.Procurement_Id.ToString(),
                Text = c.Service_Procured

            }).ToList();

            viewModel.QuestionTypes = _context.Question_Types.Select(q =>
            new SelectListItem
            {
                Value = q.Question_Type_Id.ToString(),
                Text = q.Question_Type
            }).ToList();

            return viewModel;
        }

        //Load Procurements and QuestionTypes to Edit View
        public EditQuestionViewModel LoadProAndQuesToEdit(EditQuestionViewModel viewModel)
        {
            viewModel.Procurements = _context.Procurements.Select(c =>
            new SelectListItem
            {
                Value = c.Procurement_Id.ToString(),
                Text = c.Service_Procured

            }).ToList();

            viewModel.QuestionTypes = _context.Question_Types.Select(q =>
            new SelectListItem
            {
                Value = q.Question_Type_Id.ToString(),
                Text = q.Question_Type
            }).ToList();

            return viewModel;
        }

        #endregion

        #region Adding Question Types
        //Create Question types method
        public void AddQuestionType(CreateQuestionTypeViewModel vmQuestionType)
        {
            var questionType = new QuestionType();
            questionType.Question_Type = vmQuestionType.Question_Type;
            _context.Question_Types.Add(questionType);
            _context.SaveChanges();
        }
        #endregion

        #region  GET By Id method for All Models(Procurements, Questions, Question Types and Companies)
        //Get Procurement by ID with Company Details
        public Procurement GetByID(long? id)
        {
            return _context.Procurements.Include(q => q.Questions).Include(c => c.Company).Where(p => p.Procurement_Id == id).FirstOrDefault();
        }


        //Get by id | Procurements
        public DisplayProcurementViewModel GetProcurementById(long id)
        {
            DisplayProcurementViewModel vm = new DisplayProcurementViewModel();
            Procurement p = GetByID(id);
            List<Question> q = new List<Question>();
            q = p.Questions.ToList();
            List<QuestionType> qTypes = new List<QuestionType>();
            qTypes = _context.Question_Types.ToList();


            vm.Service_Procured = p.Service_Procured;
            vm.Response_Evaluation = p.Response_Evaluation;
            vm.Date_Of_Submission = p.Date_Of_Submission;
            vm.Note = p.Note;
            vm.CompanyName = p.Company.Company_Name;
            vm.CorporateNumber = p.Company.Corporate_Number;
            vm.Questions = q.ToList();


            return vm;
        }

        #endregion

        //Deleting Procurement and related questions.
        public void DeleteProcurement(long id)
        {
            var procurement = GetByID(id);
            var question = _context.Questions.Where(q => q.ProcurementID == id).FirstOrDefault();
            if (question != null)
            {
                _context.Questions.Remove(question);
            }

            _context.Procurements.Remove(procurement);
            _context.SaveChanges();


        }

        //public void UpdateCompany(Company c)
        //{
        //    _context.Update(c);
        //    _context.SaveChanges();
        //}

        public void UpdateProcurement(Procurement pro)
        {
            //EditProcurementViewModel vm = new EditProcurementViewModel();
            //pro.PublishedDate = vm.PublishedDate;
            pro.ModifiedDate = DateTime.UtcNow.ToLocalTime();
            
            _context.Update(pro);

            _context.SaveChanges();
        }

        public EditProcurementViewModel Edit(long? id)
        {
            Procurement procurement = _context.Procurements.Include(c => c.Company).FirstOrDefault(p => p.Procurement_Id == id);




            return new EditProcurementViewModel
            {
                Procurement_Id = procurement.Procurement_Id,

                Service_Procured = procurement.Service_Procured,
                Response_Evaluation = procurement.Response_Evaluation,
                Note = procurement.Note,
                CompanyID = procurement.CompanyID,
                PublishedDate = procurement.PublishedDate,
                //ModifiedDate = DateTime.UtcNow.ToLocalTime()
                
                
                


                //CompanyID = procurement.CompanyID


            };


        }

        #region Edit Question
        //Edit Question method
        public void UpdateQuestion(Question ques)
        {

            _context.Update(ques);

            _context.SaveChanges();
        }


        public EditQuestionViewModel EditQuestion(long? id)
        {
            Question question = _context.Questions.AsNoTracking().FirstOrDefault(q => q.Question_Id == id);


            return new EditQuestionViewModel
            {
                Question_Id = question.Question_Id,
                question = question.question,
                Question_Number = question.Question_Number,
                Review_Score_Iteration = question.Review_Score_Iteration,
                Iteration_Response = question.Iteration_Response,
                Note = question.Note,
                Winning_Response = question.Winning_Response,
                Review_Score_Winning_Company = question.Review_Score_Winning_Company,
                ProcurementID = question.ProcurementID,
                QuestionTypeID = question.QuestionTypeID,
                FileUrl = question.Documents

            };


        }

        #endregion

        //Deleting Procurement and related questions.
        public void DeleteQuestion(long? id)
        {
            var question = GetQuestionById(id);
            var procurement = _context.Procurements.Where(q => q.Procurement_Id == id).FirstOrDefault();
            if (procurement != null)
            {
                _context.Procurements.Remove(procurement);
            }

            _context.Questions.Remove(question);
            _context.SaveChanges();


        }

        //Get All Question Types
        public IEnumerable<QuestionType> GetAllQuestionTypes()
        {
            return _context.Question_Types.OrderByDescending(t => t.Question_Type).ToList();
        }
        //Get By ID Question Types
        public QuestionType GetQuestionTypeById(long? id)
        {
            return _context.Question_Types.FirstOrDefault(q => q.Question_Type_Id == id);
        }

        public void DeleteQuestionType(long? id)
        {
            var quesType = _context.Question_Types.Where(t => t.Question_Type_Id == id).FirstOrDefault();
            
            _context.Question_Types.Remove(quesType);
            _context.SaveChanges();
        }




    }
}
