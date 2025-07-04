﻿
using Application.Exceptions;
using Application.FieldWork.SendFieldWorkEmail.Response;
using Application.FieldWork.UpdateBldReviewStatus.Request;
using Application.FieldWork.UpdateBldReviewStatus.Response;
using Application.Ports;
using Domain.Enum;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;

namespace Application.FieldWork.UpdateBldReviewStatus
{
    public class UpdateBldReviewStatus : IUpdateBldReviewStatus
    {
        private readonly ILogger _logger;
        private readonly IFieldWorkRepository _fieldWorkRepository;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IJobDispatcher _jobDispatcher;

        public UpdateBldReviewStatus(ILogger<UpdateBldReviewStatus> logger, IFieldWorkRepository fieldWorkRepository, IServiceScopeFactory serviceScopeFactory, IConfiguration configuration, IJobDispatcher jobDispatcher)
        {
            _logger = logger;
            _fieldWorkRepository = fieldWorkRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
            _jobDispatcher = jobDispatcher;
        }
        public async Task<UpdateBldReviewStatusResponse> Execute(UpdateBldReviewStatusRequest request)
        {
            try
            {
                _jobDispatcher.ScheduleBldReviewAndEmail(request.Id, request.UpdatedUser);
                return new UpdateBldReviewStatusSuccessResponse
                {
                    Message = "Job for updating status queued. Emails will be sent after update completes."
                };
            }
            catch (AppException appEx)
            {
                _logger.LogError(appEx,"An error occurred");
                throw new AppException(appEx.Message);
            }
        }


    }
}
