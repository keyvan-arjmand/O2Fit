using Chat.Application.Jobs;

namespace Chat.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        });
        
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            var deleteFileJobKey = new JobKey("DeleteFilesAfterThreeMonthsJob");
            var deleteGroupJobKey = new JobKey("DeleteGroupsAfterThreeMonthsJob");

            q.AddJob<DeleteFilesAfterThreeMonthsJob>(opts => opts.WithIdentity(deleteFileJobKey));
            q.AddJob<DeleteGroupsAfterThreeMonthsJob>(opts => opts.WithIdentity(deleteGroupJobKey));

            
            
            q.AddTrigger(opts => opts
                .ForJob(deleteFileJobKey)
                .WithIdentity("DeleteFilesAfterThreeMonthsJob-trigger")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInHours(24).RepeatForever()));
            
            
            q.AddTrigger(opts => opts
                .ForJob(deleteGroupJobKey)
                .WithIdentity("DeleteGroupsAfterThreeMonthsJob-trigger")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInHours(24).RepeatForever()));

        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        return services;
    }
}
