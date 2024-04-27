using Azure.ServiceBus.API.Data.BusinessObject;
using Azure.ServiceBus.API.Data.BusinessObjects;
using Azure.ServiceBus.API.Data.Core;

namespace MicrosoftAzure.EventGrid.API.Extension
{
    public static class DependencyRegistrationExtension
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection Services)
        {

            Services.AddSingleton<IConfigurationSettings, ConfigurationSettings>();

            Services.AddTransient<IFeedProcessSBQueueCore, FeedProcessSBQueueCore>();
            Services.AddTransient<IFeedProcessSBQueueBO, FeedProcessSBQueueBO>();
            Services.AddTransient<IFeedProcessSBTopicBO, FeedProcessSBTopicBO>();
            Services.AddTransient<IFeedProcessSBTopicCore, FeedProcessSBTopicCore>();

            return Services;
        }
        public static IServiceCollection AddApiDependencies(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            Services.AddControllers();
            Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            return Services;
        }
    }
}
