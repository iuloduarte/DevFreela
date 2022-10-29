﻿using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Persistence.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public  class GetAllProjectsQueryHandler: IRequestHandler<GetAllProjectsQuery, PaginationResult<ProjectViewModel>>
    {
        private readonly IProjectRepository _projectsRepository;
        public GetAllProjectsQueryHandler(IProjectRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

        public async Task<PaginationResult<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var paginationProjects = await _projectsRepository.GetAllAsync(request.Query, request.Page);  

            var projectsViewModel = paginationProjects
                .Data
                .Select(q => new ProjectViewModel(q.Id, q.Title, q.CreatedAt))
                .ToList();

            var paginationProjectsViewModel = new PaginationResult<ProjectViewModel>(
                paginationProjects.Page,
                paginationProjects.TotalPages,
                paginationProjects.PageSize,
                paginationProjects.ItemsCount,
                projectsViewModel);

            return paginationProjectsViewModel;
        }
    }
}
