namespace ProjetoDavin.Models
{
  public class Contato
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public List<Telefone> Telefones { get; set; }
  }
}
