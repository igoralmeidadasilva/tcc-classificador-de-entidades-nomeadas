global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Mvc;
global using FluentValidation;
global using Classificador.Api.Infrastructure.IoC;
global using Classificador.Api.Application.IoC;
global using Classificador.Api.Presentation.Middlewares;
global using Classificador.Api.SharedKernel.Shared;
global using Classificador.Api.Infrastructure.Services.Seed;
global using Classificador.Api.Application.Commands.UpdateUserRoleToAdmin;
global using Classificador.Api.Application.Commands.UpdateUserRoleToStandard;
global using Classificador.Api.SharedKernel.Shared.Result;
global using MediatR;
global using Classificador.Api.Presentation.IoC;
global using Microsoft.OpenApi.Models;
global using Classificador.Api.Domain.Enums;
global using Microsoft.AspNetCore.Authorization;
global using Classificador.Api.Application.Commands.CreateUser;
global using Classificador.Api.Application.Commands.LoginUser;
global using Classificador.Api.Domain.Models;
global using Classificador.Api.Application.Commands.CreatePrescribingInformationTxt;