﻿
using Application.Common.Translators;
using Application.FieldWork.GetFieldWork.Request;
using Application.FieldWork.GetFieldWork.Response;
using Application.Ports;

namespace Application.FieldWork.GetFieldWork
{
    public class GetFieldWork : IGetFieldWork
    {
        private readonly IFieldWorkRepository _fieldWorkRepository;
        public GetFieldWork(IFieldWorkRepository fieldWorkRepository)
        {
            _fieldWorkRepository = fieldWorkRepository;
        }

        public async Task<GetFieldWorkResponse> Execute(GetFieldWorkRequest request)
        {
            var fieldwork = await _fieldWorkRepository.GetFieldWork(request.Id);
            var fieldworkDto = Translator.ToDTO(fieldwork);
            return new GetFieldWorkSuccessResponse
            {
                FieldWorkDTO = fieldworkDto
            };
        }
    }
}
