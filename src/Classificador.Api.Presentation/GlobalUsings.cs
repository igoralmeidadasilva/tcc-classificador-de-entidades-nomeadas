global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Mvc;
global using Classificador.Api.Infrastructure;
global using Classificador.Api.Application;
global using Classificador.Api.Presentation.Middlewares;
global using Classificador.Api.Infrastructure.Services.Seed;
global using Classificador.Api.Application.Commands.UpdateUserRoleToAdmin;
global using Classificador.Api.Application.Commands.UpdateUserRoleToStandard;
global using Classificador.Api.SharedKernel.Shared.Results;
global using MediatR;
global using Classificador.Api.Presentation;
global using Microsoft.OpenApi.Models;
global using Classificador.Api.Domain.Enums;
global using Microsoft.AspNetCore.Authorization;
global using Classificador.Api.Application.Commands.CreateUser;
global using Classificador.Api.Application.Commands.LoginUser;
global using Classificador.Api.Domain.Models;
global using Classificador.Api.Application.Commands.CreatePrescribingInformationTxt;
global using Classificador.Api.Application.Commands.CreateClassification;
global using Classificador.Api.Application.Queries.CountingVotesForNamedEntity;
global using Classificador.Api.Application.Commands.CreateCategory;
global using Classificador.Api.Application.Commands.CreateSpecialty;
global using System.Diagnostics;
global using Classificador.Api.Presentation.Models;