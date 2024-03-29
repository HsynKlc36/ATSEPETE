﻿namespace AtSepete.UI.ApiResponses.BaseResponse
{
    public class ApiBaseDataResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
