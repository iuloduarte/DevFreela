using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Core.DTO;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries
{
    public class GetAllSkillQueryHandler : IRequestHandler<GetAllSkillQuery, List<SkillDTO>>
    {
        private readonly ISkillRepository _repository;
        
        public GetAllSkillQueryHandler(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SkillDTO>> Handle(GetAllSkillQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
