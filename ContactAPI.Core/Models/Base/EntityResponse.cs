using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Core.Models.Base
{
    public class EntityResponse<T> : BaseResponseModel
    {
        public T Result { get; set; }
        public EntityResponse()
        {

        }
        public EntityResponse(string message, T result, EntityResponseStatus status, List<Exception> exception)
        {
            Message=message;
            Result=result;
            Status=status;
            Exception=exception;
        }

        public bool IsSuccess()
        {
            return this.Status == EntityResponseStatus.Success;
        }
        public static EntityResponseBuilder<T> Builder()
        {
            return new EntityResponseBuilder<T>();
        }

        public class EntityResponseBuilder<T> : BaseResponseModel
        {
            public T Data { get; set; }
            public EntityResponseBuilder<T> SetException(Exception exception)
            {
                this.Exception.Add(exception);
                this.Status = EntityResponseStatus.Error;
                return this;
            }
            public EntityResponseBuilder<T> SetResult(T resultModel)
            {
                this.Data = resultModel;
                return this;
            }
            public EntityResponseBuilder<T> SetMessage(string message)
            {
                this.Message=message;
                return this;
            }
            public EntityResponseBuilder<T> SetSuccessStatus()
            {
                this.Status = EntityResponseStatus.Success;
                return this;
            }
            public EntityResponseBuilder<T> SetErrorStatus()
            {
                this.Status = EntityResponseStatus.Error;
                return this;
            }
            public  EntityResponse<T> Build()
            {
                return new EntityResponse<T>()
                {
                    Result = this.Data,
                    Exception= this.Exception,
                    Message = this.Message,
                    Status = this.Status
                };
            }
        }

    }


}
