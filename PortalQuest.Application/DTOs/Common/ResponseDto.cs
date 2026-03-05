using PortalQuest.Application.Constants;

namespace PortalQuest.Application.DTOs.Common
{
	public class ResponseDto<T> where T : class
	{
		public ResponseCodesEnum Code { get; set; }
		public string Message { get; set; } = string.Empty;
		public T? Result { get; set; }
	}
	public class ResponseDto : ResponseDto<dynamic> { }

}
