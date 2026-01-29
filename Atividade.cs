namespace aula14.Models;
public class Atividade
{    
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public bool Concluida { get; set; }
    public Prioridade NivelPrioridade { get; set; }

    public Atividade(string descricao, Prioridade prioridade = Prioridade.Media)
    {
        Descricao = descricao;
        DataCriacao = DateTime.Now;
        Concluida = false;
        NivelPrioridade = prioridade;
    }

    //Construtor vazio para desserialização
    public Atividade() { }

    public override string ToString()
    {
        string status = Concluida ? "[CONCLUÍDA]" : "[PENDENTE]";
        return $"{status} | Prioridade: {NivelPrioridade} | Criada em: {DataCriacao} | Descrição: {Descricao}";
    }

}