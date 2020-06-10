using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace apple.model
{
    public class ResultMsg
    {
        public ResultMsg(Code code, string msg)
        {
            this.code = code;
            this.msg = msg;
        }
        /// <summary>
        /// 返回code
        /// </summary>
        public Code code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
    }

    public class ResultMsg<T> : ResultMsg
    {
        public ResultMsg(Code code, string msg, T data, int count) : base(code, msg)
        {
            this.data = data;
            this.count = count;
        }
        /// <summary>
        /// 数据集合
        /// </summary>
        public T data { get; set; }
        /// <summary>
        /// 数据总条数
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    /// 异常类型
    /// </summary>
    public enum Code
    {
        /// <summary>
        /// 成功
        /// </summary>
        success = 1,
        /// <summary>
        /// 失败
        /// </summary>
        error = -1,
        /// <summary>
        /// 账号已登陆
        /// </summary>
        isLogin = -2
    }

    public class ContainerModel<T> where T : class
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("items")]
        public IEnumerable<T> Items { get; set; }
    }

    public class ExecutionResult<TResult, TCode> : ExecutionResult<TResult>
    {
        public ExecutionResult()
            : base()
        {

        }

        public ExecutionResult(bool isSuccess, TResult result, TCode code, string message)
            : this(Guid.NewGuid().ToString(), isSuccess, result, code, message)
        {

        }

        public ExecutionResult(string requestId, bool isSuccess, TResult result, TCode code, string message)
            : base(requestId, isSuccess, result, message)
        {
            Code = code;
        }

        [JsonProperty("code")]
        public TCode Code { get; set; }
    }

    public class ExecutionResult<T> : ExecutionResult
    {
        public ExecutionResult()
            : base()
        {

        }

        public ExecutionResult(bool isSuccess, T result, string message)
            : this(Guid.NewGuid().ToString(), isSuccess, result, message)
        {

        }

        public ExecutionResult(bool isSuccess, T result, string errorCode, string message)
            : this(Guid.NewGuid().ToString(), isSuccess, result, errorCode, message)
        {

        }

        public ExecutionResult(string requestId, bool isSuccess, T result, string message)
            : this(requestId, isSuccess, result, "", message)
        {

        }

        public ExecutionResult(string requestId, bool isSuccess, T result, string errorCode, string message)
            : base(requestId, isSuccess, errorCode, message)
        {
            Result = result;
        }

        [JsonProperty("result")]
        public T Result { get; set; }
    }

    public class ExecutionResult
    {
        public ExecutionResult()
        {

        }

        public ExecutionResult(bool isSuccess, string message)
            : this(Guid.NewGuid().ToString(), isSuccess, message)
        {

        }

        public ExecutionResult(bool isSuccess, string errorCode, string message)
            : this(Guid.NewGuid().ToString(), isSuccess, errorCode, message)
        {

        }

        public ExecutionResult(string requestId, bool isSuccess, string message)
            : this(requestId, isSuccess, "", message)
        {

        }

        public ExecutionResult(string requestId, bool isSuccess, string errorCode, string message)
        {
            Message = message;
            IsSuccess = isSuccess;
            RequestId = requestId;
            ErrorCode = string.IsNullOrEmpty(errorCode) ? "" : errorCode;
        }

        [JsonProperty("succeed")]
        public bool IsSuccess { get; set; }

        [JsonIgnore]
        public string RequestId { get; set; }

        [JsonProperty("error")]
        public string ErrorCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public string Timestamp => DateTime.Now.Ticks.ToString();
    }
}
