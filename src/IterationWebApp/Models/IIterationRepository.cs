using IterationWebApp.ViewModels;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace IterationWebApp.Models
{
    public interface IIterationRepository
    {
        //GetUser Method
        List<DisplayUserViewModel> GetAllUsers();
        //DisplayUserViewModel GetUserById(string id);
        IdentityUser GetUser(string id);

        //Get Method
        IEnumerable<Company> GetAllCompany();
        Company GetCompanyById(long id);
        IEnumerable<Question> GetAllQuestion();
        IEnumerable<Question> GetAllQuestionWithQuestionTypeAndProcurement();
        IEnumerable<QuestionType> GetAllQuestionTypes();


        void AddCompany(CreateCompanyViewModel vmCompany);
        void AddProcurement(CreateProcurementViewModel vmProcurement);

        //Get method
        IEnumerable<Procurement> GetAllProcurement();
        IEnumerable<Procurement> GetAllProcurementWithQuestions();
        List<DisplayProcurementViewModel> DisplayAllProcurements();
        double GetTotalProcurements();

        //First Load
        CreateProcurementViewModel LoadCompanies(CreateProcurementViewModel vmProcurement);
        CreateQuestionViewModel LoadProcurementsAndQuestionTypes(CreateQuestionViewModel viewModel);

        //Add Method
        void AddQuestions(CreateQuestionViewModel vmQuestion, ICollection<IFormFile> Files);
        void AddQuestionType(CreateQuestionTypeViewModel vmQuestionType);

        // Get by ID methods
        DisplayProcurementViewModel GetProcurementById(long id);
        Procurement GetByID(long? id);
        Question GetQuestionById(long? id);
        QuestionType GetQuestionTypeById(long? id);





        //Filtered Procurements
        IEnumerable<Procurement> GetSpecificProcurement(string keyword);
        IEnumerable<Procurement> GetSpecificProcurementByCompany(string searchString);
        IEnumerable<Procurement> GetSpecificProcurementByResponse(string searchString);
        IEnumerable<Procurement> GetSpecificProcurementByDate(DateTime? searchString);

        //Filtered Questions
        IEnumerable<Question> GetSpecificQuestion(string keyword);
        IEnumerable<Question> GetSpecificQuestionsByProcurement(string searchString);
        IEnumerable<Question> GetSpecificQuestionByWinningResponse(string searchString);
        IEnumerable<Question> GetSpecificQuestionByReviewScore(string searchString);

        //Sorting
        IEnumerable<Procurement> GetAllProcurementOrderByCompanyName();
        IEnumerable<Procurement> GetAllProcurementOrderByDesc();
        IEnumerable<Procurement> GetAllProcurementOrderByServiceProcured();
        IEnumerable<Procurement> GetAllProcurementDescByServiceProcured();
        IEnumerable<Procurement> GetAllProcurementOrderByResEval();
        IEnumerable<Procurement> GetAllProcurementDescByResEval();
        IEnumerable<Procurement> GetAllProcurementOrderByDateOfSub();
        IEnumerable<Procurement> GetAllProcurementDescByDateOfSub();


        //Delete methods
        void DeleteProcurement(long id);
        void DeleteQuestion(long? id);
        void DeleteQuestionType(long? id);

        //Edit Procurement
        EditProcurementViewModel Edit(long? id);
        void UpdateProcurement(Procurement pro);
        EditProcurementViewModel LoadCompaniesToEdit(EditProcurementViewModel vmEditProcurement);

        //Edit Question
        EditQuestionViewModel EditQuestion(long? id);
        void UpdateQuestion(Question ques);
        EditQuestionViewModel LoadProAndQuesToEdit(EditQuestionViewModel viewModel);





    }
}