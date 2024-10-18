using Classificador.Api.Domain.Core.Enums;
using Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public sealed class ClassificationReadOnlyRepository : BaseReadOnlyRepository<Classification>, IClassificationReadOnlyRepository
{
    public ClassificationReadOnlyRepository(IDbContextFactory<ClassifierContext> context) : base(context)
    {
    }

    public async Task<IEnumerable<CountVoteForNamedEntity>> GetCountingVotesForNamedEntityAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Classifications
            .Where(cla => cla.IdNamedEntity == id)
            .Where(cla => cla.Status == ClassificationStatus.Completo)
            .AsNoTracking()
            .GroupBy(cla => new
            {
                CategoryName = cla.Category!.Name,
                NamedEntityName = cla.NamedEntity!.Name,
                CategoryId = cla.Category!.Id
            })
            .Select(group => new CountVoteForNamedEntity
            {
                Votes = group.Count(),
                Entity = group.Key.NamedEntityName,
                Category = group.Key.CategoryName
            })
            .OrderByDescending(x => x.Votes)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CountVoteForNamedEntity>> GetMostVotedEntityByPrescribingInformation(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        string query = @"
            SELECT 
                nome_entidade AS Entity,
                CASE 
                    WHEN nome_categoria IS NULL
                        THEN ''
                    ELSE
                        nome_categoria
                END AS Category,
                quantidade AS Votes,
                posicao_inicial AS Start,
                posicao_final AS End
            FROM
            (
                SELECT 
                    en.nome AS nome_entidade,
                    ca.nome AS nome_categoria,
                    COUNT(cl.""Id"") AS quantidade,
                    en.posicao_inicial AS posicao_inicial,
                    en.posicao_final AS posicao_final,
                    ROW_NUMBER() OVER (PARTITION BY en.""Id"" ORDER BY COUNT(*) DESC) AS rank
                FROM
                    entidades_nomeadas en
                LEFT JOIN
                    classificacoes cl ON en.""Id"" = cl.id_entidade_nomeada AND cl.status = 'Completo'
                LEFT JOIN
                    categorias ca ON cl.id_categoria = ca.""Id""
                WHERE 
                    en.id_bula = {0}
                GROUP BY
                    en.""Id"", en.nome, ca.nome, en.posicao_inicial, en.posicao_final
                ORDER BY 
                    en.nome
            )
            WHERE
                rank = 1
            ORDER BY 
                Entity;";

        return await context.Database
                                .SqlQueryRaw<CountVoteForNamedEntity>(query, id)
                                .AsNoTracking()
                                .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Classification>> GetPendingClassificationsByPrescribingInformationAndIdUser(
        Guid idPrescribingInformation, 
        Guid idUser, 
        CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Classifications
            .AsNoTracking()
            .Include(cla => cla.NamedEntity)
            .Include(cla => cla.Category)
            .Where(cla => cla.IdUser.Equals(idUser))
            .Where(cla => cla.Status.Equals(ClassificationStatus.Pendente))
            .Where(cla => cla.NamedEntity!.IdPrescribingInformation.Equals(idPrescribingInformation))
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountClassificationByUserId(Guid idUser, Guid idPrescribingInformation, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Classifications
            .AsNoTracking()
            .Include(cla => cla.NamedEntity)
            .Where(cla => cla.IdUser.Equals(idUser))
            .Where(cla => cla.Status.Equals(ClassificationStatus.Completo))
            .Where(cla => cla.NamedEntity!.IdPrescribingInformation.Equals(idPrescribingInformation))
            .CountAsync(cancellationToken);
    }

    public async Task<int> GetCountClassification(Guid idPrescribingInformation, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Classifications
            .AsNoTracking()
            .Include(cla => cla.NamedEntity)
            .Where(cla => cla.Status.Equals(ClassificationStatus.Completo))
            .Where(cla => cla.NamedEntity!.IdPrescribingInformation.Equals(idPrescribingInformation))
            .CountAsync(cancellationToken);
    }

    public async Task<bool> VerifyIfClassificationExistsAsync(Guid idNamedEntity, Guid idUser, CancellationToken cancellationToken = default)
    {
        using var context = _contextFactory.CreateDbContext();
        var result = await context.Classifications.AsNoTracking()
            .AnyAsync(x => x.IdUser.Equals(idUser) && x.IdNamedEntity.Equals(idNamedEntity), cancellationToken);

        return result;
    }

}

