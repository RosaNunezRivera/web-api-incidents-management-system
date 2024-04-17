# Incident Management System API

## Description

This project is an Incident Management System API designed to manage incidents, their classifications, resolutions, departments, group processes, standards, root causes, and processes. It provides endpoints for performing CRUD (Create, Read, Update, Delete) operations on various entities related to incident management.

## Technologies Used

- ASP.NET Core
- Entity Framework Core (Code First Approach)
- FluentValidation
- AutoMapper
- Dependency Injection
- C#
- SQL Server

## Installation Instructions

1. 1. Clone the repository to your local machine.
2. Navigate to the project directory.
3. Open the solution file in Visual Studio.
4. Restore NuGet packages.
5. Update the connection string in appsettings.json to point to your SQL Server instance.
6. Run the database migrations to create the database schema.
7. Build and run the project.

## Usage

he API provides various endpoints for managing incidents, classifications, resolutions, departments, group processes, standards, root causes, and processes.

## Contributing Guidelines

- Bug reports and feature requests can be submitted through GitHub issues.
- Pull requests are welcome. Please follow the existing code style and conventions.

## License Information

This project is licensed under the [MIT License](LICENSE)

## Documentation Links

## Project Structure

```arduino
Project Name  
│   Program.cs
|   README.md
│
└───ENTITIES
│   │
│   └───Context
│   │       IncidentsManagementContext.cs
│   │
│   └───Entities
│   │   │   Department.cs
│   │   │   GroupProcess.cs
│   │   │   IncidentClassification.cs
│   │   │   Incident.cs
│   │   │   IncidentResolution.cs
│   │   │   Process.cs
│   │   │   RootCause.cs
│   │   │   Standard.cs
│   │   │   
│   └───Validators
│   │   │   DepartmentValidator.cs
│   │   │   GroupProcessValidator.cs
│   │   │   IncidentClassificationValidator.cs
│   │   │   IncidentValidator.cs
│   │   │   IncidentResolutionValidator.cs
│   │   │   ProcessValidator.cs
│   │   │   RootCauseValidator.cs
│   │   │   StandardValidator.cs
│   │   
│   └───DTOs
│       │   DepartmentDTO.cs
│       │   GroupProcessDTO.cs
│       │   IncidentClassificationDTO.cs
│       │   IncidentDTO.cs
│       │   IncidentResolutionDTO.cs
│       │   ProcessDTO.cs
│       │   RootCauseDTO.cs
│       │   StandardDTO.cs
│
└───Controllers
│   │   DepartmentController.cs
│   │   GroupProcessController.cs
│   │   IncidentClassificationController.cs
│   │   IncidentController.cs
│   │   IncidentResolutionController.cs
│   │   ProcessController.cs
│   │   RootCauseController.cs
│   │   StandardController.cs
│ 
└───BLL
│   │   DepartmentService.cs
│   │   GroupProcessService.cs
│   │   IncidentClassificationService.cs
│   │   IncidentService.cs
│   │   IncidentResolutionService.cs
│   │   ProcessService.cs
│   │   RootCauseService.cs
│   │   StandardService.cs
│   
└───DAL
│   │   DepartmentRepository.cs
│   │   GroupProcessRepository.cs
│   │   IncidentClassificationRepository.cs
│   │   IncidentRepository.cs
│   │   IncidentResolutionRepository.cs
│   │   ProcessRepository.cs
│   │   RootCauseRepository.cs
│   │   StandardRepository.cs
│   
└───Mappers
│   │   MappingProfile.cs
│   
```

## Testing Instructions
- Use tools like Postman or Swagger UI to interact with the API endpoints.
- Send HTTP requests to the respective endpoints to test their functionalities.

## Contact Information

For questions or support, contact [Rosa Nunez](mailto:rosamnunezrivera@gmail.com).

## Version History
- v1.0 (Release Date: [2024-04-07]) Initial release of the Incident Management System API.
- v1.0 (Release Date: [2024-04-17]) Validations and README.md improvements.

## Code Example
```c#
/// <summary>
/// Metod AddDepartmentService implementing validations using DTOs
/// </summary>
/// <param name="departmentDto"></param>
/// <returns></returns>
public async Task<string> AddDepartmentService(DepartmentDTO departmentDto)
{
    Department department = _mapper.Map<Department>(departmentDto);

    var validationResult = _departmentValidator.Validate(department);

    //Validation using fluent validation
    if (!validationResult.IsValid)
    {
        string validationErrors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
        return $"Validation failed: {validationErrors}";
    }

    //Validation using a method in Repository
    if (await _departmentRepository.DepartmentNameExists(departmentDto.DepartmentName))
    {
    return "Validation failed: A department with the same name already exists.";
    }

     return await _departmentRepository.AddDepartment(departmentDto);
}   
```