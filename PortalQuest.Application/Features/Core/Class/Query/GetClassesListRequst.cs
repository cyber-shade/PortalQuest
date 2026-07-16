using AutoMapper;
using MediatR;
using PortalQuest.Application.DTOs.Common;
using PortalQuest.Application.DTOs.Core;
using PortalQuest.Application.Interfaces.Repository.Core;
using PortalQuest.Application.Tools;

namespace PortalQuest.Application.Features.Core.Class.Query
{
	public class GetClassesListRequst : IRequest<ResponseDto<List<ClassDto>>>
	{
	}
	internal class GetClassesListRequestHandler(
		IClassRepository classRepository, IMapper mapper
	) : IRequestHandler<GetClassesListRequst, ResponseDto<List<ClassDto>>>
	{
		public async Task<ResponseDto<List<ClassDto>>> Handle(GetClassesListRequst request, CancellationToken cancellationToken)
		{
			var classes = await classRepository.GetAll(cancellationToken: cancellationToken);
			var model = mapper.Map<List<ClassDto>>( classes );
			return ResponseFactory.FillObject<List<ClassDto>>(model);
		}
	}
}
