using Amazon.Lambda.Core;
using LambdaCalculator.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaCalculator
{
    public class Function
    {
        public int FunctionHandler(RequestModel nums, ILambdaContext context)
        {
            return nums.Num1 + nums.Num2;
        }
    }
}