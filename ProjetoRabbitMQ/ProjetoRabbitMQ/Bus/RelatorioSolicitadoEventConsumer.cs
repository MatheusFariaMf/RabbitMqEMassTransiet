using MassTransit;
using ProjetoRabbitMQ.Relatorios;

namespace ProjetoRabbitMQ.Bus;

internal sealed class RelatorioSolicitadoEventConsumer : IConsumer<RelatorioSolicitadoEvent>
{
    private readonly ILogger<RelatorioSolicitadoEventConsumer> _logger;
    public RelatorioSolicitadoEventConsumer(ILogger<RelatorioSolicitadoEventConsumer> logger)
    {
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<RelatorioSolicitadoEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("Processando Relatório Id:{Id}, Nome:{Nome}", message.Id, message.Name);

        // Delay
        await Task.Delay(5000);
        // Atualizando Status
        var relatorio = Lista.Relatorios.FirstOrDefault(x => x.Id == message.Id);
        if (relatorio is not null) 
        {
            relatorio.Status = "Processado";
            relatorio.ProcessedTime = DateTime.UtcNow;
        }
        _logger.LogInformation("Relatório Processado Id:{Id}, Nome:{Nome}", message.Id, message.Name);
    }
}
