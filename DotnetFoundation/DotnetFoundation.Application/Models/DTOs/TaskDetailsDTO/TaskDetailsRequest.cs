using System.ComponentModel.DataAnnotations;

namespace DotnetFoundation.Application.Models.DTOs.TaskDetailsDTO;

public record TaskDetailsRequest
{
    /// <summary>
    /// Gets the Description of the task.
    /// </summary>
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; init; }
    /// <summary>
    /// Gets the Budgeted Hours of the task.
    /// </summary>
    [Required(ErrorMessage = "BudgetedHours is required")]
    [RegularExpression(@"^\d*$",
        ErrorMessage = "BudgetedHours should be number greater than or equal to 0")]
    public int BudgetedHours { get; init; }
    /// <summary>
    /// Gets the user Id of the user the task is assigned to.
    /// </summary>
    [Required(ErrorMessage = "AssignedTo is required")]
    public int AssignedTo { get; init; }
    /// <summary>
    /// Gets the Category of the task.
    /// </summary>
    public string? Category { get; init; }
}