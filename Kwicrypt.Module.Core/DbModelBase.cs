using System.ComponentModel.DataAnnotations;

namespace Kwicrypt.Module.Core;

public abstract class DbModelBase
{
    [Key]
    public Guid Id { get; private set; }

    public DbModelBase(){}
    public DbModelBase(Guid id)
    {
        Id = id;
    }
}