global using Xunit;
global using Classificador.Api.Domain.Entities;
global using Classificador.Api.Domain.Enums;
global using System.Security.Claims;
global using Microsoft.Extensions.Options;
global using Moq;
global using Classificador.Api.Infrastructure.Services;
global using Classificador.Api.Application.Models.Options;
global using AutoMapper;
global using Classificador.Api.Application.Commands.CreateUser;
global using Classificador.Api.Domain.Errors;
global using Classificador.Api.Domain.Interfaces.Services;
global using Classificador.Api.SharedKernel.Shared.Results;
global using Microsoft.Extensions.Logging;
global using FluentValidation;
global using FluentValidation.TestHelper;
global using Classificador.Api.Domain;
global using Classificador.Api.Application.Errors;
global using Classificador.Api.Application.Commands.LoginUser;
global using Classificador.Api.Domain.Models;
global using Classificador.Api.Application.Commands.UpdateUserRoleToAdmin;
global using Classificador.Api.Application.Commands.UpdateUserRoleToStandard;
global using Classificador.Api.Application.Commands.CreatePrescribingInformationTxt;
global using Classificador.Api.Domain.Interfaces.Repositories.Persistence;
global using Microsoft.AspNetCore.Http;
global using Classificador.Api.Domain.Interfaces.Repositories.ReadOnly;
global using Classificador.Api.Application.Queries.CountingVotesForNamedEntity;
global using Classificador.Api.Application.Commands.CreateCategory;
global using Classificador.Api.Application.Commands.CreateSpecialty;