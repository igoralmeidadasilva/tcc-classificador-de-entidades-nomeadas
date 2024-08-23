namespace Classificador.Api.Infrastructure.Repositories.ReadOnly;

public sealed class ClassificationReadOnlyRepository : BaseReadOnlyRepository<Classification>, IClassificationReadOnlyRepository
{
    public ClassificationReadOnlyRepository(ClassifierContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CountVoteForNamedEntity>> GetCountingVotesForNamedEntityAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Classifications
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
        string query = @"
            SELECT 
                nome_entidade as Entity,
                nome_categoria as Category,
                quantidade as Votes
            FROM
            (
                SELECT
                    en.nome AS nome_entidade,
                    ca.nome AS nome_categoria,
                    COUNT(*) AS quantidade,
                    ROW_NUMBER() OVER (PARTITION BY en.""Id"" ORDER BY COUNT(*) DESC) AS rank
                FROM
                    classificacoes cl
                JOIN
                    categorias ca ON cl.id_categoria = ca.""Id""
                JOIN
                    entidades_nomeadas en ON cl.id_entidade_nomeada = en.""Id""
                WHERE 
                    en.id_bula = {0}
                AND
                    cl.status = 'Completo'
                GROUP BY
                    en.""Id"", en.nome, ca.nome
            )
            WHERE
                rank = 1
            ORDER BY nome_entidade;
        ";

        return await _context.Database
                                .SqlQueryRaw<CountVoteForNamedEntity>(query, id)
                                .AsNoTracking()
                                .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Classification>> GetPendingClassificationsByPrescribingInformationAndIdUser(
        Guid idPrescribingInformation, 
        Guid idUser, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Classifications
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
        return await _context.Classifications
            .AsNoTracking()
            .Include(cla => cla.NamedEntity)
            .Where(cla => cla.IdUser.Equals(idUser))
            .Where(cla => cla.Status.Equals(ClassificationStatus.Completo))
            .Where(cla => cla.NamedEntity!.IdPrescribingInformation.Equals(idPrescribingInformation))
            .CountAsync(cancellationToken);
    }
}

