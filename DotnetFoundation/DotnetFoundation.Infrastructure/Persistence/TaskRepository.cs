using DotnetFoundation.Application.DTO.TaskDetailsDTO;
using DotnetFoundation.Application.Interfaces.Persistence;
using DotnetFoundation.Application.Interfaces.Services;
using DotnetFoundation.Application.Services.EmailService;
using DotnetFoundation.Domain.Entities;
using DotnetFoundation.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace DotnetFoundation.Infrastructure.Persistence;

public class TaskDetailsRepository : ITaskDetailsRepository
{
    private readonly IConfiguration _configuration;
    private readonly SqlDatabaseContext _dbContext;
    private readonly SignInManager<IdentityApplicationUser> _signInManager;
    private readonly UserManager<IdentityApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailRepository _emailRepo;
    public TaskDetailsRepository(IConfiguration configuration, SqlDatabaseContext sqlDatabaseContext, SignInManager<IdentityApplicationUser> signinManager, RoleManager<IdentityRole> roleManager, UserManager<IdentityApplicationUser> userManager, IEmailRepository emailRepository)
    {
        _dbContext = sqlDatabaseContext;
        _configuration = configuration;
        _roleManager = roleManager;
        _signInManager = signinManager;
        _userManager = userManager;
        _emailRepo = emailRepository;
    }

    public async Task<List<TaskDetails>> GetAllTasksAsync()
    {
        List<TaskDetails> taskObj = (await _dbContext.TaskDetailsDbSet.ToListAsync().ConfigureAwait(false))
        .Select(taskObj => new TaskDetails {
            Id = taskObj.Id,
            Description = taskObj.Description,
            BudgetedHours = taskObj.BudgetedHours,
            AssignedTo = taskObj.AssignedTo,
            Category = taskObj.Category,
            Status = taskObj.Status,
            CreatedOn = taskObj.CreatedOn,
            CreatedBy = taskObj.CreatedBy,
            ModifiedOn = taskObj.ModifiedOn,
            ModifiedBy = taskObj.ModifiedBy
        }).ToList();
        return taskObj;
    }

    public async Task<TaskDetails?> GetTaskByIdAsync(int Id)
    {
        TaskDetails? taskObj = await _dbContext.TaskDetailsDbSet.FindAsync(Id).ConfigureAwait(false);
        return taskObj;
    }

    public async Task<string> AddTaskAsync(TaskDetailsRequest request)
    {
        try
        {
            TaskDetails inputTaskDetails = new TaskDetails
            {
                Description = request.Description,
                BudgetedHours = request.BudgetedHours,
                AssignedTo = request.AssignedTo,
                Category = request.Category,
                Status = StatusEnum.Active,
                CreatedBy = request.AssignedTo,
                ModifiedBy = request.AssignedTo,
                ModifiedOn = DateTime.UtcNow
            };

            _dbContext.TaskDetailsDbSet.Add(inputTaskDetails);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return $"Added Task: {request.Description}";
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            Console.WriteLine($"Error inserting TaskDetails: {ex.Message}");
            return $"Error inserting TaskDetails: {ex.Message}"; // Indicates failure
        }
    }
}