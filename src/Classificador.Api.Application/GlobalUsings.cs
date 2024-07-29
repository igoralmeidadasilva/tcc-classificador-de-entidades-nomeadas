global using static Classificador.Api.Application.Models.ValidationError;
global using MediatR;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Classificador.Api.Application.Errors;
global using Classificador.Api.Application.Interfaces;
global using FluentValidation;
global using Classificador.Api.Domain;
global using System.Diagnostics;
global using Microsoft.Extensions.Logging;
global using Classificador.Api.Application.Behaviors;
global using Classificador.Api.Application.Commands.CreateUser;
global using Classificador.Api.Application.Extensions;
global using Classificador.Api.Domain.Entities;
global using Classificador.Api.Domain.Errors;
global using AutoMapper;
global using Classificador.Api.Domain.Interfaces.Services;
global using System.Security.Claims;
global using Classificador.Api.Domain.Models;
global using Classificador.Api.Application.Commands.LoginUser;
global using Classificador.Api.Application.Commands.UpdateUserRoleToAdmin;
global using Classificador.Api.Application.Commands.UpdateUserRoleToStandard;
global using Classificador.Api.Application.Models.Options;
global using Microsoft.AspNetCore.Http;
global using Classificador.Api.Domain.Interfaces.Repositories.Persistence;
global using Classificador.Api.Domain.Interfaces.Repositories.ReadOnly;
global using Classificador.Api.Application.Commands.CreatePrescribingInformationTxt;
global using System.Text;
global using Classificador.Api.SharedKernel.Shared.Results;
global using Classificador.Api.SharedKernel.Shared.Errors;
global using Classificador.Api.Application.Models;
global using Classificador.Api.Application.Commands.CreateClassification;
global using Classificador.Api.Application.Commands.CreateCategory;
global using Classificador.Api.SharedKernel.Shared.Extensions;
global using Classificador.Api.Application.Commands.CreateSpecialty;
global using Microsoft.AspNetCore.Authentication.Cookies;
global using Classificador.Api.Application.Commands.SendEmailToContact;