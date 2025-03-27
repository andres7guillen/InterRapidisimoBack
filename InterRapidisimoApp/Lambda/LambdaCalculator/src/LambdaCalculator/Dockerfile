FROM public.ecr.aws/lambda/dotnet:8
COPY "bin/Release/net8.0/linux-x64/publish" /var/task
CMD ["LambdaCalculator::LambdaCalculator.Function::FunctionHandler"]
