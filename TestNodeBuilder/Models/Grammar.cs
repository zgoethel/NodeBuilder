
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestNodeBuilder.Models;

[Table("Grammar")]
[PrimaryKey("Id")]
public class Grammar
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime? Deleted { get; set; }
}

[Table("TokenGroup")]
[PrimaryKey("Id")]
public class TokenGroup
{
    public int Id { get; set; }
    public int GrammarId { get; set; }
    public string Name { get; set; } = "";
    public DateTime? Deleted { get; set; }
}

[Table("Token")]
[PrimaryKey("Id")]
public class Token
{
    public int Id { get; set; }
    public int GrammarId { get; set; }
    public int? TokenGroupId { get; set; }
    public string Name { get; set; } = "";
    public string Regex { get; set; } = "";
    public int OrderIndex { get; set; }
    public DateTime? Deleted { get; set; }
}
