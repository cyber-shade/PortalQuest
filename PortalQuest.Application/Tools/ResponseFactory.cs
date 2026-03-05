using PortalQuest.Application.Constants;
using PortalQuest.Application.DTOs.Common;

namespace PortalQuest.Application.Tools
{
	public static class ResponseFactory
	{
		public static ResponseDto ServerError()
		{
			return new ResponseDto()
			{
				Code = ResponseCodesEnum.ServerError,
				Message = SystemMessages.ServerError,
				Result = null
			};
		}
		public static ResponseDto<T> ServerError<T>() where T : class
		{
			return new ResponseDto<T>()
			{
				Code = ResponseCodesEnum.ServerError,
				Message = SystemMessages.ServerError,
				Result = null
			};
		}
		public static ResponseDto DataError(string message = "") {
			return new ResponseDto()
			{
				Code = ResponseCodesEnum.DataError,
				Message = !string.IsNullOrEmpty(message) ? message : SystemMessages.DataError,
				Result = null
			};
		}
		public static ResponseDto<T> DataError<T>(string message = "") where T : class
		{
			return new ResponseDto<T>()
			{
				Code = ResponseCodesEnum.DataError,
				Message = !string.IsNullOrEmpty(message) ? message : SystemMessages.DataError,
				Result = null
			};
		}
		public static ResponseDto<T> FillObject<T>(T result) where T : class 
		{
			return new ResponseDto<T>()
			{
				Code = ResponseCodesEnum.Ok,
				Message = string.Empty,
				Result = result
			};
		}
		public static ResponseDto FillObject(dynamic result)
		{
			return new ResponseDto()
			{
				Code = ResponseCodesEnum.Ok,
				Message = string.Empty,
				Result = result
			};
		}
	}
}
