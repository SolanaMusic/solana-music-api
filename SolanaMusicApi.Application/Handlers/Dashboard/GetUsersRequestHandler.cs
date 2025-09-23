using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolanaMusicApi.Application.Extensions;
using SolanaMusicApi.Application.Requests;
using SolanaMusicApi.Application.Services.UserServices.UserService;
using SolanaMusicApi.Domain.DTO.Dashboard;
using SolanaMusicApi.Domain.DTO.Pagination;
using SolanaMusicApi.Domain.DTO.User;
using SolanaMusicApi.Domain.Entities.User;

namespace SolanaMusicApi.Application.Handlers.Dashboard;

public class GetUsersRequestHandler(IUserService userService, IMapper mapper) 
    : IRequestHandler<GetUsersRequest, PaginationResponseDto<UserResponseDto>>
{
    public Task<PaginationResponseDto<UserResponseDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = userService.GetUsers();
        users = users.ApplySorting(request.Sorting);
        
        if (!string.IsNullOrEmpty(request.Filter.Query))
        {
            users = users.Where(x =>
                x.Email != null && x.UserName != null &&
                (EF.Functions.Like(x.UserName, $"%{request.Filter.Query}%") ||
                 EF.Functions.Like(x.Email, $"%{request.Filter.Query}%"))
            );
        }
        
        var paginated = new DashboardResponsePaginationDto<ApplicationUser>(request.Filter, users, x => x.Profile.CreatedDate);
        var response = mapper.Map<PaginationResponseDto<UserResponseDto>>(paginated);
        
        return Task.FromResult(response);
    }
}